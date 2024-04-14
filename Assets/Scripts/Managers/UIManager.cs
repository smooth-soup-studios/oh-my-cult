using UnityEngine;
using UnityEngine.UIElements;

namespace Managers {
	public class UIManager : MonoBehaviour {
		public static UIManager Instance { get; private set; }

		// [SerializeField] private bool _hasPlaytestKey = false;
		public bool HasPlaytestKey = false;
		public float Health = 1;

		private VisualElement _root;
		private VisualElement _healthBarValue;
		private VisualElement _keyIndicator;

		private void Awake() {
			if (Instance == null) {
				Instance = this;
				_root = GetComponent<UIDocument>().rootVisualElement;
				_healthBarValue = _root.Q<VisualElement>("health-bar-value");
				_keyIndicator = _root.Q<VisualElement>("key-indicator");
			}
			else {
				Destroy(gameObject);
			}

			DontDestroyOnLoad(gameObject);
		}

		private void Update() {
			_healthBarValue.style.width = new StyleLength(new Length(Health * 100, LengthUnit.Percent));
			_keyIndicator.style.visibility = HasPlaytestKey ? Visibility.Visible : Visibility.Hidden;
		}

		public void ToggleDialogBox() {
			if (_root.Q<VisualElement>("dialog-box").ClassListContains("closed")) {
				ShowDialogBox();
			}
			else {
				HideDialogBox();
			}
		}

		public void ShowDialogBox() {
			_root.Q<VisualElement>("dialog-box").RemoveFromClassList("closed");
		}

		public void ShowDialogBox(string title, string message) {
			_root.Q<Label>("dialog-title").text = title;
			_root.Q<Label>("dialog-content").text = message;
			ShowDialogBox();
		}

		public void ShowDialogBox(string title, string message, float duration) {
			_root.Q<Label>("dialog-title").text = title;
			_root.Q<Label>("dialog-content").text = message;
			ShowDialogBox();
			Invoke(nameof(HideDialogBox), duration);
		}

		public void HideDialogBox() {
			_root.Q<VisualElement>("dialog-box").AddToClassList("closed");
		}

		public void DebugCycleHealth() {
			Health = 1 - ((1 - Health + .1f) % 1);
		}
	}
}