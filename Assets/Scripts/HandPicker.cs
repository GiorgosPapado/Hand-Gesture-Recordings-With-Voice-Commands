using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPicker : MonoBehaviour
{
    public RECORD_TYPE RecordType;
    public DataPersistenceManager manager;

    private void OnEnable()
    {
        manager.recordType = RecordType;
    }
}
