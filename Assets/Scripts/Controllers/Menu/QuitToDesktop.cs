using System.Collections;
using System.Collections.Generic;
using Controllers.Menu;
using UnityEngine;

public class QuitToDesktop : Menuitem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This method will define the action on mous click
	public override void OnMouseUp() {
        //Quit the application
        Application.Quit();
	}
}
