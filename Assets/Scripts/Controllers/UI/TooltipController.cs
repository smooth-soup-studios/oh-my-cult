using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
public class TooltipController : MonoBehaviour {
	[SerializeField]
	private GameObject _tooltipPrefab;

	public string TooltipText = "Pick up";
	private string _key = "E";
	public TooltipType Type;

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

	private void Update() {
		_label.text = TooltipText;
		_keyLabel.text = ConvertTypeToKey(Type);
	}

	public void ShowTooltip() {
		_container.RemoveFromClassList("hidden");
		_container.RemoveFromClassList("select");
	}
	public void ShowTooltip(string text) {
		TooltipText = text;
		_container.RemoveFromClassList("hidden");
	}
	public void ShowTooltip(string text, string key) {
		TooltipText = text;
		_key = key;
		Type = TooltipType.Custom;
		_container.RemoveFromClassList("hidden");
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


	private string ConvertTypeToKey(TooltipType type) {
		InputSystemRebindManager _userInput = FindObjectOfType<InputSystemRebindManager>();
		if (_userInput == null) return _key;
		return type switch {
			TooltipType.Interact => _userInput.GetBindingDisplayString("Interact", -1),
			TooltipType.Attack => _userInput.GetBindingDisplayString("Primary", -1),
			TooltipType.Custom => _key,
			_ => "E",
		};
	}

#if UNITY_EDITOR
	private void OnValidate() {
		if (_tooltipPrefab == null) {
			GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/Tooltip.prefab");
			_tooltipPrefab = prefab;
		}
	}
#endif
}

public enum TooltipType {
	Interact,
	Attack,
	Custom
}