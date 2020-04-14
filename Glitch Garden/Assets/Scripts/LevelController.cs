using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;
    [SerializeField] int attackersAlive;
    [SerializeField] float winDelay = 5;
    bool levelTimerUp = false;
    
    private void Start()
    {
        winText.SetActive(false);
        loseText.SetActive(false);
    }

    public void AddEnemy()
    {
        attackersAlive++;
    }

    public void RemoveEnemy()
    {
        attackersAlive--;
        if(attackersAlive <= 0 && levelTimerUp)
        {
            StartCoroutine(LevelWin());
        }
    }
    
    public void TimerUp()
    {
        levelTimerUp = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();
        foreach (AttackerSpawner spawner in spawnerArray)
        {
            spawner.EndSpawn();
        }
    }

    IEnumerator LevelWin()
    {
        if (winText != null)
        {
            winText.SetActive(true);
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(winDelay);
            FindObjectOfType<LevelLoader>().LoadNextScene();
        }
    }

    public void LevelLose()
    {
        if (loseText != null)
        {
            loseText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
