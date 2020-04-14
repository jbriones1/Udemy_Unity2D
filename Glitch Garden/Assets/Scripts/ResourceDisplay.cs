using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] int resources = 100;
    Text resourceText;

    // Start is called before the first frame update
    void Start()
    {
        resourceText = GetComponent<Text>();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        resourceText.text = resources.ToString();
    }

    public bool SufficientResources(int amount)
    {
        return resources >= amount;
    }

    public void AddResource(int amount)
    {
        resources += amount;
        UpdateDisplay();
    }

    public void SpendResource(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            UpdateDisplay();
        }
    }
}
