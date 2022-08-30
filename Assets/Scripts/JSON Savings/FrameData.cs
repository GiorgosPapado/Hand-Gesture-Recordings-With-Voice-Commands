using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;
using static OVRPlugin;

[System.Serializable]
public class FrameData
{
    public List<TrackingData> right_hand = new List<TrackingData>();
    public List<TrackingData> left_hand = new List<TrackingData>();
    public TrackingData hmd = new TrackingData();
    public TrackingData right_hand_global = new TrackingData();
    public TrackingData left_hand_global = new TrackingData();

    public List<float> timestamps = new List<float>();        // add timestamps

/*public FrameData()
    {
        //joints.Count.Equals(250);
        //left_hand = DataPersistenceManager.Instance.leftHandSkeleton.Bones;
*//*        right_hand.Add(new TrackingData() 
        {
            pos = new List<Vector3> {
                Vector3.one,
                Vector3.zero
                },
            rotation = new List<Quaternion> {
                Quaternion.AngleAxis(20, Vector3.up),
                Quaternion.AngleAxis(20, Vector3.down)
                }
        });        */
        
/*        for (int i = (int)BoneId.Hand_Start; i < (int)BoneId.Hand_End; i++)
        {
            joints[i].Transform.position = DataPersistenceManager.Instance.leftHandSkeleton.Bones[i].Transform.position;
        }

        for (int j = (int)BoneId.Hand_Start; j < (int)BoneId.Hand_End; j++)
        {
            joints[j].Transform.position = DataPersistenceManager.Instance.leftHandSkeleton.Bones[j].Transform.position;
            //joints[j] = DataPersistenceManager.Instance.leftHandSkeleton.Bones[j];
        };*//*
    }*/
}