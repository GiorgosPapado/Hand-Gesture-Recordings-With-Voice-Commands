using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static FrameData;
using static OVRSkeleton;
using UnityEditor;
using System;
using UnityEngine.UI;
using static OVRCustomSkeleton;


public class DataPersistenceManager : MonoBehaviour
{
    private FileDataHandler dataHandler;
    public static DataPersistenceManager Instance { get; private set; }
    private DynamicGesture gesture = new DynamicGesture();
    //private FrameData data = new FrameData();
    public OVRSkeleton leftHandSkeleton;
    public OVRSkeleton rightHandSkeleton;
    public Transform hmd;

    public GameObject button;
    public int ClassID;

    //bones try
    private List<TrackingData> left = new();
    private List<TrackingData> right = new();

    public Transform leftHandAnchor;
    public Transform rightHandAnchor;

    private Vector3 rightHandStartPos;
    private Quaternion rightHandStartRot;

    private Vector3 leftHandStartPos;
    private Quaternion leftHandStartRot;

    private void Awake()
    {
        DateTime saveNow = DateTime.Now;
        int hour = saveNow.Hour;
        int day = saveNow.Day;
        int min = saveNow.Minute;
        string ff = day.ToString() + hour.ToString() + min.ToString();
        Debug.Log(ff);
        Debug.Log(Application.persistentDataPath);
        if (Instance != null)
        {
            Debug.Log("George: Found more than one Data Manager in the scene.");
        }
        Instance = this;
        dataHandler = new FileDataHandler(Application.persistentDataPath, "Left Hand059870Wednesday, 27 July 2022.json");
        left = dataHandler.Load().frameData.left_hand;
        right = dataHandler.Load().frameData.right_hand;               
    }

    public void SaveGesture(FrameData data)
    {
        //Calculate(data);
        gesture.class_name = button.name;
        gesture.frameData = data;
        dataHandler.Save(gesture);
    }

    /*    public void LoadGesture()
        {
            dataHandler = new FileDataHandler(Application.persistentDataPath, "/Left Hand059870Wednesday, 27 July 2022.json");
            dataHandler.Load().frameData.left_hand.ToList();
        }*/

    public void Calculate(FrameData data)
    {
        Guid fileName = Guid.NewGuid();
        TrackingData leftTrackData = new TrackingData();
        TrackingData rightTrackData = new TrackingData();
        string dt = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        //string dt = DateTime.Now.ToString("date:HH:mm");
        //string dt = DateTime.Now.ToString("HH:mm:F");
        dataHandler = new FileDataHandler(Application.persistentDataPath, button.name + ClassID + fileName.ToString().Substring(0, 5) + dt + ".json");
        if (leftHandSkeleton.Bones.Count > 0 || rightHandSkeleton.Bones.Count > 0)
        {
            for (int i = (int)leftHandSkeleton.GetCurrentStartBoneId(); i < (int)leftHandSkeleton.GetCurrentEndBoneId(); i++)
            {                
                leftTrackData.pos.Add(leftHandSkeleton.Bones[i].Transform.localPosition);
                leftTrackData.rotation.Add(leftHandSkeleton.Bones[i].Transform.localRotation);
                //gesture.class_ID.Add((int)leftHandSkeleton.Bones[i].Id);
                //gesture.class_name.Add(leftHandSkeleton.Bones[i].Transform.name);
                //gesture.class_ID.Equals(leftHandSkeleton.Bones[i].Id);
                //gesture.class_name.Equals(leftHandSkeleton.Bones[i].Transform.name);                
            }

            for (int j = (int)rightHandSkeleton.GetCurrentStartBoneId(); j < (int)rightHandSkeleton.GetCurrentEndBoneId(); j++)
            {
                rightTrackData.pos.Add(rightHandSkeleton.Bones[j].Transform.localPosition);
                rightTrackData.rotation.Add(rightHandSkeleton.Bones[j].Transform.localRotation);
                //gesture.class_ID.Equals(rightHandSkeleton.Bones[j].Id);
                //gesture.class_name.Equals(rightHandSkeleton.Bones[j].Transform.name);
            }
            data.left_hand.Add(leftTrackData);
            data.right_hand.Add(rightTrackData);
            data.timestamps.Add(Time.time);
            if(leftHandSkeleton.Bones.Count > 0)
            {
                data.left_hand_global.pos.Add(leftHandAnchor.localPosition);
                data.left_hand_global.rotation.Add(leftHandAnchor.localRotation);
            }
            if(rightHandSkeleton.Bones.Count > 0)
            {
                data.right_hand_global.pos.Add(rightHandAnchor.localPosition);
                data.right_hand_global.rotation.Add(rightHandAnchor.localRotation);
            }
        }
        data.hmd.pos.Add(hmd.localPosition);
        data.hmd.rotation.Add(hmd.localRotation);
        ///////////////////////////////////////////////////////////////////////
        //gesture.frameData.Add(data);
        //Debug.Log(button);
    }
}

