public interface IEntity<T> where T : EntityConfig
{
    public void Initialize(T config);
}
