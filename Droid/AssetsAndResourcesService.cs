using System.IO;
using Android.App;
using Joy.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AssetsAndResourcesService))]
namespace Joy.Droid
{
    public class AssetsAndResourcesService : IAssetsAndResourcesService
    {
        public Stream Load(string filename)
        {
            var assetManager = Application.Context.Assets;
            var stream = assetManager.Open(filename);

            return stream;
        }
    }
}
