using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandleButton : MonoBehaviour
{
    UnityEvent m_MyEvent = new UnityEvent();
    private Button[] buttons;
    private GameObject dismissButton;
    DataPersistenceManager manager;

    private void Start()
    {
        manager = GetComponent<DataPersistenceManager>();
        buttons = FindObjectsOfType<Button>();
        //buttons = Resources.FindObjectsOfTypeAll<Button>();
    }
    /*    void ChangeFunction(UnityEngine.UnityEvents.UnityAction action)
        {
            buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
            buttons[0].GetComponent<Button>().onClick.AddListener(action);
        }*/
    public void Dismiss()
    {
        dismissButton = EventSystem.current.currentSelectedGameObject;
        //Debug.Log(dismissButton);
        //dismissButton.name = EventSystem.current.currentSelectedGameObject.name;
        manager.button = dismissButton;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (dismissButton.name == buttons[i].name) 
                manager.ClassID = i;
        }
    }
}
