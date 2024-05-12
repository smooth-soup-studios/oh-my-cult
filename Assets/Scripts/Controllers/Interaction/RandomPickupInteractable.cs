using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomPickupInteractable : MonoBehaviour {
	private readonly static string _logname = "RandomPickupInteractable";

	[SerializeField] private MonoScript _itemScript;
	[SerializeField] private InventoryItem[] _items;

	private void Awake() {
		if (_items.Length == 0) {
			Logger.LogError(_logname, "No items to spawn");
			return;
		}

		if (_itemScript == null) {
			Logger.LogError(_logname, "No item script to spawn");
			return;
		}

		if (!_itemScript.GetClass().IsSubclassOf(typeof(BaseItemPickupInteractable))) {
			Logger.LogError(_logname, "Item script is not a subclass of ItemPickupInteractable");
			return;
		}

		InventoryItem item = _items[Random.Range(0, _items.Length)];

		BaseItemPickupInteractable interactable = gameObject.AddComponent(_itemScript.GetClass()) as BaseItemPickupInteractable;

		interactable.Item = item;

		Destroy(this);
	}

	private void OnDrawGizmos() {
		if (transform == null)
			return;
		// Rainbow gizmos!!! :D
		Gizmos.color = Color.HSVToRGB((float)EditorApplication.timeSinceStartup % 1, 1, .5f);
		Gizmos.DrawWireSphere(transform.position, 15);
	}
}