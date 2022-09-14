using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public enum RECORD_TYPE
{
    Both,
    Left,
    Right
}

public class DataPersistenceManager : MonoBehaviour
{
    private FileDataHandler dataHandler;
    //public static DataPersistenceManager Instance { get; private set; }
    private DynamicGesture gesture = new DynamicGesture();
    public OVRSkeleton leftHandSkeleton;
    public OVRSkeleton rightHandSkeleton;
    public Transform hmd;

    public GameObject button;
    public int ClassID;

    //bones try
    public Transform leftHandAnchor;
    public Transform rightHandAnchor;
    public RECORD_TYPE recordType = RECORD_TYPE.Both;
   
    public string Name;

    private void Awake()
    {
        Debug.Log(Directory.GetCurrentDirectory() + "/Gestures");
        /*  
           Debug.Log(Application.persistentDataPath);
           if (Instance != null)
          {
              Debug.Log("George: Found more than one Data Manager in the scene.");
          }
          Instance = this;
       dataHandler = new FileDataHandler(Application.persistentDataPath, "JsonFile");
          left = dataHandler.Load().frameData.left_hand;
          right = dataHandler.Load().frameData.right_hand;      */
    }

    public void SaveGesture(FrameData data)
    {
        gesture.class_name = button.name;
        gesture.class_ID = ClassID;
        gesture.frameData = data;
        dataHandler.Save(gesture);
    }

    private void CalculateInternal(FrameData data, string filename)
    {        
        TrackingData leftTrackData = new TrackingData();
        TrackingData rightTrackData = new TrackingData();       
        
        dataHandler = new FileDataHandler(Directory.GetCurrentDirectory() + "/Gestures", filename);
        if ((leftHandSkeleton.Bones.Count > 0) && (recordType == RECORD_TYPE.Both || recordType == RECORD_TYPE.Left))
        {
            for (int i = (int)leftHandSkeleton.GetCurrentStartBoneId(); i < (int)leftHandSkeleton.GetCurrentEndBoneId(); i++)
            {
                leftTrackData.pos.Add(leftHandSkeleton.Bones[i].Transform.localPosition);
                leftTrackData.rotation.Add(leftHandSkeleton.Bones[i].Transform.localRotation);
            }
            data.left_hand.Add(leftTrackData);
            data.left_hand_global.pos.Add(leftHandAnchor.localPosition);
            data.left_hand_global.rotation.Add(leftHandAnchor.localRotation);
        }

        if ((rightHandSkeleton.Bones.Count > 0) && (recordType == RECORD_TYPE.Both || recordType == RECORD_TYPE.Right))
        {
            for (int j = (int)rightHandSkeleton.GetCurrentStartBoneId(); j < (int)rightHandSkeleton.GetCurrentEndBoneId(); j++)
            {
                rightTrackData.pos.Add(rightHandSkeleton.Bones[j].Transform.localPosition);
                rightTrackData.rotation.Add(rightHandSkeleton.Bones[j].Transform.localRotation);
            }
            data.right_hand.Add(rightTrackData);
            data.right_hand_global.pos.Add(rightHandAnchor.localPosition);
            data.right_hand_global.rotation.Add(rightHandAnchor.localRotation);
        }

        if (leftHandSkeleton.Bones.Count > 0 || rightHandSkeleton.Bones.Count > 0)
        {
            data.timestamps.Add(Time.time);
            data.hmd.pos.Add(hmd.localPosition);
            data.hmd.rotation.Add(hmd.localRotation);
        }
    }

    public void Calculate(FrameData data)
    {
        Guid fileName = Guid.NewGuid();
        string tt = DateTime.Now.ToString("dd MMMM HH mm");
        Name = button.name + " " + ClassID + " " + fileName.ToString().Substring(0, 5) + " " + tt + " " + PlayerStats.Rounds.ToString() + ".json";
        CalculateInternal(data, Name);
    }

    public void Test(FrameData data)
    {
        Name = "test.json";
        CalculateInternal(data, Name);
    }
}


