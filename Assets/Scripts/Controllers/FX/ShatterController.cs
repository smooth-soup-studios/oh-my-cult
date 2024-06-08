using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShatterController : MonoBehaviour {
	public GameObject ShatterPiecePrefab;
	public float ShatterForce = 10.0f;
	public float ShatterLifetime = 1.0f;
	public Vector2Int ShatterFragments;
	public float FragmentOffsetRandom = 2f;

	private SpriteRenderer _spriteRenderer;

	private void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Shatter() {
		for (int x = 0; x < ShatterFragments.x; x++) {
			for (int y = 0; y < ShatterFragments.y; y++) {
				DoInit(
					new Vector2(
						Random.Range(-ShatterForce, ShatterForce),
						Random.Range(-ShatterForce, ShatterForce)
					),
					(x, y)
				);
			}
		}
	}

	public void Shatter(GameObject hitOrigin) {
		for (int x = 0; x < ShatterFragments.x; x++) {
			for (int y = 0; y < ShatterFragments.y; y++) {
				Vector2 pieceOffset = new Vector2(
					x / (float)ShatterFragments.x * 2 - 1,
					y / (float)ShatterFragments.y * -2 + 1
				)
				* _spriteRenderer.bounds.size
				+ new Vector2(Random.Range(-FragmentOffsetRandom, FragmentOffsetRandom), Random.Range(-FragmentOffsetRandom, FragmentOffsetRandom));

				Vector2 hitDirection = (transform.position + new Vector3(pieceOffset.x, pieceOffset.y) - hitOrigin.transform.position).normalized + Vector3.up / 2;

				DoInit(
					hitDirection * ShatterForce,
					(x, y)
				);
			}
		}
	}

	private void DoInit(Vector2 forceVector, (int x, int y) fragmentOffset) {
		GameObject shatterPiece = Instantiate(ShatterPiecePrefab, transform.position, Quaternion.identity);
		shatterPiece.GetComponent<Rigidbody2D>().AddForce(
			forceVector,
			ForceMode2D.Impulse
		);

		ShatterPieceController pc = shatterPiece.GetComponent<ShatterPieceController>();
		pc.ShatterFragments = ShatterFragments;
		pc.ShatterFragmentOffset = new Vector2Int(fragmentOffset.x, fragmentOffset.y);
		pc.ParentSpriteRenderer = GetComponent<SpriteRenderer>();
		pc.Lifetime = ShatterLifetime;
	}
}