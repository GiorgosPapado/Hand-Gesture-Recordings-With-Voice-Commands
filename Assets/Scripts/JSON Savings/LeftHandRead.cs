using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static FrameData;
using static OVRSkeleton;
using UnityEditor;
using System;
using UnityEngine.UI;
using static OVRCustomSkeleton;

public class LeftHandRead : MonoBehaviour
{
    private FileDataHandler dataHandler;
    //public OVRCustomSkeleton leftSkeleton;
    //public OVRCustomSkeleton rightSkeleton;
    public OVRCustomSkeleton leftSkeleton;
    public string jsonFile;
    //public string jsonFile = "Left Hand03739eWednesday, 27 July 2022.json";
    //bones try
    private List<TrackingData> left = new();

    private void Start()
    {
        StartCoroutine("MyJoints");
    }

    private void Awake()
    {

        for (int i = 0; i < 24; i++)
        {
            //leftSkeleton.Bones[i].Id = ids;
            //Debug.Log(leftSkeleton.Bones[i].Id);
            //Debug.Log(leftSkeleton.IsDataValid);
            leftSkeleton.gameObject.SetActive(true);
        }
        //leftSkeleton = (OVRCustomSkeleton)leftHandSkeleton;
        //rightSkeleton = (OVRCustomSkeleton)rightHandSkeleton;
        //OVRSkeleton.SkeletonType skeletonTypeS = leftSkeleton.GetSkeletonType();
        //OVRSkeleton.SkeletonType skeletonType = rightSkeleton.GetSkeletonType();
        //Debug.Log(leftSkeleton.Bones[1]);
        Debug.Log(Application.persistentDataPath);
        dataHandler = new FileDataHandler(Application.persistentDataPath, jsonFile);
        left = dataHandler.Load().frameData.left_hand;

        //OVRSkeleton.SkeletonType skeletonTypeS = leftSkeleton.GetSkeletonType();
        //OVRSkeleton.SkeletonType skeletonType = rightSkeleton.GetSkeletonType();
        //leftSkeleton.TryAutoMapBonesByName();
        //rightSkeleton.TryAutoMapBonesByName();


        //leftHandSkeleton.Bones[j].Transform.Translate(left[j].pos[j], Space.Self);
        //rightHandSkeleton.Bones[j].Transform.Translate(right[j].pos[j], Space.Self);
        //leftHandSkeleton.Bones[j].Transform.position = left[j].pos[j];
        //rightHandSkeleton.Bones[j].Transform.position = right[j].pos[j];
        /*            leftHandSkeleton.Bones[j].Equals(left[j].pos[j]);
                    rightHandSkeleton.Bones[j].Equals(right[j].pos[j]);*/

    }
    private IEnumerator MyJoints()
    {
        while (true)
        {
            /*if (leftSkeleton.IsInitialized && rightSkeleton.IsInitialized)
            {*/
            for (int j = 0; j < left.Count; j++)
            {
                //for(int i = (int)leftSkeleton.GetCurrentStartBoneId(); i < left[j].pos.Count; i++)
                for (int i = (int)leftSkeleton.GetCurrentStartBoneId(); i < (int)leftSkeleton.GetCurrentEndBoneId(); i++)
                {
                    //leftSkeleton.CustomBones[i].transform.position = left[j].pos[i];
                    //rightSkeleton.CustomBones[i].transform.position = right[j].pos[i];
                    leftSkeleton.CustomBones[i].transform.rotation = left[j].rotation[i];

                    //leftSkeleton.Bones[i].Transform.position = left[j].pos[i];
                    //rightSkeleton.Bones[i].Transform.position = right[j].pos[i];
                    //leftSkeleton.Bones[i].Transform.rotation = left[j].rotation[i];
                    //rightSkeleton.Bones[i].Transform.rotation = right[j].rotation[i];    
                }
                yield return new WaitForSeconds(0.1f);
            }
            //}
        }
    }
}

