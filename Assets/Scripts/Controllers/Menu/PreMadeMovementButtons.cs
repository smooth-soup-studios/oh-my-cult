using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PreMadeMovementButtons
{
    //Keyboard
    public Button KeyboardSmall = new Button();
    public Button KeyboardBig = new Button();

    //Controller
    public Button LeftStick = new Button();
    public Button RightStick = new Button();
    public Button A = new Button();
    public Button B = new Button();
    public Button X = new Button();
    public Button Y = new Button();
    public Button Rb = new Button();
    public Button Rt = new Button();
    public Button Lb = new Button();
    public Button Lt = new Button();
    public Button Up = new Button();
    public Button Down = new Button();
    public Button Left = new Button();
    public Button Right = new Button();

    public PreMadeMovementButtons(){
        // Keyboard initialisation
        KeyboardInit();

        // Controller initialisation
        ControllerInit(ref LeftStick);
        ControllerInit(ref RightStick);
        ControllerInit(ref A);
        ControllerInit(ref B);
        ControllerInit(ref X);
        ControllerInit(ref Y);
        ControllerInit(ref Rb);
        ControllerInit(ref Rt);
        ControllerInit(ref Lb);
        ControllerInit(ref Lt);
        ControllerInit(ref Up);
        ControllerInit(ref Down);
        ControllerInit(ref Left);
        ControllerInit(ref Right);

    }

    private void KeyboardInit(){
        // Small
        KeyboardSmall.style.width = 140;
        KeyboardSmall.style.height = 130;
        KeyboardSmall.style.unityTextAlign = TextAnchor.UpperCenter;
        KeyboardSmall.style.fontSize = 60;

        // Big
        KeyboardBig.style.width = 280;
        KeyboardBig.style.height = 130;
        KeyboardBig.style.unityTextAlign = TextAnchor.UpperCenter;
        KeyboardBig.style.fontSize = 60;
    }

    private void ControllerInit(ref Button controller){
        controller.style.width = 125;
        controller.style.height = 125;
        controller.style.unityTextAlign = TextAnchor.UpperCenter;
        controller.style.fontSize = 60;
    }
}
