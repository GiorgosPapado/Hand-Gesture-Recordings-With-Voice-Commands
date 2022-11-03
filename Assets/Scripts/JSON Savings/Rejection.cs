using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rejection : MonoBehaviour
{
    public GestureRepo repo;
    public void RejectedGesture()
    {
        var rejectedFile = gameObject.transform.parent.GetComponent<JSONRead>().jsonFile;
        if (repo.validGesture.ContainsKey(rejectedFile))
        {
            repo.validGesture[rejectedFile] = false;
        }
    }


}
