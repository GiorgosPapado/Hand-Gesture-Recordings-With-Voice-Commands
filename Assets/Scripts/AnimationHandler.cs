using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

public class AnimationHandler : MonoBehaviour
{
    public GameObject animations;
    public DataPersistenceManager manager;
    public VoiceRecord record;
    public GameObject gesture;

    private void OnEnable()
    {
        record.keywordRecognizer.Start();
    }

    private void OnDisable()
    {
        record.keywordRecognizer.Stop();
    }

    void Update()
    {
        if (manager.Name == "" || record.gestureNumber == 0)
        {
            animations.SetActive(false);
            gesture.SetActive(false);
        }
        else
        {
            animations.SetActive(true);
            gesture.SetActive(true);
        }

        if(record.write || record.delete)
        {
            gesture.SetActive(false);
        }
        else if (record.write == false && record.gestureNumber > 0 && manager.lastFileName != "")
        {
            gesture.SetActive(true);
        }
    }
}
