using System;
using SkiaSharp;

namespace Joy
{
    public class FlappyBird : Game
    {
        const float VerticalSpeed = .00001f;

        JImage bird;
        float birdX, birdY, birdYSpeed;
        SKRect fullScreen;

        public FlappyBird() : base(16, 16)
        {
        }

        public override void Load()
        {
            bird = LoadImage("1pxBlack.png");
            birdX = PaintingSize.Width / 2;
            birdY = 0;
            birdYSpeed = 0;

            fullScreen = SKRect.Create(PaintingSize);
        }

		public override void Think(TimeSpan timeSincePreviousCall)
		{
            if (DetectsTouchAt(fullScreen))
            {
                birdYSpeed = -VerticalSpeed;
            }

            birdY += birdYSpeed * timeSincePreviousCall.Milliseconds;
            birdYSpeed += VerticalSpeed * timeSincePreviousCall.Milliseconds;
		}

        public override void Paint()
        {
            Erase(SKColors.White);

            Paint(bird, (int)birdX, (int)birdY);
        }
    }
}
