using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ShatterPieceController : MonoBehaviour {
	[HideInInspector] public Vector2Int ShatterFragments;
	[HideInInspector] public Vector2Int ShatterFragmentOffset;
	[HideInInspector] public SpriteRenderer ParentSpriteRenderer;
	private SpriteRenderer _spriteRenderer;
	private float _timeStart;

	private void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start() {
		_timeStart = Time.time;

		// This stuff EXPLICITLY has to be in the Start function because the parent & other settings gets set after instantiation.
		_spriteRenderer.sprite = ParentSpriteRenderer.sprite;
		_spriteRenderer.transform.localScale = ParentSpriteRenderer.transform.localScale;
		_spriteRenderer.material.SetVector("_ClipSize", new Vector2(1f / ShatterFragments.x, 1f / ShatterFragments.y));
		_spriteRenderer.material.SetVector("_ClipOffset", new Vector2(1f / ShatterFragmentOffset.x, 1f / ShatterFragmentOffset.y));

		Destroy(gameObject, 5.0f);
	}

	private void Update() {
		_spriteRenderer.material.SetFloat("_GlobalAlpha", Mathf.Lerp(1f, 0f, (Time.time - _timeStart) / 5f));
	}
}