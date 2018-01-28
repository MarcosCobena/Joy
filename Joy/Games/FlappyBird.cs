using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;

namespace Joy
{
    public class FlappyBird : Game
    {
        public static float OnePixelPerMillisecondSpeed;

        JImage bird;
        float birdX, birdY, birdYSpeed;
        SKRect fullScreen;
        List<Wall> walls;

        public FlappyBird() : base(32, 32)
        {
            OnePixelPerMillisecondSpeed = Width / (60 * 1000);

            walls = new List<Wall>(5);
        }

        public override void Load()
        {
            bird = LoadImage("1pxBlack.png");
            birdX = Width / 2;
            birdY = 0;
            birdYSpeed = 0;

            fullScreen = SKRect.Create(Width, Height);

            walls.Clear();

            for (int i = 0; i < walls.Capacity; i++)
            {
                var wall = new Wall(this, 10 * i);
                wall.Load();
                walls.Add(wall);
            }
        }

		public override void Think(TimeSpan timeSincePreviousCall)
		{
            if (birdY < 0)
            {
                birdY = 0;
                birdYSpeed = 0;
            }
            else if (walls.Any(wall => wall.Touches(birdX, birdY)) || birdY >= Height)
            {
                Load();
                return;
            }

            foreach (var wall in walls)
            {
                wall.Think(timeSincePreviousCall);
            }

            if (DetectsTouchAt(fullScreen))
            {
                birdYSpeed = -OnePixelPerMillisecondSpeed * 1.5f * Width;
            }

            birdY += birdYSpeed * timeSincePreviousCall.Milliseconds;
            birdYSpeed += OnePixelPerMillisecondSpeed;
		}

        public override void Paint()
        {
            Erase(SKColors.White);

            foreach (var wall in walls)
            {
                wall.Paint();
            }

            Paint(bird, birdX, birdY);
        }

        class Wall
        {
            const int GapHeight = 10;
            const string WallFilename = "Wall.png";

            readonly Game game;
			readonly int xOffset;

            JImage bottomWall, topWall;
            float bottomWallY, topWallY, wallsX;
            Random random;

            public Wall(Game game, int xOffset)
            {
                this.game = game;
                this.xOffset = xOffset;
            }

            internal bool Touches(float x, float y)
			{
                var distanceBetweenXAndWallsX = (int)Math.Floor(wallsX) - (int)Math.Floor(x);
                bool doesBirdTouchWall;

                if (distanceBetweenXAndWallsX == 1 && (y < topWallY + topWall.NativeImage.Height || y > bottomWallY))
                {
                    doesBirdTouchWall = true;
                }
                else
                {
                    doesBirdTouchWall = false;
                }

                return doesBirdTouchWall;
			}

            internal void Load()
            {
                bottomWall = game.LoadImage(WallFilename);
                topWall = game.LoadImage(WallFilename);

                wallsX = game.Width + xOffset;
                random = new Random();
                var halfScreenHeight = (int)Math.Round(game.Height / 2);
                topWallY = random.Next(-halfScreenHeight - GapHeight, -halfScreenHeight + GapHeight + 1);
                bottomWallY = topWallY + topWall.NativeImage.Height + GapHeight;
            }

            internal void Think(TimeSpan timeSincePreviousCall)
            {
                wallsX -= 5 * OnePixelPerMillisecondSpeed * timeSincePreviousCall.Milliseconds;
            }
            
            internal void Paint()
            {
                game.Paint(topWall, wallsX, topWallY);
                game.Paint(bottomWall, wallsX, bottomWallY);
            }
        }
    }
}
