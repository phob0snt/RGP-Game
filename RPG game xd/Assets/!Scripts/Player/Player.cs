using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Player : Entity<PlayerConfig>
{
    [Inject] private readonly IAssetLoader _loader;
    public static Transform Transform => _self.transform;
    private static Player _self;
    private Health _health;
    private MeleeComponent _melee;
    private BlockComponent _block;

    private MagicComponent _magic;

    private async void Awake()
    {
        _self = this;
        Initialize(await _loader.LoadAssetAsync<PlayerConfig>("PlayerConfig"));
    }

    public override void Initialize(PlayerConfig config)
    {
        print("pisya");
        base.Initialize(config);
        _health = _components.Find(x => x is Health) as Health;
        _health.Initialize(config.HP);

        _melee = _components.Find(x => x is MeleeComponent) as MeleeComponent;
        _block = _components.Find(x => x is BlockComponent) as BlockComponent;
        _magic = _components.Find(x => x is MagicComponent) as MagicComponent;
    }

    public async Task Attack()
    {
        await _melee.Attack();
    }

    public void Block()
    {
        _block.Block();
    }

    public async Task CastSpell(MagicSpell _spell)
    {
        await _magic.CastSpell(_spell, gameObject.transform);
    }
}
