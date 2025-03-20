using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(PlayerController))]
public class PlayerCombatController : MonoBehaviour
{

    private Player _player;
    private PlayerController _controller;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _controller = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        EventManager.AddListener<BlockPressEvent>(Block);
        EventManager.AddListener<SwordAttackPressEvent>((e) => Attack());
        EventManager.AddListener<MagicAttackPressEvent>((e) => Magic());
    }

    private void OnDisable()
    {
        EventManager.AddListener<BlockPressEvent>(Block);
        EventManager.AddListener<SwordAttackPressEvent>((e) => Attack());
        EventManager.AddListener<MagicAttackPressEvent>((e) => Magic());
    }

    public void Block(BlockPressEvent e)
    {
        _controller.IsBlocking = e.IsBlocking;
    }

    public async void Attack()
    {
        if(_controller.IsAttacking)
            return;

        await Task.Yield();
        _controller.IsAttacking = true;
    }

    public async void Magic()
    {
        if(_controller.IsMagic)
            return;
            
        await Task.Yield();
        _controller.IsMagic = true;
    }
}