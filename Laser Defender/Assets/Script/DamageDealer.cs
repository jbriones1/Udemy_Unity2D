using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] GameObject hitVFX;
    [SerializeField] float durationOfExplosion = 1f;

    public int GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        GameObject explosion = Instantiate(hitVFX, new Vector3(transform.position.x,transform.position.y,-1), transform.rotation);
        Destroy(explosion, durationOfExplosion);
        Destroy(gameObject);           
    }
}
