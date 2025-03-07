using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : IInputManager, IInitializable
{
    private readonly InputActionAsset _inputActionAsset;

    private InputAction _locomotion;
    private InputAction _crouch;
    private InputAction _cameraRotation;
    private InputAction _swordAttack;
    private InputAction _magicAttack;


    public InputManager(InputActionAsset inputActions)
    {
        _inputActionAsset = inputActions;
    }

    public void Initialize()
    {
        _locomotion = _inputActionAsset.FindAction("Locomotion");
        _crouch = _inputActionAsset.FindAction("Crouch");
        _cameraRotation = _inputActionAsset.FindAction("Camera");
        _swordAttack = _inputActionAsset.FindAction("SwordAttack");
        _magicAttack = _inputActionAsset.FindAction("MagicAttack");

        _locomotion.Enable();
        _crouch.Enable();
        _cameraRotation.Enable();
        _swordAttack.Enable();
        _magicAttack.Enable();

        _locomotion.performed += ctx => EventManager.Broadcast(new LocomotionEvent() { LocomotionInput = ctx.ReadValue<Vector2>() });
        _locomotion.canceled += ctx => EventManager.Broadcast(new LocomotionEvent() { LocomotionInput = ctx.ReadValue<Vector2>() });
        _cameraRotation.performed += ctx => EventManager.Broadcast(new MouseMoveEvent() { MouseInput = ctx.ReadValue<Vector2>() });
        _cameraRotation.canceled += ctx => EventManager.Broadcast(new MouseMoveEvent() { MouseInput = ctx.ReadValue<Vector2>() });
        _crouch.performed += PressCrouch;
        _crouch.canceled += PressCrouch;
        _swordAttack.performed += PressSwordAttack;
        _magicAttack.performed += PressMagicAttack;
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
