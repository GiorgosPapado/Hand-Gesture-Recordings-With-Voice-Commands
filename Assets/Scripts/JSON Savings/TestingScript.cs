using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestingScript : MonoBehaviour
{

    private string gesture = "LeftInfinity 0 398cc 13 September 17 00 1.json";

    private void OnEnable()
    {
        Debug.Log(Directory.GetCurrentDirectory());
        Debug.Log(Directory.GetCurrentDirectory() + "/Gestures");
        File.Delete(Directory.GetCurrentDirectory() + "/Gestures/" + gesture);
        //File.Delete(Directory.GetCurrentDirectory() + "/new/new.txt");
    }
}
