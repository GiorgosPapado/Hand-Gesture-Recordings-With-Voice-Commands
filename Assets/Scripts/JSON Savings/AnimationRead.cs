using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class AnimationRead : MonoBehaviour
{
    private FileDataHandler dataHandler;
    public OVRCustomSkeleton leftSkeleton;
    public OVRCustomSkeleton rightSkeleton;
    public string jsonFile;

    private List<TrackingData> left = new();
    private List<TrackingData> right = new();
    private List<float> hour = new();
    private FrameData frameData;
    public DataPersistenceManager manager;
    private bool counter = false;
    public GameObject gesture;
    private OVRCustomSkeleton lefty;
    private OVRCustomSkeleton righty;
    public GameObject panel;

    private void Start()
    {
        //StartCoroutine(nameof(MyJoints));
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(MyJoints));
    }

    public void OnDisable()
    {
        StopCoroutine(nameof(MyJoints));
        counter = false;
        if (lefty != null)
            Destroy(lefty.gameObject);
        if (righty != null)
            Destroy(righty.gameObject);
        leftSkeleton.gameObject.SetActive(true);
        rightSkeleton.gameObject.SetActive(true);
        manager.infinityImageLeft.SetActive(false);
        manager.infinityImageRight.SetActive(false);
    }

    public void ActiveStart()
    {
        if (counter == false && gesture.activeInHierarchy)
        {
            counter = true;
            jsonFile = manager.button.name + ".json";
            dataHandler = new FileDataHandler(Directory.GetCurrentDirectory() + "/Gestures", jsonFile);
            frameData = dataHandler.Load().frameData;
            left = frameData.left_hand;
            right = frameData.right_hand;
            hour = frameData.timestamps;
            righty = Instantiate(rightSkeleton);
            righty.transform.localScale *= 5;
            lefty = Instantiate(leftSkeleton);
            lefty.transform.localScale *= 5;
            leftSkeleton.gameObject.SetActive(false);
            rightSkeleton.gameObject.SetActive(false);
            lefty.GetComponent<OVRHand>().enabled = false;
            lefty.GetComponent<OVRCustomSkeleton>().enabled = false;
            righty.GetComponent<OVRHand>().enabled = false;
            righty.GetComponent<OVRCustomSkeleton>().enabled = false;
        }

        if (manager.button.name == "LeftInfinity")
            manager.infinityImageLeft.SetActive(true);
        else if(manager.button.name == "RightInfinity")
            manager.infinityImageRight.SetActive(true);
    }

    private (Vector3 pos, Quaternion rot) TransformToHMDSpace(Vector3 pos, Quaternion rot, Vector3 HMDPos, Quaternion HMDRot)
    {
        Vector3 tpos = Quaternion.Inverse(HMDRot) * (pos - HMDPos);
        Quaternion trot = Quaternion.Inverse(HMDRot) * rot;
        return (tpos, trot);
    }

    private IEnumerator MyJoints()
    {
        while (true)
        {
            ActiveStart();
            int frameCount = Math.Max(left.Count, right.Count);
            for (int j = 0; j < frameCount; j++)
            {
                for (int i = 0; i < (int)lefty.GetCurrentEndBoneId(); i++)
                {
                    if (left.Count > 0)
                    {
                        lefty.CustomBones[i].transform.localPosition = left[j].pos[i];
                        lefty.CustomBones[i].transform.localRotation = left[j].rotation[i];
                    }
                    if (right.Count > 0)
                    {
                        righty.CustomBones[i].transform.localPosition = right[j].pos[i];
                        righty.CustomBones[i].transform.localRotation = right[j].rotation[i];
                    }
                }
                if (left.Count > 0)
                {
                    var tform = TransformToHMDSpace(frameData.left_hand_global.pos[j], frameData.left_hand_global.rotation[j], frameData.hmd.pos[0], frameData.hmd.rotation[0]);
                    lefty.transform.localPosition = tform.pos + leftSkeleton.transform.position;
                    lefty.transform.localRotation = tform.rot;
                }
                if (right.Count > 0)
                {
                    var tform = TransformToHMDSpace(frameData.right_hand_global.pos[j], frameData.right_hand_global.rotation[j], frameData.hmd.pos[0], frameData.hmd.rotation[0]);
                    righty.transform.localPosition = tform.pos + rightSkeleton.transform.position;
                    righty.transform.localRotation = tform.rot;
                }

                if (left.Count < 1)
                {
                    lefty.gameObject.SetActive(false);
                }
                if (right.Count < 1)
                {
                    righty.gameObject.SetActive(false);
                }
/*                    if (j == 0)
                    {
                        yield return new WaitForSeconds(Time.timeScale);
                    }
                    else
                    {
                        yield return new WaitForSeconds((hour[j] - hour[j - 1]));
                    }*/
                yield return new WaitForSeconds(j == 0 ? Time.timeScale : hour[j] - hour[j - 1]);
               
            }
        }
    }
}
