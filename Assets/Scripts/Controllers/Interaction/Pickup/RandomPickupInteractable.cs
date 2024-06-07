using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomPickupInteractable : MonoBehaviour {
	private readonly static string _logname = "RandomPickupInteractable";

	[SerializeField] private GameObject _prefab;
	[SerializeField] private ItemStack[] _items;

	private void Awake() {
		ItemStack item = _items[Random.Range(0, _items.Length)];

		GameObject NewItem = Instantiate(_prefab, transform.position, transform.rotation);
		NewItem.GetComponent<BaseItemPickupInteractable>().PickupStack = item;

		Destroy(gameObject);
	}

	private void OnDrawGizmos() {
#if UNITY_EDITOR
		if (transform == null)
			return;
		// Rainbow gizmos!!! :D
		Gizmos.color = Color.HSVToRGB((float)EditorApplication.timeSinceStartup % 1, 1, .5f);
		Gizmos.DrawWireSphere(transform.position, 15);
#endif
	}
}