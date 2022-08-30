using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRSkeleton;

[System.Serializable]
public class TrackingData
{
    public List<Vector3> pos = new List<Vector3>();
    public List<Quaternion> rotation = new List<Quaternion>();
    //public List<JointData> jointData = new List<JointData>();
}

/*public class JointData
{
    public BoneId boneId;
    public Vector3 pos;
    public Quaternion rotation;
}*/