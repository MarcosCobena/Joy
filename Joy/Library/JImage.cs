using SkiaSharp;
using Xamarin.Forms;

namespace Joy
{
    public class JImage
    {
        public SKImage NativeImage { get; private set; }

        private JImage()
        { }

        internal static JImage Load(string filename)
        {
            var assetsAndResourcesService = DependencyService.Get<IAssetsAndResourcesService>();
            var imageStream = assetsAndResourcesService.Load(filename);
            var bitmap = SKBitmap.Decode(imageStream);
            var nativeImage = SKImage.FromBitmap(bitmap);

            var image = new JImage
            {
                NativeImage = nativeImage
            };

            return image;
        }
    }
}
