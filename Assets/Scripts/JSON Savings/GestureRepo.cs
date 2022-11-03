using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;

public class GestureRepo : MonoBehaviour
{
    public Dictionary<string, bool> validGesture = new();
    public string path = Directory.GetCurrentDirectory() + "/build/Gestures";
    public string[] files;
    public GameObject[] animations;
    public static int counter = 0;
    private int initCounter = 0;
    private List<string> allJson = new();
    private Guid FolderGui = Guid.NewGuid();

    private void Start()
    {
        //Directory.GetCurrentDirectory().Equals(path);
        files = Directory.GetFiles(path, "*.json");
        
        
        for (int i = 0; i < files.Length; i++)
        {
            validGesture.Add(Path.GetFileName(files[i]), true);
            allJson = validGesture.Keys.ToList();
             //allJson[i] = Path.GetFileName(files[i]);
        }
}

public void Save()
    {
        string fileName = " " + FolderGui.ToString().Substring(0, 7) + " " + "Repos.json"  ;
        string fullPath = path + fileName;

        string json = JsonConvert.SerializeObject(validGesture, Formatting.Indented);     
        File.WriteAllText(fullPath, json);
        //File.WriteAllLines(fullPath, validGesture);
    }

    public void GestureAnimation()
    {
        animations = GameObject.FindGameObjectsWithTag("Gestures");

        for (int i = initCounter; i < initCounter + animations.Length; i++)
        {
            var component = animations[i - initCounter].GetComponent<JSONRead>();
            component.jsonFile = allJson[i];
            component.Refresh();

            counter = i;
            if (i == files.Length) 
            {
                counter = 0;
                break;
            }
        }
        initCounter = counter;
    }

/*    public void Rejection()
    {
        var rejectedFile = gameObject.transform.parent.GetComponent<JSONRead>().jsonFile;
        if (validGesture.ContainsKey(rejectedFile))
        {
            validGesture[rejectedFile] = false;
        }
    }*/
}