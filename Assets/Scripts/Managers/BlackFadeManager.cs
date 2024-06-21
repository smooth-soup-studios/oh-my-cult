using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BlackFadeManager : MonoBehaviour {
	public static BlackFadeManager Instance { get; private set; }
	private static readonly string _logname = "BlackFadeManager";

	public bool UnblackenOnLoad = false;

	private VisualElement _blackFader;

	private TimedTween _opacityTween;

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

		GetComponent<UIDocument>().enabled = true;
		_opacityTween = new() {
			From = 0,
			To = 0,
			Duration = 1,
		};

		_blackFader = GetComponent<UIDocument>().rootVisualElement.Q("fader");
		OnLoad();
		SceneManager.sceneLoaded += (_, __) => OnLoad();
	}

	private void OnLoad() {
		GameObject _skip = GameObject.Find("SkipIntro");
		if (_skip != null) {
			_skip.GetComponent<UIDocument>().sortingOrder++;
		}

		if (UnblackenOnLoad) {
			UnblackenOnLoad = false;
			Unblacken(1);
			Update();
		}
	}

	private void Update() {
		_blackFader.style.opacity = _opacityTween.GetClamped();
	}

	public void Blacken(float duration) {
		_opacityTween.From = 0;
		_opacityTween.To = 1;
		_opacityTween.Duration = duration;
		_opacityTween.TStart = Time.time;
	}

	public void Unblacken(float duration) {
		_opacityTween.From = 1;
		_opacityTween.To = 0;
		_opacityTween.Duration = duration;
		_opacityTween.TStart = Time.time;
	}

	private void OnValidate() {
		GetComponent<UIDocument>().enabled = false;
	}
}