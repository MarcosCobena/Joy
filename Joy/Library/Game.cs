using System;
using SkiaSharp;

namespace Joy
{
    public abstract class Game
    {
		SKPoint? lastTouchPoint;
        SKSize paintingSize;

        public SKSize PaintingSize { get => paintingSize; set => paintingSize = value; }

        public SKCanvas NativeCanvas { get; internal set; }

        public Game()
        {
            paintingSize = SKSize.Empty;
        }

        public Game(int width, int height)
        {
            paintingSize = new SKSize(width, height);
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

        public void Paint(JImage image, int x, int y)
        {
            var nativeImage = image.NativeImage;
            var sourceRect = new SKRect(0, 0, nativeImage.Width, nativeImage.Height);
            var canvas = NativeCanvas;
            var scaledWidth = canvas.DeviceClipBounds.Width / paintingSize.Width;
            var scaledHeight = canvas.DeviceClipBounds.Height / paintingSize.Height;
            var destRect = SKRect.Create(x * scaledWidth, y * scaledHeight,
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
