using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DroppingItemController : MonoBehaviour {
	public GameObject PickupPointInteractable;
	public ItemStack ItemToDrop;

	public float Lifetime = .7f;
	public float Height = 0.25f;

	private Vector3 _startPosition;
	private float _timeStart;

	private SpriteRenderer _spriteRenderer;

	void Awake() {
		if (!PickupPointInteractable.TryGetComponent(out BaseItemPickupInteractable _)) {
			throw new System.Exception("DroppingItemController: PickupPointInteractable must have a BaseItemPickupInteractable component.");
		}

		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start() {
		_timeStart = Time.time;
		_startPosition = transform.position;
		_spriteRenderer.sprite = ItemToDrop.Item.InvData.ItemIcon;
	}

	void Update() {
		float t = (Time.time - _timeStart) / Lifetime;
		float h = Mathf.Sin(t * Mathf.PI) * Height;
		transform.position = new Vector3(_startPosition.x, _startPosition.y + h, _startPosition.z);

		if (Time.time - _timeStart > Lifetime) {
			GameObject g = Instantiate(PickupPointInteractable, transform.position, Quaternion.identity);
			g.GetComponent<BaseItemPickupInteractable>().PickupStack = ItemToDrop;

			Destroy(gameObject);
		}
	}
}
