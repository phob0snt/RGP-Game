using System.Threading.Tasks;

public interface IAsyncInitializable
{
    public Task InitializeAsync();
}