using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Menu{
    public class MenuController : MonoBehaviour
    {
    //     // Start is called before the first frame update
    //     void Start()
    //     {
            
    //     }

    //     // Update is called once per frame
    //     void Update()
    //     {
            
    //     }

        //Start a game without a save file
        public void startNewGame(){
            //SceneManager will load the specified scene to start the gameplay
            // can be made to load the next scene instead of a fixed one
            SceneManager.LoadScene(1);
            //throw new System.NotImplementedException();
        }

        //Start a game from the latest save file
        void continueGame(){
            //Overleggen met Wouter hoe we een gamesave inladen
            throw new System.NotImplementedException();
        }

        //Quit the application
        public void quitToDesktop(){
            //Debug print to show it works within unity
            Debug.Log("Quit!");
            // This wont work in unity itself but it does with a build
            Application.Quit();
        }

        //Navigate back to the main menu
        public void quitToMainMenu(){
            //return to the scene whith the main menu 
            SceneManager.LoadScene(0);
        }
    }
}
