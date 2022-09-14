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
        }
        else
        {
            animations.SetActive(true);
        }

        if(record.write == true)
        {
            gesture.SetActive(false);
        }
        else if (record.write == false && record.gestureNumber > 0)
        {
            gesture.SetActive(true);
        }
    }
}
