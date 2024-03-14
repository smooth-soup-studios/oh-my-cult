using UnityEngine;

public class ScreenShakeExampleTrigger : MonoBehaviour {
    void Update() {
        // DEBUG
        if (Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Shake");
            ScreenShakeController.Instance.StartShake();
        }
        // END DEBUG
    }
}
