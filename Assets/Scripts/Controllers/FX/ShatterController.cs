using UnityEngine;

public class ShatterController : MonoBehaviour {
	public GameObject ShatterPiecePrefab;
	public float ShatterForce = 10.0f;
	public Vector2Int ShatterFragments;
	public int ShatterFragmentsX = 10;
	public int ShatterFragmentsY = 10;

	public void Shatter() {
		int count = 0;
		for (int x = 0; x < ShatterFragments.x; x++) {
			for (int y = 0; y < ShatterFragments.y; y++) {
				GameObject shatterPiece = Instantiate(ShatterPiecePrefab, transform.position, Quaternion.identity);
				shatterPiece.GetComponent<Rigidbody2D>().AddForce(
					new Vector2(
						Random.Range(-ShatterForce, ShatterForce),
						Random.Range(-ShatterForce, ShatterForce)
						),
					ForceMode2D.Impulse
				);

				ShatterPieceController pc = shatterPiece.GetComponent<ShatterPieceController>();
				pc.ShatterFragments = ShatterFragments;
				pc.ShatterFragmentOffset = new Vector2Int(x, y);
				pc.ParentSpriteRenderer = GetComponent<SpriteRenderer>();

				count++;
			}
		}

		Debug.Log("yeah " + count);
	}
}