using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.XR;
using Zenject;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _crouchSpeed = 2.5f;
    [SerializeField] private float _sensivity = 1;
    [SerializeField] private float _smoothVelocityValue = 0.03f;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private Transform _cameraWalkRoot;
    [SerializeField] private Transform _cameraCrouchRoot;
    [SerializeField] private float _crouchAnimDuration = 0.4f;

    private float _currentSpeed;

    private bool _isCrouching;

    public bool CanMove { get; set; } = true;
    public bool CanLook { get; set; } = true;

    private PlayerStateMachine _stateMachine;

    private Vector2 _locomotionInput;
    private Vector2 _cameraInput;
    private Vector2 _smoothDir;

    private float _camYRotation;
    private float _camXRotation;

    private Vector3 _currentRotation;
    private Vector3 _velocity;

    private CharacterController _controller;
    private Animator _animator;

    private Sequence _startCrouchColliderSequence;
    private Sequence _endCrouchColliderSequence;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _currentSpeed = _moveSpeed;

        ConfigureStateMachine();
    }

    private void OnEnable()
    {
        EventManager.AddListener<LocomotionEvent>(SetMovementDir);
        EventManager.AddListener<MouseMoveEvent>(SetCameraInput);
        EventManager.AddListener<CrouchPressEvent>((e) => ChangeCrouchState());
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<LocomotionEvent>(SetMovementDir);
        EventManager.RemoveListener<MouseMoveEvent>(SetCameraInput);
        EventManager.RemoveListener<CrouchPressEvent>((e) => ChangeCrouchState());
    }

    private void SetMovementDir(LocomotionEvent e) => _locomotionInput = e.LocomotionInput;
    private void SetCameraInput(MouseMoveEvent e) => _cameraInput = e.MouseInput;

    private void ConfigureStateMachine()
    {
        _stateMachine = new PlayerStateMachine();

        var locomotionState = new LocomotionState(this, _animator);
        var crouchState = new CrouchState(this, _animator);

        _stateMachine.AddTransition(locomotionState, crouchState, new FuncPredicate(() => _isCrouching));
        _stateMachine.AddTransition(crouchState, locomotionState, new FuncPredicate(() => !_isCrouching));

        _stateMachine.SetState(locomotionState);
    }

    private void Update()
    {
        Look();
        _stateMachine.Update();
    }

    private void ChangeCrouchState()
    {
        //RaycastHit hit = default;
        //if (!_endCrouchColliderSequence.IsPlaying() && !_startCrouchColliderSequence.IsPlaying())
        //{
        //    if (_isCrouching && !Physics.Raycast(transform.position + new Vector3(0, _controller.height, 0), Vector3.up, out hit, 0.5f))
        //        _isCrouching = false;
        //    else if (!_isCrouching)
        //        _isCrouching = true;
        //    else
        //        Debug.Log(hit.collider.gameObject);
        //}
    }

    public void Move()
    {
        _velocity.x = 0;
        _velocity.z = 0;
        CalculateGravity();
        Vector2 _smoothInputVector = Vector2.zero;
        _smoothDir = Vector2.SmoothDamp(_smoothDir, CanMove ? _locomotionInput : Vector2.zero, ref _smoothInputVector, _smoothVelocityValue);
        Vector3 moveDir = new(_smoothDir.x, 0, _smoothDir.y);
        Vector3 tempVelocity = _currentSpeed * moveDir.x * transform.right + _currentSpeed * moveDir.z * transform.forward;
        _velocity += tempVelocity;

        _controller.Move(Time.deltaTime * _velocity);
    }

    private void CalculateGravity()
    {
        if (_controller.isGrounded)
        {
            _velocity.y = _gravity * Time.deltaTime;
        }
        else
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
    }

    private void Look()
    {
        _camYRotation += _cameraInput.x * _sensivity * Time.deltaTime;

        _camXRotation -= _cameraInput.y * _sensivity * Time.deltaTime;
        _camXRotation = Mathf.Clamp(_camXRotation, -90f, 90f);

        _currentRotation = new Vector3(_camXRotation, _camYRotation, 0);

        //_camera.transform.DORotate(_currentRotation, 0.1f);
        //transform.DORotate(new Vector3(0, _camYRotation, 0), 0.1f);
    }

    public void EnableCrouch()
    {
    }

    public void DisableCrouch()
    {
    }
}
