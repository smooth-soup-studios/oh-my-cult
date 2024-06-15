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
		RotateHitboxOnMove();

	}

	// Update is called once per frame
	void Update() {
		// Move the projectile in the direction the target was when it was instantiated

		transform.position = Vector3.MoveTowards(
		transform.position,
		_targetPosition,
		_speed * Time.deltaTime);






		// // If it exists for too long, destroy!
		// if (_flying == false) {
		// 	Destroy(gameObject);
		// }
	}
	private void RotateHitboxOnMove() {
		// float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
		// transform.rotation = Quaternion.Euler(0, 0, angle);

		Vector3 dir = _target.transform.position - transform.position;

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
		yield return new WaitForSeconds(_flytime);
		Destroy(gameObject);
	}
	public IEnumerator Destroy() {
		yield return new WaitForSeconds(0.5f);
		Destroy(gameObject);
	}
}
