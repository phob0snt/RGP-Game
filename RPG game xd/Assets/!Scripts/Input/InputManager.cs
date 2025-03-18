using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : IInputManager, IAsyncInitializable
{
    private readonly IAssetLoader _loader;
    private InputActionAsset _inputActionAsset;

    private InputAction _locomotion;
    private InputAction _crouch;
    private InputAction _cameraRotation;
    private InputAction _swordAttack;
    private InputAction _magicAttack;
    private InputAction _jump;
    private InputAction _block;
    private InputAction _run;


    public InputManager(IAssetLoader loader)
    {
        _loader = loader;
    }

    public async Task InitializeAsync()
    {
        _inputActionAsset = await _loader.LoadAssetAsync<InputActionAsset>("InputActionAsset");
        _locomotion = _inputActionAsset.FindAction("Locomotion");
        _crouch = _inputActionAsset.FindAction("Crouch");
        _cameraRotation = _inputActionAsset.FindAction("Camera");
        _swordAttack = _inputActionAsset.FindAction("SwordAttack");
        _magicAttack = _inputActionAsset.FindAction("MagicAttack");
        _jump = _inputActionAsset.FindAction("Jump");
        _block = _inputActionAsset.FindAction("Block");
        _run = _inputActionAsset.FindAction("Run");

        _inputActionAsset.Enable();

        _locomotion.performed += ctx => EventManager.Broadcast(new LocomotionEvent() { LocomotionInput = ctx.ReadValue<Vector2>() });
        _locomotion.canceled += ctx => EventManager.Broadcast(new LocomotionEvent() { LocomotionInput = ctx.ReadValue<Vector2>() });
        _cameraRotation.performed += ctx => EventManager.Broadcast(new MouseMoveEvent() { MouseInput = ctx.ReadValue<Vector2>() });
        _cameraRotation.canceled += ctx => EventManager.Broadcast(new MouseMoveEvent() { MouseInput = ctx.ReadValue<Vector2>() });
        _crouch.performed += PressCrouch;
        _crouch.canceled += PressCrouch;
        _swordAttack.performed += PressSwordAttack;
        _magicAttack.performed += PressMagicAttack;
        _jump.performed += PressJump;
        _block.performed += PressBlock;
        _block.canceled += PressBlock;
        _run.performed += PressRun;
    }

    private void PressRun(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            EventManager.Broadcast(Events.RunEvent);
    }

    private void PressBlock(InputAction.CallbackContext context)
    {
        EventManager.Broadcast(new BlockPressEvent(){IsBlocking = context.ReadValueAsButton()});
    }
    private void PressJump(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            EventManager.Broadcast(Events.JumpEvent);
    }

    private void PressCrouch(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            EventManager.Broadcast(Events.CrouchPressEvent);
    }

    private void PressSwordAttack(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            EventManager.Broadcast(Events.SwordAttackPressEvent);
    }

    private void PressMagicAttack(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            EventManager.Broadcast(Events.MagicAttackPressEvent);
    }
}
