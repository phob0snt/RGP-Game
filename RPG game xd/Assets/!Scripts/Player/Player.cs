using System.Threading.Tasks;
using Zenject;

public class Player : Entity<PlayerConfig>
{
    [Inject] private readonly IAssetLoader _loader;
    private Health _health;
    private MeleeComponent _melee;
    private BlockComponent _block;

    private MagicComponent _magic;

    private async void Awake()
    {
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

    // public async Task Block()
    // {
    //     await _block.Block();
    // }

    public async Task CastSpell()
    {
        await _magic.CastSpell();
    }
}
