using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
public class AddressablesAssetLoader : IAssetLoader
{
    public async Task<T> LoadAssetAsync<T>(string path)
    {
        return await Addressables.LoadAssetAsync<T>(path).Task;
    }
}