using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
//using ZeroFormatter;
/*using MessagePack;
using MsgPack;

[MessagePackObject(keyAsPropertyName: true)]*/
//[ZeroFormattable]
public class FileDataHandler
{
    private string dataDirPath = "";
    public string dataFileName = "";
    
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public DynamicGesture Load()
    {
        //use Path.Combine to account for different OS'S having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        DynamicGesture loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //load the serialized Data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize the data from Json back into the c# object
                loadedData = JsonUtility.FromJson<DynamicGesture>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log("George: Error occured when trying to load data from file" + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(DynamicGesture data)
    {

        //use Path.Combine to account for different OS'S having different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // create the directory path will be written to if it doesnt already exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize the c# frame data object into Json
            string dataToStore = JsonUtility.ToJson(data, true);

            //write the serialized data to the file
            //use Using blocks as they ensure that the connection to that file is closed once we are done reading or writing to it.
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log("George: Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
}
