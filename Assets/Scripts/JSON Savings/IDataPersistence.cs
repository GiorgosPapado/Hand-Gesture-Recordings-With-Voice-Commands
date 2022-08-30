using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence 
{
    public void LoadData(FrameData frameData);

    public void SaveData(FrameData frameData);
}
