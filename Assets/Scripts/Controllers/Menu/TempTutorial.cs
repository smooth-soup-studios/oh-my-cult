using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempTutorial : MonoBehaviour
{
    void StartGame(){
        SceneManager.LoadSceneAsync(2);
    }
    
}
