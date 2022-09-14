using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
using System.IO;

[RequireComponent (typeof(AudioSource))]
public class VoiceRecord : MonoBehaviour
{
    [HideInInspector]
    public KeywordRecognizer keywordRecognizer;    
    public Dictionary<string, Action> actions = new();

    private FrameData data = new();
    public DataPersistenceManager manager;
    
    public ConfidenceLevel confidence;
    //private bool left = false;
    //private bool right = false;
    [HideInInspector]
    public bool write = false;

    private bool cancel = false;
    private bool test = false;
    public ToggleRecorder toggleColor;

    [HideInInspector]
    public int gestureNumber = 0;
    public GameObject sunLight;

    private void Start()
    {
        //my actions
        actions.Add("begin", Record);
        actions.Add("stop", Stop);
        actions.Add("cancel", Cancel);
        actions.Add("test", Test);
        actions.Add("delete", Delete);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(),confidence);
        keywordRecognizer.OnPhraseRecognized += RecognizedWord;
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
            if (write)
            {
                manager.Calculate(data);
            }
            if (cancel)
            {
                Purge();
                cancel = false;
            }

            //if (left || right || both || test)
            if(write || test)
            {
                toggleColor.RecordingON();
            }
            else
            {
                toggleColor.RecordingOff();
            }

        }

    public void Record()
    {
        Purge();
        PlayerStats.Rounds += 1;
        Debug.Log("Start Both Hands - Gesture");
       
        sunLight.SetActive(true);
        test = cancel = false;
        write = true;

    }

    public void Stop()
    {
        //if (right == left && left == both && test == false)
        //{ 
        
        //}
        //else
        if(write || test)
        {
            if (PlayerStats.Rounds > 10)
            {
                PlayerStats.Rounds = 0;
            }
            if(data.left_hand.Count > 0 || data.right_hand.Count > 0)
            {
                manager.SaveGesture(data);
                gestureNumber += 1;
            }
            sunLight.SetActive(false);
            write = false;
            test = false;
            Debug.Log("Stop Gesture");
        }
    }

    public void Cancel()
    {
        cancel = true;
        test = write = false;
        Debug.Log("Gestured Cancelled");

        if (PlayerStats.Rounds > 0)
        {
            PlayerStats.Rounds -= 1;
        }
        Purge();
    }

    public void Purge()
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

    public void Test()
    {
        manager.Test(data);
        sunLight.SetActive(true);
        Debug.Log("Start Testing Gesture");
        test = true;
        write = cancel = false;
    }

    public void Delete()
    {
        Debug.Log("LETS DELETE IT");
    }

    private void KeyRecognition()
    {
/*        actions.Add("begin", Record);
        actions.Add("stop", Stop);
        actions.Add("left", LeftHanded);
        actions.Add("right", RightHanded);
        actions.Add("cancel", Cancel);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), confidence);
        keywordRecognizer.OnPhraseRecognized += RecognizedWord;
        keywordRecognizer.Start();
        Debug.Log(keywordRecognizer.IsRunning.ToString());*/

        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            var p = PhraseRecognitionSystem.isSupported;
            var l = PhraseRecognitionSystem.Status;
            Debug.Log(p);
            Debug.Log(l);
            //Debug.Log(confidence);
        }
    }

    public void RecognizedWord(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        if (speech.text == "begin")
        {
            actions["begin"].Invoke();
        }

        if (speech.text == "stop")
        {
            actions["stop"].Invoke();
        }

        if (speech.text == "cancel")
        {
            actions["cancel"].Invoke();
        }

        if (speech.text == "test")
        {
            actions["test"].Invoke();
        }

        if (speech.text == "delete")
        {
            actions["delete"].Invoke();
        }
    }
}
