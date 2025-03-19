using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<AsyncInitializer>().AsSingle();
        Container.BindInterfacesTo<AddressablesAssetLoader>().AsSingle();
        //Container.BindInterfacesTo<EnemyHandler>().AsSingle();
        //Container.BindInterfacesTo<EnemyFactory>().AsSingle();
        Container.BindInterfacesTo<InputManager>().AsSingle();
    }
}
