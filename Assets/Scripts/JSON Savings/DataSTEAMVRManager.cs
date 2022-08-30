/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static FrameData;
using UnityEditor;
using System;
using UnityEngine.UI;
using static OVRPlugin;

public class DataSTEAMVRManager : MonoBehaviour
{
    private FileDataHandler dataHandler;
    public static DataSTEAMVRManager Instance { get; private set; }
    private DynamicGesture gesture = new DynamicGesture();
    private FrameData data = new FrameData();
    public Skeleton leftHandSkeleton;
    public Skeleton rightHandSkeleton;
    public Button button;



    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("George: Found more than one Data Manager in the scene.");
        }
        Instance = this;
        //DateTime dt = DateTime.Now;
        //dataHandler = new FileDataHandler(Application.persistentDataPath, fileName.ToString().Substring(0, 5) + ".json");
        //SaveGesture();
    }

    public void SaveGesture()
    {
        Calculate(data);
        dataHandler.Save(gesture);
    }

    public void Calculate(FrameData data)
    {
        Guid fileName = Guid.NewGuid();
        TrackingData leftTrackData = new TrackingData();
        TrackingData rightTrackData = new TrackingData();
        string dt = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        dataHandler = new FileDataHandler(Application.persistentDataPath, button.name + fileName.ToString().Substring(0, 5) + dt + ".json");
        if(leftHandSkeleton.NumBones > 0)
        {
            for (int i = (int)leftHandSkeleton.GetCurrentStartBoneId(); i < (int)leftHandSkeleton.GetCurrentEndBoneId(); i++)
            for (int i = (int)leftHandSkeleton.; i < (int)leftHandSkeleton.GetCurrentEndBoneId(); i++)
            {
                leftTrackData.pos.Add(leftHandSkeleton.Bones[i].Transform.position);
                leftTrackData.pos.Add(leftHandSkeleton.Bones[i].Pose.Position);
                leftTrackData.rotation.Add(leftHandSkeleton.Bones[i].Transform.rotation);
                //gesture.class_ID.Add((int)leftHandSkeleton.Bones[i].Id);
                //gesture.class_name.Add(leftHandSkeleton.Bones[i].Transform.name);
                //gesture.class_ID.Equals(leftHandSkeleton.Bones[i].Id);
                //gesture.class_name.Equals(leftHandSkeleton.Bones[i].Transform.name);
            }
            data.left_hand.Add(leftTrackData);
        }

        if (rightHandSkeleton.Bones.Count > 0)
        {
            for (int j = (int)rightHandSkeleton.GetCurrentStartBoneId(); j < (int)rightHandSkeleton.GetCurrentEndBoneId(); j++)
            {
                rightTrackData.pos.Add(rightHandSkeleton.Bones[j].Transform.position);
                rightTrackData.rotation.Add(rightHandSkeleton.Bones[j].Transform.rotation);
                //gesture.class_ID.Equals(rightHandSkeleton.Bones[j].Id);
                //gesture.class_name.Equals(rightHandSkeleton.Bones[j].Transform.name);
            }
            data.right_hand.Add(rightTrackData);
        }
        gesture.frameData.Add(data);
        gesture.class_name = button.name;
    }
}
*/