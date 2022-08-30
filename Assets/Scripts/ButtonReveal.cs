using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReveal : MonoBehaviour
{

    public GameObject Panel;

    public void ShowButton()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
        else
        {
            Panel.SetActive(true);
            //Time.timeScale = 1f;
        }
    }
}