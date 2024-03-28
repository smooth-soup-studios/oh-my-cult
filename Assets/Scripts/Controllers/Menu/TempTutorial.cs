using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempTutorial : MonoBehaviour
{
    public void StartGame(){
        //Set the actual game scene in the third slot for this to work
        SceneManager.LoadSceneAsync(2);
    }
    
}
