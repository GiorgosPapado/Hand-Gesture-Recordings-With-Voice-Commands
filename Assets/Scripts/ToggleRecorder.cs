using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRecorder : MonoBehaviour
{
/*    public Color green;
    public Color Red;*/
    public void RecordingON()
        {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    public void RecordingOff()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
