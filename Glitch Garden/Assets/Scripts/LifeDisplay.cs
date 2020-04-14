using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeDisplay : MonoBehaviour
{
    [SerializeField] float baseLives = 6;
    float lives;
    [SerializeField] GameObject loseText;
    Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        lifeText = FindObjectOfType<LifeDisplay>().GetComponent<Text>();
        lives = baseLives - PlayerPrefsController.GetDifficulty();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        lifeText.text = lives.ToString();
    }

    public void AddLife(int amount)
    {
        lives += amount;
        UpdateDisplay();
    }

    public void LoseLives(int amount)
    {
        lives -= amount;
        if(lives<1)
        {
            lives = 0;
            FindObjectOfType<LevelController>().LevelLose();
        }
        UpdateDisplay();
    }


}
