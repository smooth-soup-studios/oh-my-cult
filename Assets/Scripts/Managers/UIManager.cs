using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour {
	public static UIManager Instance { get; private set; }

	public bool HasPlaytestKey = false;
	public float Health = 1;
	[HideInInspector] public float DashStart = -(PlayerDashState.DashCooldown + PlayerDashState.DashDuration);

	[SerializeField] private Color _dashReadyColor = new Color(0f, 1f, 0.5f);
	[SerializeField] private Color _dashUnreadyColor = new Color(0f, 0.5f, 1f);

	private Inventory _playerInventory;
	private VisualElement _root;
	private VisualElement _healthBarValue;
	private VisualElement _dashBarValue;
	private VisualElement _keyIndicator;
	private VisualElement _hotbar;
	private Color _borderColor = new Color(33f / 255f, 15f / 255f, 59f / 255f);


	private void Awake() {

		if (Instance == null) {
			Instance = this;
			_root = GetComponent<UIDocument>().rootVisualElement;
			_healthBarValue = _root.Q<VisualElement>("health-bar-value");
			_dashBarValue = _root.Q<VisualElement>("Dash-cooldown-value");
			_keyIndicator = _root.Q<VisualElement>("key-indicator");
			_hotbar = _root.Q<VisualElement>("Hotbar");
			_playerInventory = FindFirstObjectByType<StateMachine>().gameObject.GetComponent<Inventory>();
			GameObject.Find("PauseMenu").GetComponent<UIDocument>().rootVisualElement.visible = false;

		}
		else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	private void Start() {
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
		float timeSinceDash = Time.time - DashStart + 0.27f;
		float dashValue = timeSinceDash switch {
			float v when v < PlayerDashState.DashDuration => 100 - v / PlayerDashState.DashDuration * 100,
			float v when
			  v >= PlayerDashState.DashDuration &&
			  v < PlayerDashState.DashDuration + PlayerDashState.DashCooldown =>
			  (v - PlayerDashState.DashDuration) / PlayerDashState.DashCooldown * 100,
			_ => 100,
		};

		if (dashValue == 100) {
			_dashBarValue.style.backgroundColor = _dashReadyColor;
		}
		else {
			_dashBarValue.style.backgroundColor = _dashUnreadyColor;
		}
		_dashBarValue.style.width = new StyleLength(new Length(dashValue, LengthUnit.Percent));
	}

	private void InvUpdate() {
		// When transitioning between scenes the ref to the inventory is lost so reaquire it
		if (_playerInventory == null) {
			try {
				_playerInventory = FindFirstObjectByType<StateMachine>().PlayerInventory;
			}
			catch { return; }
		}

		for (int i = 0; i < _hotbar.childCount; i++) {
			//Sprite sprite = null;
			VisualElement itemSlot = _hotbar[i];
			Label slot = itemSlot.Q<Label>("Stack");
			if (_playerInventory.GetInventoryMaxSize() >= i) {
				ItemStack stack = _playerInventory.GetStackByIndex(i);
				if (stack.Item != null) {
					if (itemSlot.childCount < 2) {
						Image item = new() {
							sprite = stack.Item.InvData.ItemIcon
						};
						itemSlot.Add(item);
					}
					//sprite = stack.Item.InvData.ItemIcon;
					slot.text = stack.Amount.ToString();
				}
				else {
					slot.text = "";
					if (itemSlot.childCount > 1) {
						for (int x = 1; x < itemSlot.childCount; x++) {
							itemSlot.Remove(itemSlot[x]);
						}
					}
				}
			}
			// Image item = new() {
			// 	sprite = sprite
			// };
			// if (itemSlot.childCount > 1) {
			// 	for (int x = 1; x < itemSlot.childCount; x++) {
			// 		itemSlot.Remove(itemSlot[x]);
			// 	}
			// }
			// itemSlot.Add(item);
			HighlightSelectedItem(itemSlot, i);
		}
	}

	private void HighlightSelectedItem(VisualElement itemSlot, int index) {
		// If the slot is selected
		if (index == _playerInventory.GetCurrentIndex()) {
			itemSlot.style.borderBottomColor = _dashReadyColor;
			itemSlot.style.borderLeftColor = _dashReadyColor;
			itemSlot.style.borderRightColor = _dashReadyColor;
			itemSlot.style.borderTopColor = _dashReadyColor;
		}
		// If the slot was selected and needs to be cleared
		else if (itemSlot.resolvedStyle.borderBottomColor == _dashReadyColor) {
			itemSlot.style.borderBottomColor = _borderColor;
			itemSlot.style.borderLeftColor = _borderColor;
			itemSlot.style.borderRightColor = _borderColor;
			itemSlot.style.borderTopColor = _borderColor;
		}
	}
}