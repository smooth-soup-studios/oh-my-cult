using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Menu{
    public class InGameMenu : MonoBehaviour
    {
        
        public void QuitToMainMenu(){
            //return to the scene whith the main menu 
            //First an unload scene?
            SceneManager.LoadScene(2);
        }
    }
}