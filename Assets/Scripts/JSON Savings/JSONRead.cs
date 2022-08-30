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

public class JSONRead : MonoBehaviour 
{
    /*    private const string Path = @"C:/Users/giorgospap/AppData/LocalLow/DefaultCompany/Gesture Project/Left Hand059870Wednesday, 27 July 2022.json";

        // Start is called before the first frame update
        void Start()
        {
            string json = File.ReadAllText(Path);
            var obj = JsonUtility.FromJson<DynamicGesture>(json);

        }*/

    private FileDataHandler dataHandler;
    //public OVRCustomSkeleton leftSkeleton;
    //public OVRCustomSkeleton rightSkeleton;
    public OVRCustomSkeleton leftSkeleton;
    public OVRCustomSkeleton rightSkeleton;
    public string jsonFile;
    //public string jsonFile = "Left Hand03739eWednesday, 27 July 2022.json";
    //bones try
    private List<TrackingData> left = new();
    private List<TrackingData> right = new();
    private List<float> hour = new();
    private FrameData frameData;

    
    private void Start()
    {
        //if(left.Count > 0)
        //{
        //    var offsetL = leftSkeleton.CustomBones[0].transform.position - left[0].pos[0];
        //    leftSkeleton.CustomBones[0].transform.position = left[0].pos[0] + offsetL;
        //    leftSkeleton.CustomBones[0].transform.rotation = left[0].rotation[0];
        //}

        //if(right.Count > 0)
        //{
        //    var offsetR = rightSkeleton.CustomBones[0].transform.position - right[0].pos[0];
        //    rightSkeleton.CustomBones[0].transform.position = right[0].pos[0] + offsetR;
        //    rightSkeleton.CustomBones[0].transform.rotation = right[0].rotation[0];
        //}

        StartCoroutine(nameof(MyJoints));
    }

    private void Awake()
    {

/*        for(int i = 0; i < 24; i++)
        {
            //leftSkeleton.Bones[i].Id = ids;
            //Debug.Log(leftSkeleton.Bones[i].Id);
            //Debug.Log(leftSkeleton.IsDataValid);
            leftSkeleton.gameObject.SetActive(true);
        }*/
        //leftSkeleton = (OVRCustomSkeleton)leftHandSkeleton;
        //rightSkeleton = (OVRCustomSkeleton)rightHandSkeleton;
        //OVRSkeleton.SkeletonType skeletonTypeS = leftSkeleton.GetSkeletonType();
        //OVRSkeleton.SkeletonType skeletonType = rightSkeleton.GetSkeletonType();
        //Debug.Log(leftSkeleton.Bones[1]);
        Debug.Log(Application.persistentDataPath);
        dataHandler = new FileDataHandler(Application.persistentDataPath, jsonFile);
        frameData = dataHandler.Load().frameData;
        left = frameData.left_hand;
        right = frameData.right_hand;
        hour = frameData.timestamps;

        //leftHandOriginalPos = leftSkeleton.transform.position;
        //leftHandOriginalRot = leftSkeleton.transform.rotation;

        //rightHandOriginalPos = rightSkeleton.transform.position;
        //rightHandOriginalRot = rightSkeleton.transform.rotation;

    }    
   /* private void Update()
    {
        int frameCount = Math.Max(left.Count, right.Count);
        for (int j = 0; j < frameCount; j++)
        {
            for (int i = (int)leftSkeleton.GetCurrentStartBoneId(); i < (int)leftSkeleton.GetCurrentEndBoneId(); i++)
            {
                if (left.Count > 0)
                {
                    //leftSkeleton.CustomBones[i].transform.position = left[j].pos[i];
                    leftSkeleton.CustomBones[i].transform.rotation = left[j].rotation[i];
                    //Mathf.Lerp(leftSkeleton.CustomBones[i].transform.position.x, left[j].pos[i].x, hour[i]);
                    leftSkeleton.CustomBones[i].transform.position = new Vector3(Mathf.Lerp(leftSkeleton.CustomBones[i].transform.position.x, left[j].pos[i].x, 1), Mathf.Lerp(leftSkeleton.CustomBones[i].transform.position.y, left[j].pos[i].y, 1), Mathf.Lerp(leftSkeleton.CustomBones[i].transform.position.z, left[j].pos[i].z, 1));
                }
            }
            for (int i = (int)rightSkeleton.GetCurrentStartBoneId(); i < (int)rightSkeleton.GetCurrentEndBoneId(); i++)
            {
                if (right.Count > 0)
                {
                    //rightSkeleton.CustomBones[i].transform.position = right[j].pos[i];
                    rightSkeleton.CustomBones[i].transform.rotation = right[j].rotation[i];
                    rightSkeleton.CustomBones[i].transform.position = new Vector3(Mathf.Lerp(rightSkeleton.CustomBones[i].transform.position.x, right[j].pos[i].x, 1), Mathf.Lerp(rightSkeleton.CustomBones[i].transform.position.y, right[j].pos[i].y, 1), Mathf.Lerp(rightSkeleton.CustomBones[i].transform.position.z, right[j].pos[i].z, 1));
                }
            }
            //leftSkeleton.Bones[i].Transform.position = left[j].pos[i];
            //rightSkeleton.Bones[i].Transform.position = right[j].pos[i];
            //leftSkeleton.Bones[i].Transform.rotation = left[j].rotation[i];
            //rightSkeleton.Bones[i].Transform.rotation = right[j].rotation[i];    
            if (left.Count < 1)
            {
                leftSkeleton.gameObject.SetActive(false);
            }
            if (right.Count < 1)
            {
                rightSkeleton.gameObject.SetActive(false);
            }
        }*/

