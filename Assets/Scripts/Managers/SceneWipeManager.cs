using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneWipeManager : MonoBehaviour {
	enum WipeQueueType {
		IN,
		OUT
	}

	public static float WipeTime = .3f;

	private static string _logname = "SceneWipeManager";

	public static SceneWipeManager Instance;

	List<WipeQueueType> _wipeQueue = new();

	bool _wipeBusy = false;

	VisualElement _wiperRoot;

	public bool ShouldWipeOffWhenStart = false;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Logger.LogWarning(_logname, "Multiple Instances found! Exiting..");
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(Instance);

		_wiperRoot = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("wiper-root");

		SceneManager.sceneLoaded += OnSceneLoad;
	}

	void OnSceneLoad(Scene _, LoadSceneMode __) {
		if (ShouldWipeOffWhenStart) {
			WipeOff();
		}
	}

	void Update() {
		if (_wipeBusy || _wipeQueue.Count == 0) {
			return;
		}

		_wipeBusy = true;

		switch (_wipeQueue[0]) {
			case WipeQueueType.IN:
				StartCoroutine(_wipeIn());
				break;
			case WipeQueueType.OUT:
				StartCoroutine(_wipeOut());
				break;
		}

		_wipeQueue.RemoveAt(0);
	}

	public void WipeIn() {
		_wipeQueue.Add(WipeQueueType.IN);
	}

	/// <summary>
	/// This method was renamed because of copyright for the famous TV-program 'Wipeout'.
	/// </summary>
	public void WipeOff() {
		_wipeQueue.Add(WipeQueueType.OUT);
	}

	public void WipeInAndOff() {
		WipeIn();
		WipeOff();
	}

	IEnumerator _wipeIn() {
		_wiperRoot.RemoveFromClassList("up");
		_wiperRoot.AddToClassList("down");
		yield return new WaitForSeconds(.01f);

		_wiperRoot.AddToClassList("show");
		_wiperRoot.RemoveFromClassList("down");

		yield return new WaitForSeconds(WipeTime - .02f);


		_wipeBusy = false;
	}

	IEnumerator _wipeOut() {
		_wiperRoot.RemoveFromClassList("down");
		yield return new WaitForSeconds(.01f);

		_wiperRoot.AddToClassList("show");
		_wiperRoot.AddToClassList("up");

		yield return new WaitForSeconds(WipeTime - .02f);

		_wiperRoot.RemoveFromClassList("show");
		_wiperRoot.RemoveFromClassList("up");

		_wipeBusy = false;
	}

	public void ResetCurrentWipe() {
		_wiperRoot.RemoveFromClassList("show");
		_wiperRoot.RemoveFromClassList("up");
		_wiperRoot.RemoveFromClassList("down");
		_wipeBusy = false;
	}

	public void StopAndClearWipes() {
		_wipeQueue.Clear();
		ResetCurrentWipe();
	}
}
