using System.Collections;
using System.Collections.Generic;
using Controllers.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartNewGame : Menuitem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This method will define the action on mous click
	public override void OnMouseUp() {
        //SceneManager will load the specified scene to start the gameplay
        SceneManager.LoadScene(1);
		throw new System.NotImplementedException();
	}
}
