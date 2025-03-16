public class Player : Entity<PlayerConfig>
{
    private Health _health;
    public override void Initialize(PlayerConfig config)
    {
        base.Initialize(config);
        _health = _components.Find(x => x is Health) as Health;
        _health.Initialize(config.HP);
    }
}
