using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
public class TooltipController : MonoBehaviour {
	[SerializeField]
	private GameObject _tooltipPrefab;

	public string TooltipText = "Pick up";
	public string Key = "E";
	public TooltipType Type;

	[SerializeField] private Sprite _smallKey;

	[SerializeField] private Sprite _bigKey;

	private UIDocument _doc;
	private VisualElement _root;

	private VisualElement _container;
	private VisualElement _icon;
	private Label _keyLabel;
	private Label _label;

	private void Awake() {
		GameObject tooltip = Instantiate(_tooltipPrefab, transform);
		_doc = tooltip.GetComponent<UIDocument>();
		_root = _doc.rootVisualElement;
		_container = _root.Q<VisualElement>("container");
		_icon = _root.Q<VisualElement>("icon");
		_keyLabel = _icon.Q<Label>();
		_label = _root.Q<Label>("label");
	}


	public void ShowTooltip() {
		_container.RemoveFromClassList("hidden");
		_container.RemoveFromClassList("select");
		UpdateTooltip();
	}
	public void ShowTooltip(string text) {
		TooltipText = text;
		ShowTooltip();
	}
	public void ShowTooltip(string text, string key) {
		TooltipText = text;
		Key = key;
		Type = TooltipType.Custom;
		ShowTooltip();
	}
	public void HideTooltip() {
		_container.AddToClassList("hidden");
	}
	public void Select() {
		_container.AddToClassList("select");
		StartCoroutine(_deselect());

		IEnumerator _deselect() {
			yield return new WaitForSeconds(0.3f);
			_container.RemoveFromClassList("select");
		}
	}

	private void UpdateTooltip() {
		_label.text = TooltipText;
		SetKeyLabel(ConvertTypeToKey(Type));
		InputSystemRebindManager _userInput = FindObjectOfType<InputSystemRebindManager>();
		_userInput.TextChange(_keyLabel.text, _icon, _keyLabel);
	}

	private void SetKeyLabel(string text) {
		_keyLabel.text = text.Replace("\u0001", "").Replace(" ", "Space");
	}


	private string ConvertTypeToKey(TooltipType type) {
		InputSystemRebindManager _userInput = FindObjectOfType<InputSystemRebindManager>();
		if (_userInput == null) return Key;
		return type switch {
			TooltipType.Interact => _userInput.GetBindingDisplayString("Interact"),
			TooltipType.Attack => _userInput.GetBindingDisplayString("Primary"),
			TooltipType.Custom => Key,
			_ => "E",
		};
	}


#if UNITY_EDITOR
	private void OnValidate() {
		if (_tooltipPrefab == null) {
			GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Tooltip.prefab");
			_tooltipPrefab = prefab;
		}
		if (_bigKey == null) {
			_bigKey = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Menu/Keyboard_Key_Medium.png");
		}
		if (_smallKey == null) {
			_smallKey = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Menu/Keyboard_Key_Small.png");
		}
	}
#endif
}

public enum TooltipType {
	Interact,
	Attack,
	Custom
}