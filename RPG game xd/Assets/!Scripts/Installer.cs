using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Installer : MonoInstaller
{
    [SerializeField] private InputActionAsset _inputAsset;
    public override void InstallBindings()
    {
        Debug.Log("DONE");
        Container.Bind<InputActionAsset>().FromInstance(_inputAsset).AsSingle();
        Container.BindInterfacesTo<InputManager>().AsSingle();
    }
}
