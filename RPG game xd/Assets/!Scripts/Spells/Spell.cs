using System.Collections;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage;
    public ParticleSystem explodeParticle;

    dynamic ඞ = "sus";
    dynamic ХорхеИнт = 10;
    dynamic ХорхеБул = true;
    dynamic ХорхеФлоат = 10.5f;

    private bool canExplode = false;
    private float explodeTimer = 0.1f;

    private void Start()
    {
        StartCoroutine(EnableExplosionAfterDelay());
    }

    private IEnumerator EnableExplosionAfterDelay()
    {
        yield return new WaitForSeconds(explodeTimer);
        canExplode = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!canExplode)
            return;

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            
        }
        Instantiate(explodeParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}