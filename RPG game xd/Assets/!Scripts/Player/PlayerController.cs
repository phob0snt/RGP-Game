using System;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IPlayerController
{
    [SerializeField] private float _moveSpeed = 4;
    [SerializeField] private float _rotationSpeed = 7;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _crouchSpeed = 2.5f;
    [SerializeField] private float _sensivity = 1;
    [SerializeField] private float _smoothVelocityValue = 0.03f;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private Transform _cameraWalkRoot;
    [SerializeField] private Transform _cameraCrouchRoot;
    [SerializeField] private float _crouchAnimDuration = 0.4f;

    private Player _player;

    [field: SerializeField] public Sword Weapon {get; private set;}
    [field: SerializeField] public Shield Shield {get; private set;}
    [field: SerializeField] public MagicSpell Spell {get; private set;}
     
    
    private float _currentSpeed;

    private bool _isCrouching;
    
    private bool _isJumping;

    private bool _isAttacking;

    private bool _isBlocking;

    private bool _isRunning;

    private bool _isMagic;

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
    [SerializeField] private Animator _animator;

    private Sequence _startCrouchColliderSequence;
    private Sequence _endCrouchColliderSequence;
    
    [SerializeField] private float _jumpForce = 5f;
    private bool _isGrounded;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _player = GetComponent<Player>();

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
        EventManager.AddListener<JumpEvent>((e) => Jump());
        EventManager.AddListener<RunEvent>((e) => Run());
        EventManager.AddListener<BlockPressEvent>(Block);
        EventManager.AddListener<SwordAttackPressEvent>((e) => Attack());
        EventManager.AddListener<MagicAttackPressEvent>((e) => Magic());
    }

    

    private void OnDisable()
    {
        EventManager.RemoveListener<LocomotionEvent>(SetMovementDir);
        EventManager.RemoveListener<MouseMoveEvent>(SetCameraInput);
        EventManager.RemoveListener<CrouchPressEvent>((e) => ChangeCrouchState());
        EventManager.RemoveListener<JumpEvent>((e) => Jump());
        EventManager.RemoveListener<RunEvent>((e) => Run());
        EventManager.RemoveListener<BlockPressEvent>(Block);
        EventManager.RemoveListener<SwordAttackPressEvent>((e) => Attack());
        EventManager.RemoveListener<MagicAttackPressEvent>((e) => Magic());
    }

    private void SetMovementDir(LocomotionEvent e) => _locomotionInput = e.LocomotionInput;
    private void SetCameraInput(MouseMoveEvent e) => _cameraInput = e.MouseInput;

    private void ConfigureStateMachine()
    {
        _stateMachine = new PlayerStateMachine();

        var locomotionState = new LocomotionState(this, _animator);
        var crouchState = new CrouchState(this, _animator);
        var idleState = new IdleState(this, _animator);
        var jumpState = new JumpState(this, _animator);
        var AttackState = new AttackState(this, _animator);
        var BlockState = new BlockState(this, _animator);
        var RunState = new RunState(this, _animator);
        var MagicState = new MagicState(this, _animator);

        _stateMachine.AddTransition(locomotionState, crouchState, new FuncPredicate(() => _isCrouching));
        _stateMachine.AddTransition(crouchState, locomotionState, new FuncPredicate(() => !_isCrouching));
        _stateMachine.AddTransition(locomotionState, idleState, new FuncPredicate(() => _locomotionInput == Vector2.zero));
        _stateMachine.AddTransition(idleState, locomotionState, new FuncPredicate(() => _locomotionInput != Vector2.zero));
        _stateMachine.AddAnyTransition(jumpState, new FuncPredicate(() => _isJumping));
        _stateMachine.AddTransition(jumpState, idleState, new FuncPredicate(() => !_isJumping));
        _stateMachine.AddTransition(jumpState, locomotionState, new FuncPredicate(() => _locomotionInput != Vector2.zero));
        _stateMachine.AddAnyTransition(AttackState, new FuncPredicate(() => _isAttacking));
        _stateMachine.AddAnyTransition(BlockState, new FuncPredicate(() => _isBlocking));
        _stateMachine.AddTransition(AttackState, idleState, new FuncPredicate(() => !_isAttacking));
        _stateMachine.AddTransition(BlockState, idleState, new FuncPredicate(() => !_isBlocking));
        _stateMachine.AddAnyTransition(MagicState, new FuncPredicate(() => _isMagic));
        _stateMachine.AddTransition(MagicState, idleState, new FuncPredicate(() => !_isMagic));


        _stateMachine.SetState(idleState);
    }

    private void Update()
    {
        _stateMachine.Update();
        CheckGroundStatus();
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

    public void Run()
    {
        _isRunning = true;
    }

    public void Block(BlockPressEvent e)
    {
        _isBlocking = e.IsBlocking;
    }

    public async void Attack()
    {
        if(_isAttacking)
            return;

        _isAttacking = true;
        await _player.Attack();

        _isAttacking = false;
    }

    public async void Magic()
    {
        if(_isMagic)
            return;

        _isMagic = true;
        await _player.CastSpell();

        _isMagic = false;
        
    }

    private void CheckGroundStatus()
    {
        _isGrounded = _controller.isGrounded;
    }

    public void Jump()
    {
        if (_isGrounded)
        {
            _velocity.y = _jumpForce;
            _isJumping = true;
        }
    }
    
    
    public void Move(float Multiplier = 1f)
    {
        _velocity.x = 0;
        _velocity.z = 0;
        CalculateGravity();
        Vector2 _smoothInputVector = Vector2.zero;
        _smoothDir = Vector2.SmoothDamp(_smoothDir, CanMove ? _locomotionInput : Vector2.zero, ref _smoothInputVector, _smoothVelocityValue);
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        Vector3 moveDir = cameraRight * _smoothDir.x + cameraForward * _smoothDir.y;
        moveDir.Normalize();

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }

        Vector3 forwardMovement = transform.forward * _currentSpeed * moveDir.magnitude * Multiplier;
        _velocity += forwardMovement;
        
        

        _controller.Move(Time.deltaTime * _velocity);
        
        if (_isJumping && _controller.isGrounded)
        {
            _isJumping = false;
        }
    }

    private void CalculateGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // небольшое значение для удержания игрока на земле
        }
        else
        {
            _velocity.y += _gravity * Time.deltaTime;
        }
    }

    public void EnableCrouch()
    {
    }

    public void DisableCrouch()
    {
    }
}
