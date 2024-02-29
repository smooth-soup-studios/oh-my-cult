using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAnimController : MonoBehaviour {
    bool _isOpen = false;
    const float _duration = .5f;

    // SX: 10 * 2 * (16/9) * .95
    // SY: 10 - (10 * 2 * 16/9 * .05)
    // OY: -5

    float _openY;
    float _closedY;

    // Start is called before the first frame update
    void Start() {
        var vww = Camera.main.orthographicSize * Camera.main.aspect;
        var mg = vww * .05;
        var fnh = Camera.main.orthographicSize - mg;

        _openY = (float)(fnh + mg) * -.5f;
        _closedY = (float)(fnh + mg) * -1.5f;
        transform.localPosition = new Vector3(0, _closedY, 0);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            // GetComponent<Animator>().SetTrigger("Next");
            ToggleOpen();
        }
    }

    public void ToggleOpen() {
        if (_isOpen) {
            transform.LeanMoveLocalY(_closedY, _duration).setEaseInCubic();
        }
        else {
            // transform.LeanMoveLocalY(-transform.localScale.y * 2, _duration).setEaseInCubic();
            transform.LeanMoveLocalY(_openY, _duration).setEaseOutCubic();
        }

        _isOpen = !_isOpen;
    }
}
