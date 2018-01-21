using System;
using SkiaSharp;

namespace Joy
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Translates <paramref name="location"/> from <paramref name="realSize"/> to <paramref name="virtualSize"/> based
        /// in 0-index.
        /// </summary>
        /// <returns>The virtual.</returns>
        /// <param name="location">Location.</param>
        /// <param name="realSize">Real size.</param>
        /// <param name="virtualSize">Virtual size.</param>
        public static SKPoint ToVirtual(this SKPoint location, SKSize realSize, SKSize virtualSize)
        {
            var virtualX = (int)Math.Floor((location.X / realSize.Width) * virtualSize.Width);
            var virtualY = (int)Math.Floor((location.Y / realSize.Height) * virtualSize.Height);
            var virtualLocation = new SKPoint(virtualX, virtualY);

            return virtualLocation;
        }
    }
}
