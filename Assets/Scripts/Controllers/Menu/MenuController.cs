using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Menu{
    public class MenuController //: MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //Start a game withoout a save file
        void startNewGame(){
            //SceneManager will load the specified scene to start the gameplay
            SceneManager.LoadScene(1);
            //throw new System.NotImplementedException();
        }

        //Start a game from the latest save file
        void continueGame(){
            //Overleggen met Wouter hoe we een gamesave inladen
            throw new System.NotImplementedException();
        }

        //Quit the application
        void quitToDesktop(){
            Application.Quit();
        }

        //Navigate back to the main menu
        void quitToMainMenu(){

        }
    }
}
