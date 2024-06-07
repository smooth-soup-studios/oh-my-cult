using UnityEngine;

public class SaveTester : MonoBehaviour {
	private void Update() {
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Minus)) {
			if (!SaveManager.Instance.HasGameData()) {
				SaveManager.Instance.ChangeSelectedProfileIdNoLoad("debug");
			}
			SaveManager.Instance.NewGame();
			SaveManager.Instance.SaveGame();
		}
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Equals)) {
			if (!SaveManager.Instance.HasGameData()) {
				SaveManager.Instance.ChangeSelectedProfileIdNoLoad("debug");
			}
			SaveManager.Instance.LoadGame();
		}
#endif
	}
}