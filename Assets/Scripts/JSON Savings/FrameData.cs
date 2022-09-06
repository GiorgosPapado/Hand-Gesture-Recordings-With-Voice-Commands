using System.Collections.Generic;


[System.Serializable]
public class FrameData
{
    public List<TrackingData> right_hand = new List<TrackingData>();
    public List<TrackingData> left_hand = new List<TrackingData>();
    public TrackingData hmd = new TrackingData();
    public TrackingData right_hand_global = new TrackingData();
    public TrackingData left_hand_global = new TrackingData();

    public List<float> timestamps = new List<float>();        // add timestamps
}