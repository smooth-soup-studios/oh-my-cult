using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[SerializeField] private float _speed = 5;

	public WeaponItem EnemyWeapon;
	private float _flytime = 5f;
	private Vector2 _targetPosition;
	private GameObject _target;
	private Vector2 _direction;

	void Start() {
		_target = GameObject.FindWithTag("Player");
		_targetPosition = _target.transform.position;
		_direction = (_targetPosition - (Vector2)transform.position).normalized;
		StartCoroutine(WaitBeforeDestroy(_flytime));
		RotateHitboxOnMove();
	}

	void Update() {
		// Move the projectile in the direction the target was when it was instantiated
		transform.position = Vector2.MoveTowards(
		transform.position,
		(Vector2)transform.position + _direction,
		_speed * Time.deltaTime);
	}

	private void RotateHitboxOnMove() {
		float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
		// Calculated angle plus adjustment to make the sprite face the right way
		transform.rotation = Quaternion.AngleAxis(angle - 224f, Vector3.forward);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		Debug.Log(other.gameObject.name);
		if (!other.CompareTag("Enemy") && other.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast")) {
			EnemyWeapon.PrimaryAction(gameObject);
			StartCoroutine(WaitBeforeDestroy(0.1f));
		}
	}

	public IEnumerator WaitBeforeDestroy(float time) {
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
