using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShatterController : MonoBehaviour {
	public GameObject ShatterPiecePrefab;
	public float ShatterForce = 2.0f;
	public float ShatterLifetime = 3.0f;
	public Vector2Int ShatterFragments = new(8, 8);
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

	public void Shatter(GameObject hitOrigin) => Shatter(hitOrigin.transform.position);

	public void Shatter(Vector3 hitOrigin) {
		for (int x = 0; x < ShatterFragments.x; x++) {
			for (int y = 0; y < ShatterFragments.y; y++) {
				Vector2 pieceOffset = new Vector2(
					x / (float)ShatterFragments.x * 2 - 1,
					y / (float)ShatterFragments.y * -2 + 1
				)
				* _spriteRenderer.bounds.size
				+ new Vector2(Random.Range(-FragmentOffsetRandom, FragmentOffsetRandom), Random.Range(-FragmentOffsetRandom, FragmentOffsetRandom));

				// Logger.Log("ShatterController", $"Shattering! origin: {hitOrigin.transform.position} dist from origin: {Vector3.Distance(transform.position, hitOrigin.transform.position)}");
				// Logger.Log("ShatterController", $"Shattering! pieceOffset: {transform.position + new Vector3(pieceOffset.x, pieceOffset.y) - hitOrigin.transform.position}");
				Vector2 hitDirection = (transform.position + new Vector3(pieceOffset.x, pieceOffset.y) - hitOrigin).normalized + Vector3.up / 2;
				// Logger.Log("ShatterController", $"Shattering! direction: {hitDirection}");

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

		Debug.Log($"dbg: {GetComponent<SpriteRenderer>().sprite.rect}");

		ShatterPieceController pc = shatterPiece.GetComponent<ShatterPieceController>();
		pc.ShatterFragments = ShatterFragments;
		pc.ShatterFragmentOffset = new Vector2Int(fragmentOffset.x, fragmentOffset.y);
		pc.ParentSpriteRenderer = GetComponent<SpriteRenderer>();
		pc.Lifetime = ShatterLifetime;

		// 		if (TryGetComponent<Animator>(out Animator animator)) {
		// #pragma warning disable IDE0008 // Use explicit type
		// 			var clipinfo = animator.GetCurrentAnimatorClipInfo(0);
		// 			var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

		// 			var currentTime = stateInfo.normalizedTime % stateInfo.length;
		// 			var currentFrame = (int)(currentTime * clipinfo[0].clip.frameRate);

		// #pragma warning restore IDE0008 // Use explicit type


		// Debug.Log($"GGG {animator.GetCurrentAnimatorStateInfo(0).normalizedTime}");
		// Debug.Log($"GGG {currentFrame}");
		// Debug.Log($"AAAAA {clipinfo[0].clip.name}");

		// for (int i = 0; i < clipinfo.Length; i++) {
		// Debug.Log($"{i} : {clipinfo[i].clip.localBounds.min}");
		// Debug.Log($"{i} : {clipinfo[i].clip.name}");
		// Debug.Log($"{i} : {clipinfo[i].clip.}");
		// if (clipinfo[i].clip.frameRate > 0) {
		// pc.GetComponent<SpriteRenderer>().sprite = clipinfo[i].clip.frames[0].sprite;
		// break;
		// }
		// }
		// Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.localBounds.min);
		// pc.GetComponent<SpriteRenderer>().sprite =
		// 	animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate > 0 ?
		// 		animator.GetCurrentAnimatorClipInfo(0)[0].clip.frames[0].sprite :
		// 		animator.GetCurrentAnimatorClipInfo(0)[0].clip.frameRate;
		// }
	}

	// 	int GetCurrentFrame() {
	// 		if (TryGetComponent<Animator>(out Animator animator)) {
	// #pragma warning disable IDE0008 // Use explicit type
	// 			var clipinfo = animator.GetCurrentAnimatorClipInfo(0);
	// 			var stateInfo = animator.GetCurrentAnimatorStateInfo(0);

	// 			var currentTime = stateInfo.normalizedTime % stateInfo.length;
	// 			var currentFrame = (int)(currentTime * clipinfo[0].clip.frameRate);

	// #pragma warning restore IDE0008 // Use explicit type


	// 			// Debug.Log($"GGG {animator.GetCurrentAnimatorStateInfo(0).normalizedTime}");
	// 			return currentFrame;
	// 		}
	// 		return 0;
	// 	}

	// 	void OnDrawGizmos() {
	// 		Handles.color = Color.red;
	// 		Handles.Label(transform.position, GetCurrentFrame().ToString());
	// 	}
}