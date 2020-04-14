using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLevel : MonoBehaviour
{
    [SerializeField] int blockCount;
    
    public void BlockCounter()
    {
        blockCount++;
    }

    public void BlockDestroyed()
    {
        blockCount--;
        if (blockCount <= 0)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

}
