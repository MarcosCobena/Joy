using System;
using SkiaSharp;

namespace Joy
{
    public class HueAndFPSDemo : Game
    {
		TimeSpan accumulatedTime;
		int framesPerSecond;
        int hue;

        public HueAndFPSDemo() : base(32, 32)
        { }

        public override void Load()
        {
            accumulatedTime = TimeSpan.Zero;
            framesPerSecond = 0;
            hue = 0;
        }

		public override void Think(TimeSpan timeSincePreviousCall)
		{
			accumulatedTime += timeSincePreviousCall;
			framesPerSecond++;
			
			if (accumulatedTime >= TimeSpan.FromSeconds(1))
			{
				System.Diagnostics.Debug.WriteLine($"{framesPerSecond} FPS");
				accumulatedTime = TimeSpan.Zero;
				framesPerSecond = 0;
			}
			
			hue = (hue + 1) % 360;
		}

        public override void Paint()
        {
            var color = SKColor.FromHsl(hue, 50, 50);
            Erase(color);
        }
    }
}
