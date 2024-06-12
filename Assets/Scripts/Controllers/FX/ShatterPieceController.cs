using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ShatterPieceController : MonoBehaviour {
	[HideInInspector] public Vector2Int ShatterFragments;
	[HideInInspector] public Vector2Int ShatterFragmentOffset;
	[HideInInspector] public SpriteRenderer ParentSpriteRenderer;
	[HideInInspector] public float Lifetime = 1.0f;
	private SpriteRenderer _spriteRenderer;
	private float _timeStart;

	private float _freezeY;

	private void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start() {
		_timeStart = Time.time;

		// This stuff EXPLICITLY has to be in the Start function because the parent & other settings gets set after instantiation.
		Texture2D baseTexture = ParentSpriteRenderer.sprite.texture;
		Vector2 textureSize = new(baseTexture.width, baseTexture.height);
		Rect outerBounds = ParentSpriteRenderer.sprite.rect;
		Vector2 outerFragments = textureSize / outerBounds.size;

		Vector2 outerOffset = new(
			outerBounds.x / textureSize.x,
			1 - outerBounds.y / textureSize.y - 1 / outerFragments.y // FUCK YOU, UNITY! WE COULD'VE BEEN FRIENDS. ⚔️ THE FIGHT IS ON.
		);

		_spriteRenderer.sprite = ParentSpriteRenderer.sprite;
		_spriteRenderer.transform.localScale = ParentSpriteRenderer.transform.localScale;
		_spriteRenderer.material.SetVector("_ClipSize", new Vector2(1f / ShatterFragments.x / outerFragments.x, 1f / ShatterFragments.y / outerFragments.y));
		_spriteRenderer.material.SetTexture("_MainTex", ParentSpriteRenderer.sprite.texture);

		float offsetX = (float)ShatterFragmentOffset.x / ShatterFragments.x / outerFragments.x + outerOffset.x;
		float offsetY = (float)ShatterFragmentOffset.y / ShatterFragments.y / outerFragments.y + outerOffset.y;
		_spriteRenderer.material.SetVector("_ClipOffset", new Vector2(offsetX, offsetY));

		_freezeY = transform.position.y - _spriteRenderer.bounds.size.y * (1 - offsetY) + Random.Range(-1f, 1f);

		Destroy(gameObject, Lifetime);
	}

	private void Update() {
		_spriteRenderer.material.SetFloat("_GlobalAlpha", Mathf.Lerp(1f, 0f, (Time.time - _timeStart) / Lifetime));

		if (transform.position.y < _freezeY) {
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		}
	}
}