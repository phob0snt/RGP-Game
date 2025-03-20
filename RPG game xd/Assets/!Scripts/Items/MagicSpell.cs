using UnityEngine;

public class MagicSpell : BaseItem, IWeapon
{
    [SerializeField] private GameObject _spellPrefab;
    [SerializeField] private ParticleSystem _mainParticle;
    [SerializeField] private ParticleSystem _subParticle;
    [SerializeField] private Light _light;
    [SerializeField] private int _damage = 10;

    private void Awake()
    {
        _light.range = 0;
    }

    public void Attack()
    {
        Debug.Log("Magic Attack");
    }

    public void Cast(Transform root)
    {
        GameObject fireball = Instantiate(_spellPrefab, root.position, Quaternion.identity);
        fireball.GetComponent<Spell>().SetDamage(_damage);
        fireball.GetComponent<Rigidbody>().linearVelocity = (root.right + (-root.forward * 0.75f) + (-root.up * 0.1f)) * 10;
    }

    public void StartCast()
    {
        _mainParticle.Play();
        _subParticle.Play();
        _light.range = 10;
    }

    public void StopCast(){
        _mainParticle.Stop();
        _subParticle.Stop();
        _light.range = 0;
    }
}