using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    //Change color when the mouse hovers over the object
    private void OnMouseEnter() {
        GetComponent<Renderer>().material.color = Color.green;
    }

    //Change color back to normal when the mouse is no longer hovering over the object
    private void OnMouseExit() {
        GetComponent<Renderer>().material.color = Color.black;
    }
}
