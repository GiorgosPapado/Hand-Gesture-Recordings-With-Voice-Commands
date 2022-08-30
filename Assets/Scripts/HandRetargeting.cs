using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.Assertions;

public class HandRetargeting : MonoBehaviour
{
    public OVRSkeleton LeftHand;
    public OVRSkeleton RightHand;

    public OVRCustomSkeleton RetargetedLeftHand;
    public OVRCustomSkeleton RetargetedRightHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void RetargetHand(OVRSkeleton source, OVRCustomSkeleton target)
    {
        if(source.Bones.Count > 0) { 
            for(int boneid = (int)source.GetCurrentStartBoneId(); boneid < (int)source.GetCurrentEndBoneId(); boneid++)
            {
                Assert.IsTrue(boneid >= (int)target.GetCurrentStartBoneId() && boneid < (int)target.GetCurrentEndBoneId());
                target.CustomBones[boneid].transform.position = source.Bones[boneid].Transform.position;
                target.CustomBones[boneid].transform.rotation = source.Bones[boneid].Transform.rotation;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RetargetHand(LeftHand, RetargetedLeftHand);
        RetargetHand(RightHand, RetargetedRightHand);
    }
}
