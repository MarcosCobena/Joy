using System.IO;

namespace Joy
{
    public interface IAssetsAndResourcesService
    {
        Stream Load(string filename);
    }
}
