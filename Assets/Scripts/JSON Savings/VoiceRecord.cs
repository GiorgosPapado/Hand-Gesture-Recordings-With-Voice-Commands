using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;

[RequireComponent (typeof(AudioSource))]
public class VoiceRecord : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;    
    private Dictionary<string, Action> actions = new();
    private FrameData data = new();
    public DataPersistenceManager manager;
    public ConfidenceLevel confidence;
    private bool left = false;
    private bool right = false;
    private bool both = false;
    private bool cancel = false;
    public GameObject sunLight;

        private void Start()
    {
        //my actions
        actions.Add("begin", Record);
        actions.Add("stop", Stop);
        actions.Add("left", LeftHanded);
        actions.Add("right", RightHanded);
        actions.Add("cancel", Cancel);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(),confidence);        
        keywordRecognizer.OnPhraseRecognized += RecognizedWord;
        keywordRecognizer.Start();
        Debug.Log(keywordRecognizer.IsRunning.ToString());

            foreach (var device in Microphone.devices)
            {
                Debug.Log("Name: " + device);
               var p =  PhraseRecognitionSystem.isSupported;
                var l = PhraseRecognitionSystem.Status;
                Debug.Log(p);
                Debug.Log(l);
                Debug.Log(confidence);
                
            }
    }
      private void Update()
        {
            if (right)
            {
                manager.Calculate(data);
                data.left_hand.Clear();
            }
            else if (left)
            {
                manager.Calculate(data);
                data.right_hand.Clear();
            }
            else if (both)
            {
                manager.Calculate(data);
            }
        if (cancel)
        {
            Purge();
            cancel = false;
        }
        }

    private void RecognizedWord(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void LeftHanded()
    {
        Purge();
        if (left == false && right == false && both == false)
        {
            PlayerStats.Rounds += 1;
            Debug.Log("Start Left Hand - Gesture");
        }
        sunLight.SetActive(true);
        both = right = cancel = false;
        left = true;
    }
    private void RightHanded()
    {
        Purge();
        if (left == false && right == false && both == false)
        {
            PlayerStats.Rounds += 1;
            Debug.Log("Start Right Hand - Gesture");
        }
        sunLight.SetActive(true);
        both = left = cancel = false;
        right = true;    
    }

    private void Record()
    {
        Purge();
        if (left == false && right == false && both == false)
        {
            PlayerStats.Rounds += 1;
            Debug.Log("Start Both Hands - Gesture");
        }
        sunLight.SetActive(true);
        right = left = cancel = false;
        both = true;
    }

    private void Stop()
    {
        if (right == left && left == both)
        { 
        
        }
        else
        {
            if (PlayerStats.Rounds > 10)
            {
                PlayerStats.Rounds = 0;
            }
            manager.SaveGesture(data);
            sunLight.SetActive(false);
            both = left = right = false;
            Debug.Log("Stop Gesture");
        }
    }

    private void Cancel()
    {
        cancel = true;
        both = left = right = false;
        Debug.Log("Gestured Cancelled");

        if (PlayerStats.Rounds > 0)
        {
            PlayerStats.Rounds -= 1;
        }
        Purge();
    }

    private void Purge()
    {
        data.left_hand.Clear();
        data.right_hand.Clear();
        data.hmd.pos.Clear();
        data.hmd.rotation.Clear();
        data.left_hand_global.pos.Clear();
        data.left_hand_global.rotation.Clear();
        data.right_hand_global.pos.Clear();
        data.right_hand_global.rotation.Clear();
        data.timestamps.Clear();
        sunLight.SetActive(false);
    }
}
