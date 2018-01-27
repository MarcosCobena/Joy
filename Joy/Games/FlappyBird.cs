using System;
using SkiaSharp;

namespace Joy
{
    // TODO w & h could be exposed without PaintingSize to shorten it up
    public class FlappyBird : Game
    {
        public static float OnePixelPerMillisecondSpeed;

        JImage bird;
        float birdX, birdY, birdYSpeed;
        SKRect fullScreen;
        Wall wall;

        public FlappyBird() : base(32, 32)
        {
            OnePixelPerMillisecondSpeed = PaintingSize.Width / (60 * 1000);

            wall = new Wall(this);
        }

        public override void Load()
        {
            bird = LoadImage("1pxBlack.png");
            birdX = PaintingSize.Width / 2;
            birdY = 0;
            birdYSpeed = 0;

            fullScreen = SKRect.Create(PaintingSize);

            wall.Load();
        }

		public override void Think(TimeSpan timeSincePreviousCall)
		{
            wall.Think(timeSincePreviousCall);

            if (birdY < 0)
            {
                birdY = 0;
                birdYSpeed = 0;
            }
            else if (wall.Touches(birdX, birdY) || birdY >= PaintingSize.Height)
            {
                Load();
                return;
            }

            if (DetectsTouchAt(fullScreen))
            {
                birdYSpeed = -OnePixelPerMillisecondSpeed * 1.5f * PaintingSize.Width;
            }

            birdY += birdYSpeed * timeSincePreviousCall.Milliseconds;
            birdYSpeed += OnePixelPerMillisecondSpeed;
		}

        public override void Paint()
        {
            Erase(SKColors.White);

            wall.Paint();
            Paint(bird, (int)birdX, (int)birdY);
        }

        class Wall
        {
            const int GapHeight = 5;

			readonly Game game;

            JImage bottomWall, topWall;
            float bottomWallY, topWallY, wallsX;
            Random random;

            public Wall(Game game)
            {
                this.game = game;
            }

            internal bool Touches(float x, float y)
			{
                if (x >= wallsX - 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
			}

            internal void Load()
            {
                bottomWall = game.LoadImage("Wall.png");
                topWall = game.LoadImage("Wall.png");

                wallsX = game.PaintingSize.Width;
                random = new Random();
                var halfScreenHeight = (int)Math.Round(game.PaintingSize.Height / 2);
                bottomWallY = random.Next(-halfScreenHeight - GapHeight, -halfScreenHeight + GapHeight + 1);
                topWallY = bottomWallY + bottomWall.NativeImage.Height + GapHeight;
            }

            internal void Think(TimeSpan timeSincePreviousCall)
            {
                wallsX -= 3 * OnePixelPerMillisecondSpeed * timeSincePreviousCall.Milliseconds;
            }
            
            internal void Paint()
            {
                game.Paint(topWall, (int)wallsX, (int)topWallY);
                game.Paint(bottomWall, (int)wallsX, (int)bottomWallY);
            }
        }
    }
}
