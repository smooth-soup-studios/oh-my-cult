using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EventSystemSelectionInitializer : MonoBehaviour
{
    public PanelSettings PanelSettings; // drag your PanelSettings here in the inspector
    void Start()
    {
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(EventSystem.current.transform.Find(PanelSettings.name).gameObject);
    }
}
