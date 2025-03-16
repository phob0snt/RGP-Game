using System.Threading.Tasks;

public interface IAssetLoader
{
    public Task<T> LoadAssetAsync<T>(string path);
}
