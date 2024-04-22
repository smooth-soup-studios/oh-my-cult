using UnityEngine;

public class SaveTester : MonoBehaviour {
	private void Update() {
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.L)) {
			SaveManager.Instance.NewGame();
			SaveManager.Instance.SaveGame();
		}
#endif
	}
}