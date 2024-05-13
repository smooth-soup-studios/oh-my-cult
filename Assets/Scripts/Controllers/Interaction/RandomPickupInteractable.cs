using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomPickupInteractable : MonoBehaviour {
	private readonly static string _logname = "RandomPickupInteractable";

	[SerializeField] private GameObject _prefab;
	[SerializeField] private InventoryItem[] _items;

	private void Awake() {

		InventoryItem item = _items[Random.Range(0, _items.Length)];

		// BaseItemPickupInteractable interactable = gameObject.AddComponent(_itemScript.GetClass()) as BaseItemPickupInteractable;
		GameObject NewItem = Instantiate(_prefab, transform.position, transform.rotation);
		NewItem.GetComponent<BaseItemPickupInteractable>().Item = item;

		Destroy(gameObject);
	}

	private void OnDrawGizmos() {
		if (transform == null)
			return;
		// Rainbow gizmos!!! :D
		Gizmos.color = Color.HSVToRGB((float)EditorApplication.timeSinceStartup % 1, 1, .5f);
		Gizmos.DrawWireSphere(transform.position, 15);
	}
}