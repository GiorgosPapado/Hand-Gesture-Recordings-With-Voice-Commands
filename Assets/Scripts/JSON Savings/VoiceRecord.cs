using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
using System.IO;


[RequireComponent(typeof(AudioSource))]
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
    [HideInInspector]
    public bool delete = false;
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
        actions.Add("delete file", Delete);
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), confidence);
        keywordRecognizer.OnPhraseRecognized += RecognizedWord;
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            var p = PhraseRecognitionSystem.isSupported;
            var l = PhraseRecognitionSystem.Status;
/*            Debug.Log(p);
            Debug.Log(l);
            Debug.Log(confidence);*/
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
        if (test)
        {
            manager.Test(data);
        }
        if (delete)
        {   
            if(manager.lastFileName != "")
            {
                Debug.Log(manager.lastFileName);
                File.Delete(Directory.GetCurrentDirectory() + "/Gestures/" + manager.lastFileName);
                manager.lastFileName = "";
                manager.Name = "";
                if (PlayerStats.Rounds > 0)
                {
                    PlayerStats.Rounds -= 1;
                }
            }
            delete = false;
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
        Debug.Log("Start A new Gesture");
       
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
            if(data.left_hand.Count > 0 || data.right_hand.Count > 0)
            {
                manager.SaveGesture(data);
                gestureNumber += 1;
            }
            sunLight.SetActive(false);
            write = false;
            test = false;
            Debug.Log("Stop Gesture");
            Debug.Log(manager.Name);
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
        Purge();
        sunLight.SetActive(true);
        Debug.Log("Start Testing Gesture");
        test = true;
        write = cancel = false;
    }

    public void Delete()
    {
        test = write = false;
        delete = true;
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

        if (speech.text == "delete file")
        {
            actions["delete file"].Invoke();
        }
    }
}
