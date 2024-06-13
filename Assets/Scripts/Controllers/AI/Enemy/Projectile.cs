using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour {
	// Start is called before the first frame update
	[SerializeField] private float _speed = 2;
	[Tooltip("If an projectile is this much away from its origin, it gets destroyed :).")]

	public WeaponItem EnemyWeapon;
	private float _destroyDistance = 5;
	private float _flytime = 2f;
	private bool _flying;

	//   public BaseBehaviourTree Enemy;
	private Vector2 _targetPosition;
	private Vector2 _startPosition;
	private GameObject _target;
	void Start() {
		_target = GameObject.FindWithTag("Player");
		_targetPosition = _target.transform.position;
		_startPosition = transform.position;
		StartCoroutine(FlyTime());
	}

	// Update is called once per frame
	void Update() {
		// Move the projectile in the direction the target was when it was instantiated

		transform.position = Vector3.MoveTowards(
		transform.position,
		_targetPosition,
		_speed * Time.deltaTime);
		RotateHitboxOnMove(Vector2.Min(transform.position, _targetPosition));




		// // If it exists for too long, destroy!
		// if (Vector2.Distance(transform.position, _startPosition) > _destroyDistance || _flying == false) {
		// 	Destroy(gameObject);
		// }
	}
	private void RotateHitboxOnMove(Vector2 movement) {
		float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag("Enemy") && other.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast")) {
			Logger.Log("Arrow", "Destroy");
			Debug.Log(other.gameObject.name);
			EnemyWeapon.PrimaryAction(gameObject);
			StartCoroutine(Destroy());

		}
	}

	public IEnumerator FlyTime() {
		_flying = true;
		yield return new WaitForSeconds(_flytime);
		_flying = false;
	}
	public IEnumerator Destroy() {
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
