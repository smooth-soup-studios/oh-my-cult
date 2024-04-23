using UnityEngine;
using UnityEngine.UIElements;

namespace Managers {
	public class UIManager : MonoBehaviour {
		public static UIManager Instance { get; private set; }

		// [SerializeField] private bool _hasPlaytestKey = false;
		public bool HasPlaytestKey = false;
		public float Health = 1;
		public float Dash = 1;

		private float _speed = 0.3f;

		[HideInInspector] public Inventory PlayerInventory { get; private set; }

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
				PlayerInventory = GetComponent<Inventory>();
			}
			else {
				Destroy(gameObject);
			}

			DontDestroyOnLoad(gameObject);
		}

		private void Update() {
			_healthBarValue.style.width = new StyleLength(new Length(Health , LengthUnit.Percent));
			_dashBarValue.style.width = new StyleLength(new Length(Dash , LengthUnit.Percent));
			_keyIndicator.style.visibility = HasPlaytestKey ? Visibility.Visible : Visibility.Hidden;

			DashCheck();
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

		public bool OnDash(bool dash){
			
			if (Dash >= 100f){
				Dash = 1;
				_dashBarValue.style.backgroundColor = new Color(0f, 0.5f, 1f);
				return dash;
			}
			return false;
		}

		private void DashCheck(){
			if(Dash < 100){
				Dash = Mathf.MoveTowards(Dash, 100, _speed);
			}else{
				_dashBarValue.style.backgroundColor = new Color(1f, 1f, 0f);
			}
		}

		private void InvUpdate(){
			for(int i = 0;i <_hotbar.childCount ;i++){
				Sprite sprite = PlayerInventory.GetItemByIndex(i).InvData.ItemIcon;
				Image item = new Image();
				item.sprite = sprite;
				VisualElement itemSlot = _hotbar[i];
				if(itemSlot.childCount > 0){
					for(int x = 0;x <itemSlot.childCount ;i++){
						itemSlot.Remove(itemSlot[i]);
					}
				}
				itemSlot.Add(item);
			}
		}
	}
}