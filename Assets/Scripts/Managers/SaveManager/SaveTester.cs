using Managers;
using UnityEngine;

public class SaveTester : MonoBehaviour {
	private void Update() {
		if (Input.GetKeyDown(KeyCode.L)) {
			SaveManager.Instance.NewGame();
			SaveManager.Instance.SaveGame();
		}
	}
}