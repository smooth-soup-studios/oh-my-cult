using UnityEngine;
using UnityEngine.UIElements;


public class PreMadeMovementButtons : MonoBehaviour
{
    //Keyboard
    private Button _keyboardSmall;
    private Button _keyboardBig;

    //Controller
    private Button _leftStick;
    private Button _rightStick;
    private Button _a;
    private Button _b;
    private Button _x;
    private Button _y;
    private Button _rb;
    private Button _rt;
    private Button _lb;
    private Button _lt;
    private Button _up;
    private Button _down;
    private Button _left;
    private Button _right;

    [Tooltip("Scaling buttons. Make 1 for normal menu, 0.78 for in-game menu!")]
    public float Scale;

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
        ControllerInit(ref _leftStick, LeftStickImg);
        ControllerInit(ref _rightStick, RightStickImg);
        ControllerInit(ref _a, AImg);
        ControllerInit(ref _b, BImg);
        ControllerInit(ref _x, XImg);
        ControllerInit(ref _y, YImg);
        ControllerInit(ref _rb, RbImg);
        ControllerInit(ref _rt, RtImg);
        ControllerInit(ref _lb, LbImg);
        ControllerInit(ref _lt, LtImg);
        ControllerInit(ref _up, UpImg);
        ControllerInit(ref _down, DownImg);
        ControllerInit(ref _left, LeftImg);
        ControllerInit(ref _right, RightImg);
    }

    private void KeyboardInit(){
        if(Scale == 0){
            Scale = 1;
        }

        // Small
        _keyboardSmall = new();
        _keyboardSmall.style.width = 140 * Scale;
        _keyboardSmall.style.height = 130 * Scale;
        _keyboardSmall.style.backgroundImage = new StyleBackground(KeyboardSmallImg);
        // _keyboardSmall.style.marginTop = 5;
        // _keyboardSmall.style.marginBottom = 30;
        // _keyboardSmall.style.marginLeft = 35;
        // _keyboardSmall.style.marginRight = 35;

        // Big
        _keyboardBig = new();
        _keyboardBig.style.width = 280 * Scale;
        _keyboardBig.style.height = 130 * Scale;
        _keyboardBig.style.backgroundImage = new StyleBackground(KeyboardBigImg);
        // _keyboardBig.style.marginTop = 5;
        // _keyboardBig.style.marginBottom = 30;
        // _keyboardBig.style.marginLeft = 35;
        // _keyboardBig.style.marginRight = 35;
    }

    private void ControllerInit(ref Button controller, Sprite sprite){
        if(Scale == 0){
            Scale = 1;
        }

        controller = new();
        controller.style.width = 125 * Scale;
        controller.style.height = 125 * Scale;
        controller.style.backgroundImage = new StyleBackground(sprite);
    }

    public Button GetKeyboardButton(string buttonText){
        if(buttonText.Length > 1){
            return _keyboardBig;
        }
        return _keyboardSmall;
    }

    public Button GetControllerButton(string buttonText){
		return buttonText switch {
			"LS/Up" => _leftStick,
            "LS/Down" => _leftStick,
            "LS/Left" => _leftStick,
            "LS/Right" => _leftStick,
			"RS/Up" => _rightStick,
            "RS/Down" => _rightStick,
            "RS/Left" => _rightStick,
            "RS/Right" => _rightStick,
            "A" => _a,
            "B" => _b,
            "X" => _x,
            "Y" => _y,
            "RB" => _rb,
            "RT" => _rt,
            "LB" => _lb,
            "Lt" => _lt,
            //"D-pad/Up" => _up,
            //"D-pad/Down" => _down,
            //"D-pad/Left" => _left,
            //"D-pad/Right" => _right,
			_ => _leftStick,
		};
	}
}