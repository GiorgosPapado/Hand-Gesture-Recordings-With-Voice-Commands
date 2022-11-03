using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class JSONRead : MonoBehaviour 
{
    private FileDataHandler dataHandler;
    public OVRCustomSkeleton leftSkeleton;
    public OVRCustomSkeleton rightSkeleton;
    public string jsonFile;
    //public string jsonFile = "Left Hand03739eWednesday, 27 July 2022.json";

    private List<TrackingData> left = new();
    private List<TrackingData> right = new();
    private List<float> hour = new();
    private FrameData frameData;
    private DataPersistenceManager manager;

    private Coroutine coroutine;
    private void Start()
    {
        //coroutine = StartCoroutine(nameof(MyJoints));
        //StartCoroutine(nameof(MyJoints));    
}

    private void LoadJSONFile()
    {

        //manager = GetComponent<DataPersistenceManager>();
        dataHandler = new FileDataHandler(Directory.GetCurrentDirectory() + "/build/Gestures", jsonFile);
        frameData = dataHandler.Load().frameData;
        left = frameData.left_hand;
        right = frameData.right_hand;
        hour = frameData.timestamps;
    }

    private void Awake()
    {
        LoadJSONFile();
    }

    private (Vector3 pos, Quaternion rot) TransformToHMDSpace(Vector3 pos, Quaternion rot, Vector3 HMDPos, Quaternion HMDRot)
    {
        Vector3 tpos = Quaternion.Inverse(HMDRot)*(pos - HMDPos);
        Quaternion trot = Quaternion.Inverse(HMDRot) * rot;        
        return (tpos, trot);
    }



    private IEnumerator MyJoints()
    {
        while (true)
        {
            int frameCount = Math.Max(left.Count, right.Count);
            for (int j = 0; j < frameCount; j++)
            {
                for (int i = 0; i < (int)leftSkeleton.GetCurrentEndBoneId(); i++)
                {
                    if (left.Count > 0)
                    {
                        leftSkeleton.CustomBones[i].transform.localPosition = left[j].pos[i];
                        leftSkeleton.CustomBones[i].transform.localRotation = left[j].rotation[i];
                    }
                    if (right.Count > 0)
                    {
                        rightSkeleton.CustomBones[i].transform.localPosition = right[j].pos[i];
                        rightSkeleton.CustomBones[i].transform.localRotation = right[j].rotation[i];
                    }
                }
                if (left.Count > 0)
                {
                    var tform = TransformToHMDSpace(frameData.left_hand_global.pos[j], frameData.left_hand_global.rotation[j], frameData.hmd.pos[0], frameData.hmd.rotation[0]);
                    leftSkeleton.transform.localPosition = tform.pos;
                    leftSkeleton.transform.localRotation = tform.rot;
                }
                if(right.Count > 0)
                {
                    var tform = TransformToHMDSpace(frameData.right_hand_global.pos[j], frameData.right_hand_global.rotation[j], frameData.hmd.pos[0], frameData.hmd.rotation[0]);
                    rightSkeleton.transform.localPosition = tform.pos;
                    rightSkeleton.transform.localRotation = tform.rot;
                }

                if (left.Count < 1)
                        {
                    leftSkeleton.gameObject.SetActive(false);
                        }
                    if(right.Count < 1)
                        {
                    rightSkeleton.gameObject.SetActive(false);
                         }
                if (j == 0)
                {
                    yield return new WaitForSeconds(Time.timeScale);
                }
                else
                {
                    yield return new WaitForSeconds((hour[j] - hour[j - 1]));
                }
            }
        } 
    }

    /*    public void OnFileLoaded()
        {
            //manager = GetComponent<DataPersistenceManager>();
            dataHandler = new FileDataHandler(Directory.GetCurrentDirectory() + "/build/Gestures", jsonFile);
            frameData = dataHandler.Load().frameData;
            left = frameData.left_hand;
            right = frameData.right_hand;
            hour = frameData.timestamps;
        }

        private void OnEnable()
        {
            OnFileLoaded();
        }*/

    private void OnEnable()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(nameof(MyJoints));
        }        
    }
    private void OnDisable()
    {
        StopCoroutine(coroutine);
        coroutine = null;
    }

    public void Refresh()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        LoadJSONFile();
        coroutine = StartCoroutine(nameof(MyJoints));
    }
}
