using System.Collections;
using UnityEngine;

public class DamageableObjectScript : MonoBehaviour
{

    public float Health = 100;
    public float maxHealth = 100;

    public float FireResistance = 100;
    public float MaxFireResistance = 100;
    public ParticleSystem FirePrefab;
    ParticleSystem Fire;

    public bool onFire = false;
    public bool Damageable = true;

    public Transform EffectsBase;

    public string state;

    private void Start()
    {
        if (EffectsBase == null)
            EffectsBase = transform;
        if (FirePrefab == null)
            FirePrefab = Resources.Load<ParticleSystem>("BurningFlame");
    }

    private void Update()
    {
        if (onFire && FireResistance > 0)
        {
            onFire = false;
            Fire.Stop();
        }
    }

    void TakeFireDamage(float Damage)
    {
        if (Fire == null)
        {
            Fire = Instantiate(FirePrefab);
            Fire.Stop();
            Fire.transform.SetParent(EffectsBase);
            Fire.transform.localPosition = Vector3.zero;
            Fire.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Damageable)
        {
            StartCoroutine("DamageImmunity", 3);
            FireResistance -= Damage;
            if (FireResistance > 0)
            {
                Fire.Emit((int)Damage);

            }
            else
            {
                onFire = true;
                StartCoroutine(SetOnFire());
            }
        }
    }

    IEnumerator SetOnFire()
    {
        Fire.Play();
        while (onFire)
        {
            Health -= maxHealth / 10;
            if (Health <= 0)
            {
                this.state = "burnt";
                break;
            }
            yield return new WaitForSeconds(5);
        }
        Fire.Stop();
    }

    IEnumerator DamageImmunity(float Seconds)
    {
        Damageable = false;
        yield return new WaitForSeconds(Seconds);
        Damageable = true;
    }
}
