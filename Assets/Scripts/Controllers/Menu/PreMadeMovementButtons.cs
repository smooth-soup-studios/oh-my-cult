using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PreMadeMovementButtons : MonoBehaviour
{
    //Keyboard
    [HideInInspector] public Button KeyboardSmall;
    [HideInInspector] public Button KeyboardBig;

    //Controller
    [HideInInspector] public Button LeftStick;
    [HideInInspector] public Button RightStick;
    [HideInInspector] public Button A;
    [HideInInspector] public Button B;
    [HideInInspector] public Button X;
    [HideInInspector] public Button Y;
    [HideInInspector] public Button Rb;
    [HideInInspector] public Button Rt;
    [HideInInspector] public Button Lb;
    [HideInInspector] public Button Lt;
    [HideInInspector] public Button Up;
    [HideInInspector] public Button Down;
    [HideInInspector] public Button Left;
    [HideInInspector] public Button Right;

    // Images
    public Sprite KeyboardSmallImg;
    public Sprite KeyboardBigImg;
    public Sprite LeftStickImg;
    public Sprite RightStickImg;
    public Sprite AImg;
    public Sprite BImg;
    public Sprite XImg;
    public Sprite YImg;
    public Sprite RbImg;
    public Sprite RtImg;
    public Sprite LbImg;
    public Sprite LtImg;
    public Sprite UpImg;
    public Sprite DownImg;
    public Sprite LeftImg;
    public Sprite RightImg;

    private void Awake() {
        // Keyboard initialisation
        KeyboardInit();

        // Controller initialisation
        ControllerInit(ref LeftStick, LeftStickImg);
        ControllerInit(ref RightStick, RightStickImg);
        ControllerInit(ref A, AImg);
        ControllerInit(ref B, BImg);
        ControllerInit(ref X, XImg);
        ControllerInit(ref Y, YImg);
        ControllerInit(ref Rb, RbImg);
        ControllerInit(ref Rt, RtImg);
        ControllerInit(ref Lb, LbImg);
        ControllerInit(ref Lt, LtImg);
        ControllerInit(ref Up, UpImg);
        ControllerInit(ref Down, DownImg);
        ControllerInit(ref Left, LeftImg);
        ControllerInit(ref Right, RightImg);
    }

    private void KeyboardInit(){
        // Small
        KeyboardSmall = new();
        KeyboardSmall.style.width = 140;
        KeyboardSmall.style.height = 130;
        KeyboardSmall.style.unityTextAlign = TextAnchor.UpperCenter;
        KeyboardSmall.style.fontSize = 60;
        KeyboardSmall.style.backgroundImage = new StyleBackground(KeyboardSmallImg);

        // Big
        KeyboardBig = new();
        KeyboardBig.style.width = 280;
        KeyboardBig.style.height = 130;
        KeyboardBig.style.unityTextAlign = TextAnchor.UpperCenter;
        KeyboardBig.style.fontSize = 60;
        KeyboardBig.style.backgroundImage = new StyleBackground(KeyboardBigImg);
    }

    private void ControllerInit(ref Button controller, Sprite sprite){
        controller = new();
        controller.style.width = 125;
        controller.style.height = 125;
        controller.style.unityTextAlign = TextAnchor.UpperCenter;
        controller.style.fontSize = 60;
        controller.style.backgroundImage = new StyleBackground(sprite);
    }
}
