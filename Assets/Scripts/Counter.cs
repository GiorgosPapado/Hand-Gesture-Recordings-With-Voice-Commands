using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{

    public GameObject leftPanel;
    public GameObject rightPanel;
    public GameObject BothPanel;

    void Update()
    {
        if(!leftPanel.activeInHierarchy && !rightPanel.activeInHierarchy && !BothPanel.activeInHierarchy)
        {
            PlayerStats.Rounds = 0;
        }
    }
}
