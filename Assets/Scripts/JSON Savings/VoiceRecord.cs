using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.Android;
using System;
using System.Linq;
using Facebook.WitAi.Lib;
using System.Timers;
using UnityEngine.Audio;

[RequireComponent (typeof(AudioSource))]
public class VoiceRecord : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;    
    private Dictionary<string, Action> actions = new();
    private FrameData data = new();
    private DynamicGesture gesture = new();
    public DataPersistenceManager manager;
    public ConfidenceLevel confidence;
    private bool left = false;
    private bool write = false;
    private bool both = false;
    private bool counter = false;


/*    AudioSource _audioSource;
    //Microphone Input
    public bool _useMicrophone;
    public AudioClip _audioClip;
    public string _selectedDevice;
    public AudioMixerGroup _mixerGroupMicrophone, _mixerGroupMaster;
 */
        private void Start()
    {
       
        /*        //audio initialization
                _audioSource = GetComponent<AudioSource>();


                //microphone input
                if (_useMicrophone)
                {
                    if(Microphone.devices.Length > 0)
                    {
                        _selectedDevice = Microphone.devices[0].ToString();
                        _audioSource.outputAudioMixerGroup = _mixerGroupMicrophone;
                        _audioSource.clip = Microphone.Start(_selectedDevice, true, 10, AudioSettings.outputSampleRate); ;
                    }
                    else
                    {
                        _audioSource.outputAudioMixerGroup = _mixerGroupMaster;
                        _useMicrophone = false;
                    }
                }
                else
                {
                    _audioSource.clip = _audioClip;
                }
                _audioSource.Play();*/

        //my actions
        actions.Add("begin", Record);
        actions.Add("stop", Stop);
        //actions.Add("test", Record);
        //actions.Add("stop", Stop);
        actions.Add("left", LeftHanded);
        actions.Add("write", RightHanded);
        //actions.Add("start", ()=> { });
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(),confidence);        
        keywordRecognizer.OnPhraseRecognized += RecognizedWord;
        keywordRecognizer.Start();
        Debug.Log(keywordRecognizer.IsRunning.ToString());
        //StartCoroutine("MyEvent");
        {
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
    }
      private void Update()
        {
            if (write)
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
        }

/*      private IEnumerator MyEvent()
    {
        while (true)
        {
                if (write)
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
                yield return new WaitForSeconds(0.1f);
        }
    }*/


    private void RecognizedWord(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void LeftHanded()
    {
        if (counter == false)
        {
            counter = true;
        }
        else
        {
            return;
        }
        PlayerStats.Rounds += 1;
        left = true;
        //gesture.frameData.Clear();
            //gesture.frameData.right_hand.Clear();
            Debug.Log("Start Left Hand - Gesture");
            //manager.Calculate(data);
            //data.right_hand.Clear();
    }
    private void RightHanded()
    {
        if (counter == false)
        {
            counter = true;
        }
        else
        {
            return;
        }
        PlayerStats.Rounds += 1;
        write = true;
            //gesture.frameData.left_hand.Clear();
            Debug.Log("Start Right Hand - Gesture");
            //manager.Calculate(data);
            //data.left_hand.Clear();
    }

    private void Record()
    {
        if(counter == false)
        {
            counter = true;
        }
        else
        {
            return;
        }
            PlayerStats.Rounds += 1;
            both = true;
            Debug.Log("Start Both Hands - Gesture");
            //manager.Calculate(data);
    }

    private void Stop()
    {
        if(PlayerStats.Rounds > 10)
        {
            PlayerStats.Rounds = 0;
        }
        //DataPersistenceManager manager = gameObject.GetComponent(typeof(DataPersistenceManager)) as DataPersistenceManager; 
        manager.SaveGesture(data);
        data.left_hand.Clear();
        data.right_hand.Clear();
        //left = right == false;
        left = false;
        write = false;
        both = false;
        counter = false;
        Debug.Log("Stop Gesture");      
    }

    public void Loaded()
    {
        //manager.LoadGesture();
    }
}
