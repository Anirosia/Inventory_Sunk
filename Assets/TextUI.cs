using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public GameObject textDisplay;

    void Update()
    {
        if (InventoryManager._instance.inInteractArea)
        {
            textDisplay.SetActive(true);
        }
        else
        {
            textDisplay.SetActive(false);
        }
    }
}
