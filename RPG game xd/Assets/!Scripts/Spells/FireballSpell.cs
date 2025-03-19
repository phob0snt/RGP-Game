using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public GameObject spellPrefab;
    public int damage = 10;

    private ParticleSystem mainParticle;
    private ParticleSystem subParticle;
    private Light light;

    private void Awake()
    {
        mainParticle = GetComponent<ParticleSystem>();
        subParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
        light = transform.GetChild(1).GetComponent<Light>();
    }

    private void Start()
    {
        light.range = 0;
    }

    public void Cast(Vector3 position, Vector3 direction)
    {
        GameObject fireball = Instantiate(spellPrefab, position, Quaternion.identity);
        fireball.GetComponent<Spell>().damage = damage;
        fireball.GetComponent<Rigidbody>().linearVelocity = direction * 10;
    }

    public void StartCast()
    {
        mainParticle.Play();
        subParticle.Play();
        light.range = 10;
    }

    public void StopCast(){
        mainParticle.Stop();
        subParticle.Stop();
        light.range = 0;
    }
}
