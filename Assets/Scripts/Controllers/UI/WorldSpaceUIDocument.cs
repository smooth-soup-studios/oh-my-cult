using UnityEngine;
using UnityEngine.UIElements;

public class WorldSpaceUIDocument : MonoBehaviour {
	private UIDocument _doc;
	private VisualElement _root;

	private void Start() {
		_doc = GetComponent<UIDocument>();
		_root = _doc.rootVisualElement;
	}

	private void Update() {
		Vector3 screenSpacePosition = new Vector2(Screen.width / 2, Screen.height / 2);
		Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.TransformPoint(Vector3.zero)) - screenSpacePosition;
		Vector2 normalisedScreenPos = _doc.panelSettings.referenceResolution / new Vector2(Screen.width, Screen.height) * screenPos;
		_root.transform.position = new Vector3(normalisedScreenPos.x, -normalisedScreenPos.y, 0);
	}
}