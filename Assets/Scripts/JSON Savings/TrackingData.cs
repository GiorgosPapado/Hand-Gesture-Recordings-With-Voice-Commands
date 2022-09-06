using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrackingData
{
    public List<Vector3> pos = new List<Vector3>();
    public List<Quaternion> rotation = new List<Quaternion>();
}

