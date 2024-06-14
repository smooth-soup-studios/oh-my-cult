using System;
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
        KeyboardSmall.style.unityTextAlign = TextAnchor.UpperCenter;
        KeyboardSmall.style.backgroundImage = new StyleBackground(KeyboardSmallImg);
        KeyboardSmall.style.marginTop = 5;
        KeyboardSmall.style.marginBottom = 30;
        KeyboardSmall.style.marginLeft = 35;
        KeyboardSmall.style.marginRight = 35;

        // Big
        KeyboardBig = new();
        KeyboardBig.style.unityTextAlign = TextAnchor.UpperCenter;
        KeyboardBig.style.backgroundImage = new StyleBackground(KeyboardBigImg);
        KeyboardBig.style.marginTop = 5;
        KeyboardBig.style.marginBottom = 30;
        KeyboardBig.style.marginLeft = 35;
        KeyboardBig.style.marginRight = 35;
    }

    private void ControllerInit(ref Button controller, Sprite sprite){
        controller = new();
        controller.style.width = 125;
        controller.style.height = 125;
        controller.style.backgroundImage = new StyleBackground(sprite);
    }

    public Button GetKeyboardButton(String buttonText){
        if(buttonText.Length > 1){
            return KeyboardBig;
        }
        return KeyboardSmall;
    }

    public Button GetControllerButton(String buttonText){
		return buttonText switch {
			"LS/Up" => LeftStick,
            "LS/Down" => LeftStick,
            "LS/Left" => LeftStick,
            "LS/Right" => LeftStick,
			"RS/Up" => RightStick,
            "RS/Down" => RightStick,
            "RS/Left" => RightStick,
            "RS/Right" => RightStick,
            "A" => A,
            "B" => B,
            "X" => X,
            "Y" => Y,
            "RB" => Rb,
            "RT" => Rt,
            "LB" => Lb,
            "Lt" => Lt,
            //"D-pad/Up" => Up,
            //"D-pad/Down" => Down,
            //"D-pad/Left" => Left,
            //"D-pad/Right" => Right,
			_ => LeftStick,
		};
	}
}