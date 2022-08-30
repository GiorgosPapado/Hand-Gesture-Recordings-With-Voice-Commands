/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static FrameData;
using static OVRSkeleton;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;
    public static DataPersistenceManager Instance { get; private set; }

    public OVRSkeleton leftHandSkeleton;
    public OVRSkeleton rightHandSkeleton;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("George: Found more than one Data Manager in the scene.");
        }
        Instance = this;
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        //SaveGesture();
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


*//*    public void SaveGesture()
    {
        // pass the data to other scripts to update it
        //foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        //{
        //    dataPersistenceObj.SaveData(frameData);
        //}

        //save that data to a file using that data handler
        var frameData = new FrameData();
        *//*frameData.left_hand.Add(new TrackingData()
        {
            pos = new List<Vector3> {
                Vector3.one,
                Vector3.zero
            },
            rotation = new List<Quaternion> {
                Quaternion.AngleAxis(20, Vector3.up),
                Quaternion.AngleAxis(20, Vector3.down)
            }
        });

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
            }*//*

    public void SaveGesture()
    {
        var gesture = new DynamicGesture();
        var data = new FrameData();
        TrackingData leftTrackData = new TrackingData();
        TrackingData rightTrackData = new TrackingData();

        if (leftHandSkeleton.Bones.Count > 0)
        {
            for (int i = (int)leftHandSkeleton.GetCurrentStartBoneId(); i < (int)leftHandSkeleton.GetCurrentEndBoneId(); i++)
            {
                JointData jd = new JointData();
                jd.boneId = leftHandSkeleton.Bones[i].Id;
                jd.pos = leftHandSkeleton.Bones[i].Transform.position;
                jd.rotation = leftHandSkeleton.Bones[i].Transform.rotation;
                leftTrackData.jointData.Add(jd);
*//*                leftTrackData.pos.Add(leftHandSkeleton.Bones[i].Transform.position);
                leftTrackData.rotation.Add(leftHandSkeleton.Bones[i].Transform.rotation);
                gesture.class_ID.Add((int)leftHandSkeleton.Bones[i].Id);
                gesture.class_name.Add(leftHandSkeleton.Bones[i].Transform.name);*//*
            }
            //data.left_hand.Add(leftTrackData);
            //            gesture.frameData.Add(data);
            data.left_hand.Add(leftTrackData);
        }

        if (rightHandSkeleton.Bones.Count > 0)
        {
            for (int j = (int)rightHandSkeleton.GetCurrentStartBoneId(); j < (int)rightHandSkeleton.GetCurrentEndBoneId(); j++)
            {
                JointData jd = new JointData();
                jd.boneId = rightHandSkeleton.Bones[j].Id;
                jd.pos = rightHandSkeleton.Bones[j].Transform.position;
                jd.rotation = rightHandSkeleton.Bones[j].Transform.rotation;
                rightTrackData.jointData.Add(jd);
            }
            data.right_hand.Add(rightTrackData);
        }
        gesture.frameData.Add(data);
        dataHandler.Save(gesture);
    }
}
*/