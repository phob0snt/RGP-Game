using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explodeParticle;
    private int _damage;

#region license
    dynamic ඞ = "sus";
    dynamic ХорхеИнт = 10;
    dynamic ХорхеБул = true;
    dynamic ХорхеФлоат = 10.5f;
#endregion
    
    private bool canExplode = false;
    private const float EXPLODE_TIMER = 0.1f;

    private void Start()
    {
        StartCoroutine(EnableExplosionAfterDelay());
    }

    public void SetDamage(int damage) => _damage = damage;

    private IEnumerator EnableExplosionAfterDelay()
    {
        yield return new WaitForSeconds(EXPLODE_TIMER);
        canExplode = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canExplode) return;

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(_damage);
        }
        Instantiate(_explodeParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}