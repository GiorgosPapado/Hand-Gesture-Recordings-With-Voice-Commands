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
            if (left)
            {
                manager.Calculate(data);
                data.right_hand.Clear();
            }
            if (both)
            {
                manager.Calculate(data);
            }
        if (cancel)
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
        }
        }

    private void RecognizedWord(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void LeftHanded()
    {
        if(left == false && right == false && both == false)
            PlayerStats.Rounds += 1;

        cancel = false;
        left = true;
            Debug.Log("Start Left Hand - Gesture");
    }
    private void RightHanded()
    {
        if (left == false && right == false && both == false)
            PlayerStats.Rounds += 1;

        cancel = false;
        right = true;
            Debug.Log("Start Right Hand - Gesture");
    }

    private void Record()
    {
        if (left == false && right == false && both == false)
            PlayerStats.Rounds += 1;

        cancel = false;
        both = true;
            Debug.Log("Start Both Hands - Gesture");
    }

    private void Stop()
    {
        if(PlayerStats.Rounds > 10)
        {
            PlayerStats.Rounds = 0;
        }
        manager.SaveGesture(data);
        data.left_hand.Clear();
        data.right_hand.Clear();
        data.hmd.pos.Clear();
        data.hmd.rotation.Clear();
        data.left_hand_global.pos.Clear();
        data.left_hand_global.rotation.Clear();
        data.right_hand_global.pos.Clear();
        data.right_hand_global.rotation.Clear();
        data.timestamps.Clear();
        left = false;
        right = false;
        both = false;
        Debug.Log("Stop Gesture");      
    }

    private void Cancel()
    {
        cancel = true;
        left = false;
        right = false;
        both = false;
        Debug.Log("Gestured Cancelled");
    }
}
