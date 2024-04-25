using UnityEngine;
using UnityEngine.UIElements;

namespace Managers {
	public class UIManager : MonoBehaviour {
		public static UIManager Instance { get; private set; }

		public bool HasPlaytestKey = false;
		public float Health = 1;
		public float DashStart = -(PlayerDashState.DashCooldown + PlayerDashState.DashDuration);

		private Inventory _playerInventory;
		private VisualElement _root;
		private VisualElement _healthBarValue;
		private VisualElement _dashBarValue;
		private VisualElement _keyIndicator;
		private VisualElement _hotbar;

		private void Awake() {
			if (Instance == null) {
				Instance = this;
				_root = GetComponent<UIDocument>().rootVisualElement;
				_healthBarValue = _root.Q<VisualElement>("health-bar-value");
				_dashBarValue = _root.Q<VisualElement>("Dash-cooldown-value");
				_keyIndicator = _root.Q<VisualElement>("key-indicator");
				_hotbar = _root.Q<VisualElement>("Hotbar");
				_playerInventory = FindFirstObjectByType<StateMachine>().gameObject.GetComponent<Inventory>();
			}
			else {
				Destroy(gameObject);
			}

			DontDestroyOnLoad(gameObject);
		}

		private void Update() {
			_healthBarValue.style.width = new StyleLength(new Length(Health, LengthUnit.Percent));
			_keyIndicator.style.visibility = HasPlaytestKey ? Visibility.Visible : Visibility.Hidden;

			DashUpdate();
			InvUpdate();
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

		private void DashUpdate() {
			float timeSinceDash = Time.time - DashStart;
			float dashValue = timeSinceDash switch {
				float v when v < PlayerDashState.DashDuration => 100 - v / PlayerDashState.DashDuration * 100,
				float v when
					v >= PlayerDashState.DashDuration &&
					v < PlayerDashState.DashDuration + PlayerDashState.DashCooldown =>
					(v - PlayerDashState.DashDuration) / PlayerDashState.DashCooldown * 100,
				_ => 100,
			};

			_dashBarValue.style.width = new StyleLength(new Length(dashValue, LengthUnit.Percent));
		}

		private void InvUpdate() {
			for (int i = 0; i < _hotbar.childCount; i++) {
				Sprite sprite = null;
				if (_playerInventory.GetInventoryMaxSize() >= i) {
					if (_playerInventory.GetItemByIndex(i) != null) {
						sprite = _playerInventory.GetItemByIndex(i).InvData.ItemIcon;
					}
				}
				Image item = new Image();
				item.sprite = sprite;
				VisualElement itemSlot = _hotbar[i];
				if (itemSlot.childCount > 0) {
					for (int x = 0; x < itemSlot.childCount; i++) {
						itemSlot.Remove(itemSlot[x]);
					}
				}
				itemSlot.Add(item);
			}
		}
	}
}