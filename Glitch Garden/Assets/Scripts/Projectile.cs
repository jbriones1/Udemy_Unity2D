using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float damage = 100f;
    [SerializeField] bool piercing = false;
    [SerializeField] GameObject projectileVFX;
    //[SerializeField] float spinSpeed = 60f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void CheckPierce()
    {
        if (!piercing)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var health = other.GetComponent<Health>();
        var attacker = other.GetComponent<Attacker>();
        var pos = other.transform.position;
        if (attacker && health)
        {
            health.DealDamage(damage);
            CheckPierce();
            ProjectileVFX(pos);
        }

    }

    private void ProjectileVFX(Vector3 obj)
    {
        if(!projectileVFX) { return; }
        GameObject hitVFX = Instantiate(projectileVFX, obj, Quaternion.identity);
    }

}
