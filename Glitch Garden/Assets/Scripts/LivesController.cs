using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var attacker = other.GetComponent<Attacker>();
        var lifeDmg = attacker.GetLifeDmg();
        Destroy(other.gameObject);
        if (attacker)
        {
            FindObjectOfType<LifeDisplay>().LoseLives(lifeDmg);
        }
    }

}
