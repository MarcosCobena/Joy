using System;
using SkiaSharp;

namespace Joy
{
    public abstract class Game
    {
		SKPoint? lastTouchPoint;
        float width;
        float height;

        public SKCanvas NativeCanvas { get; internal set; }

        public float Width => width;

        public float Height => height;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Joy.Game"/> class. <paramref name="width"/> and 
        /// <paramref name="height"/> fix the final resolution, stretching in to fit it.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public Game(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public abstract void Load();

		public abstract void Think(TimeSpan timeSincePreviousCall);

        public abstract void Paint();

        public bool DetectsTouchAt(SKRect area)
        {
            if (lastTouchPoint.HasValue && area.Contains(lastTouchPoint.Value))
            {
                return true;
            }

            return false;
        }

        public void Erase(SKColor color)
        {
            NativeCanvas.Clear(color);
        }

        public JImage LoadImage(string filename)
        {
            var image = JImage.Load(filename);

            return image;
        }

        public void Paint(JImage image, float x, float y)
        {
            var nativeImage = image.NativeImage;
            var sourceRect = new SKRect(0, 0, nativeImage.Width, nativeImage.Height);
            var canvas = NativeCanvas;
            var scaledWidth = canvas.DeviceClipBounds.Width / width;
            var scaledHeight = canvas.DeviceClipBounds.Height / height;
            var destRect = SKRect.Create((int)x * scaledWidth, (int)y * scaledHeight,
                                         nativeImage.Width * scaledWidth, nativeImage.Height * scaledHeight);
            canvas.DrawImage(nativeImage, sourceRect, destRect);
        }

        internal void ClearLastTouchPoint()
        {
            lastTouchPoint = null;
        }

        internal void PromoteTouchAt(SKPoint virtualPoint)
        {
            lastTouchPoint = virtualPoint;
        }
    }
}
