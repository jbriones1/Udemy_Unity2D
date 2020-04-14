using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    bool spawn = true;
    [SerializeField] float minSpawnDelay = 5f;
    [SerializeField] float maxSpawnDelay = 10f;
    [SerializeField] Attacker[] attackers;

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minSpawnDelay,maxSpawnDelay));
            SpawnAttacker();
        }
    }

    private void SpawnAttacker()
    {
        Spawn(UnityEngine.Random.Range(0,attackers.Length));
    }

    private void Spawn(int index)
    {
        Attacker newAttacker = Instantiate(
            attackers[index], transform.position, transform.rotation)
            as Attacker;
        newAttacker.transform.parent = transform;
    }

    public void EndSpawn()
    {
        spawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