    private (Vector3 pos, Quaternion rot) TransformToHMDSpace(Vector3 pos, Quaternion rot, Vector3 HMDPos, Quaternion HMDRot)
    {
        Vector3 tpos = Quaternion.Inverse(HMDRot)*(pos - HMDPos);
        Quaternion trot = Quaternion.Inverse(HMDRot) * rot;        
        //return (Vector3.zero, Quaternion.identity);
        return (tpos, trot);
    }
    private IEnumerator MyJoints()
    {
        while (true)
        {

            //*if (leftSkeleton.IsInitialized && rightSkeleton.IsInitialized)
            //*
            int frameCount = Math.Max(left.Count, right.Count);
            for (int j = 0; j < frameCount; j++)
            {
                //var offsetL = leftSkeleton.CustomBones[0].transform.localPosition - left[j].pos[0];
                //var offsetR = rightSkeleton.CustomBones[0].transform.localPosition - right[j].pos[0];
                for (int i = 0; i < (int)leftSkeleton.GetCurrentEndBoneId(); i++)
                {
                    if (left.Count > 0)
                    {
                        //var tform = TransformToHMDSpace(left[j].pos[i], left[j].rotation[i], frameData.left_hand_global.pos[0], frameData.left_hand_global.rotation[0]);
                        //leftSkeleton.CustomBones[i].transform.localPosition = tform.pos;
                        //leftSkeleton.CustomBones[i].transform.localRotation = tform.rot;

                        //var offsetL = leftSkeleton.CustomBones[i].transform.localPosition - left[j].pos[i];
                        //var offsetL = leftSkeleton.CustomBones[i].transform.position - left[j-1].pos[i];
                        //leftSkeleton.CustomBones[i].transform.position += left[j].pos[i] - left[j-1].pos[i];

                        //leftSkeleton.CustomBones[i].transform.localPosition = left[j].pos[i];
                        //leftSkeleton.CustomBones[i].transform.localRotation = left[j].rotation[i];


                        //leftSkeleton.CustomBones[i].transform.position = left[j].pos[i];
                        //leftSkeleton.CustomBones[i].transform.rotation = left[j].rotation[i];

                        //Mathf.Lerp(leftSkeleton.CustomBones[i].transform.position.x, left[j].pos[i].x, hour[i]);
                        //leftSkeleton.CustomBones[i].transform.position = new Vector3(Mathf.Lerp(leftSkeleton.CustomBones[i].transform.position.x, left[j].pos[i].x, hour[i]), Mathf.Lerp(leftSkeleton.CustomBones[i].transform.position.y, left[j].pos[i].y, hour[i]), Mathf.Lerp(leftSkeleton.CustomBones[i].transform.position.z, left[j].pos[i].z, hour[i]));

                        leftSkeleton.CustomBones[i].transform.localPosition = left[j].pos[i];
                        leftSkeleton.CustomBones[i].transform.localRotation = left[j].rotation[i];
                    }
                    if (right.Count > 0)
                    {
                        //var tform = TransformToHMDSpace(right[j].pos[i], right[j].rotation[i], frameData.right_hand_global.pos[0], frameData.right_hand_global.rotation[0]);
                        //rightSkeleton.CustomBones[i].transform.localPosition = tform.pos;
                        //rightSkeleton.CustomBones[i].transform.localRotation = tform.rot;
                        //rightSkeleton.CustomBones[i].transform.localPosition = right[j].pos[i];
                        //rightSkeleton.CustomBones[i].transform.localRotation = right[j].rotation[i];

                        //var offsetR = rightSkeleton.CustomBones[i].transform.localPosition - right[j].pos[i];
                        //var offsetR = rightSkeleton.CustomBones[i].transform.position - right[j-1].pos[i];

                        ////rightSkeleton.CustomBones[i].transform.position += right[j].pos[i] - right[j-1].pos[i];


                        //rightSkeleton.CustomBones[i].transform.position = right[j].pos[i];
                        //rightSkeleton.CustomBones[i].transform.rotation = right[j].rotation[i];

                        rightSkeleton.CustomBones[i].transform.localPosition = right[j].pos[i];
                        rightSkeleton.CustomBones[i].transform.localRotation = right[j].rotation[i];
                    }
                }
                if (left.Count > 0)
                {
                    //leftHand.transform.position = leftHandOriginalPos + frameData.left_hand_global.pos[j];                                        
                    //leftSkeleton.transform.localPosition = frameData.left_hand_global.pos[j] - frameData.left_hand_global.pos[0];
                    var tform = TransformToHMDSpace(frameData.left_hand_global.pos[j], frameData.left_hand_global.rotation[j], frameData.hmd.pos[0], frameData.hmd.rotation[0]);
                    leftSkeleton.transform.localPosition = tform.pos;
                    leftSkeleton.transform.localRotation = tform.rot;

                    //leftSkeleton.transform.localPosition = tform.pos;
                    //leftSkeleton.transform.localRotation = Quaternion.Inverse(leftHandOriginalRot) * frameData.left_hand_global.rotation[j];
                    //leftSkeleton.transform.localRotation = frameData.left_hand_global.rotation[j];

                    //leftSkeleton.transform.rotation = Quaternion.Inverse(frameData.hmd.rotation[0]) * frameData.left_hand_global.rotation[j];
                    //leftHand.transform.rotation = leftHandOriginalRot * frameData.left_hand_global.rotation[j];                    

                }
                if(right.Count > 0)
                {
                    //rightHand.transform.position = rightHandOriginalPos + frameData.right_hand_global.pos[j];
                    //rightSkeleton.transform.localPosition = frameData.right_hand_global.pos[j] - frameData.right_hand_global.pos[0];

                    var tform = TransformToHMDSpace(frameData.right_hand_global.pos[j], frameData.right_hand_global.rotation[j], frameData.hmd.pos[0], frameData.hmd.rotation[0]);
                    rightSkeleton.transform.localPosition = tform.pos;
                    rightSkeleton.transform.localRotation = tform.rot;

                    //rightSkeleton.transform.localRotation = Quaternion.Inverse(rightHandOriginalRot) * frameData.right_hand_global.rotation[j];
                    //rightSkeleton.transform.localRotation = frameData.right_hand_global.rotation[j];
                    //rightSkeleton.transform.rotation = Quaternion.Inverse(frameData.hmd.rotation[0]) * frameData.right_hand_global.rotation[j];
                    //rightHand.transform.rotation = rightHandOriginalRot * frameData.right_hand_global.rotation[j];
                }
                //rightSkeleton.CustomBones[i].transform.position = new Vector3(Mathf.Lerp(rightSkeleton.CustomBones[i].transform.position.x, right[j].pos[i].x, hour[i]), Mathf.Lerp(rightSkeleton.CustomBones[i].transform.position.y, right[j].pos[i].y, hour[i]), Mathf.Lerp(rightSkeleton.CustomBones[i].transform.position.z, right[j].pos[i].z, hour[i]));  
                //leftSkeleton.Bones[i].Transform.position = left[j].pos[i];
                //rightSkeleton.Bones[i].Transform.position = right[j].pos[i];
                //leftSkeleton.Bones[i].Transform.rotation = left[j].rotation[i];
                //rightSkeleton.Bones[i].Transform.rotation = right[j].rotation[i];    
                if (left.Count < 1)
                        {
                    leftSkeleton.gameObject.SetActive(false);
                        }
                    if(right.Count < 1)
                        {
                    rightSkeleton.gameObject.SetActive(false);
                         }
                yield return new WaitForSeconds(0.01f);
            }
        } 
    }
}
