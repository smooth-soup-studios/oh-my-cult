using UnityEngine;

public class SaveTester : MonoBehaviour {
	private void Update() {
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
			if (!SaveManager.Instance.HasGameData()) {
				SaveManager.Instance.ChangeSelectedProfileIdNoLoad("debug");
			}
			SaveManager.Instance.NewGame();
			SaveManager.Instance.SaveGame();
		}
		if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
			if (!SaveManager.Instance.HasGameData()) {
				SaveManager.Instance.ChangeSelectedProfileIdNoLoad("debug");
			}
			SaveManager.Instance.LoadGame();
		}
#endif
	}
}