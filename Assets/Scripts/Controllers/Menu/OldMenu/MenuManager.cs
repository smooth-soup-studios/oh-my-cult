using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers.Menu{
    public class MenuManager : MonoBehaviour
    {
    public GameObject InGameMenu;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape)){
                PauseGame();
            }
        }

        public void PauseGame(){
            InGameMenu.SetActive(!InGameMenu.activeSelf);
            if(InGameMenu.activeSelf){
                Time.timeScale = 0;
            }
            else{
                Time.timeScale = 1;
            }
        }
    }
}