//private void Start()
//{
//    this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
//    this.dataPersistenceObjects = FindAllDataPersistenceObjects();
//    LoadGesture();
//}

//public void NewGesture()
//{
//    this.frameData = new FrameData();
//}

//public void LoadGesture()
//{
//    //Load any saved data from a file data handler
//    this.frameData = dataHandler.Load();
//    // if no data to load, no gestures
//    if(this.frameData == null)
//    {
//        Debug.Log("George: No data was found. Initiallizing data to default.");

//    }

//    //push the Loaded Data to all other scripts that need it
//        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
//    {
//        dataPersistenceObj.LoadData(frameData);
//    }

//    //Debug.Log("George: Load Gestures Count = " + frameData.left_hand);
//    //Debug.Log("George: Load Gestures Count = " + frameData.right_hand);
//}


/* public void SaveGesture()
    {

        var frameData = new FrameData();

        frameData.right_hand.Add(new TrackingData()
        {
            pos = new List<Vector3> {
                Vector3.one,
                Vector3.zero
                },
            rotation = new List<Quaternion> {
                Quaternion.AngleAxis(20, Vector3.up),
                Quaternion.AngleAxis(20, Vector3.down)
                }
        });*//*


        //for (int i = (int)BoneId.Hand_Start; i < (int)BoneId.Hand_End; i++)
        DynamicGesture gesture = new DynamicGesture();
        //gesture.frameData leftTrackData = new TrackingData();
        TrackingData leftTrackData = new TrackingData();
        TrackingData rightTrackData = new TrackingData();
        //for (int i = (int)BoneId.Hand_Start; i < (int)BoneId.Hand_End; i++)\
        if (leftHandSkeleton.Bones.Count > 0)
        {
            for (int i = (int)leftHandSkeleton.GetCurrentStartBoneId(); i < (int)leftHandSkeleton.GetCurrentEndBoneId(); i++)
            {
                leftTrackData.pos.Add(leftHandSkeleton.Bones[i].Transform.position);
                leftTrackData.rotation.Add(leftHandSkeleton.Bones[i].Transform.rotation);
                gesture.class_ID.Add((int)leftHandSkeleton.Bones[i].Id);
                gesture.class_name.Add(leftHandSkeleton.Bones[i].Transform.name);
            }
        }
        if (rightHandSkeleton.Bones.Count > 0) 
        { 
            for (int j = (int)rightHandSkeleton.GetCurrentStartBoneId(); j < (int)rightHandSkeleton.GetCurrentEndBoneId(); j++)
            {
                rightTrackData.pos.Add(rightHandSkeleton.Bones[j].Transform.position);
                rightTrackData.rotation.Add(rightHandSkeleton.Bones[j].Transform.rotation);
                gesture.class_ID.Add((int)rightHandSkeleton.Bones[j].Id);
                gesture.class_name.Add(rightHandSkeleton.Bones[j].Transform.name);
            }
        }
        frameData.left_hand.Add(leftTrackData);
        frameData.right_hand.Add(rightTrackData);
        //gesture.frameData.Add(leftTrackData);
        //gesture.frameData.Add(rightTrackData);
        dataHandler.Save(frameData);
     }*/

/*    private void OnApplicationQuit()
        {
            SaveGesture();
        }*/


