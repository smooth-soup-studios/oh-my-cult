using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class DynamicZoomController : MonoBehaviour {
	public GameObject CombatCenterFollow;
	public bool FrozenOnPlayer = false;
	public bool ZoomInOnAction = true;

	public float ZoomSpeed = 1;
	public float MinZoom = 5;
	public float MaxZoom = 7;

	private List<GameObject> _enemies = new();
	private CinemachineVirtualCamera _virtualCamera;
	private CircleCollider2D _collider;

	void Awake() {
		if (CombatCenterFollow == null) {
			CombatCenterFollow = GameObject.Find("CombatCenterFollow");
		}
		_collider = GetComponent<CircleCollider2D>();

	}

	void Update() {
		// Here because it would not find it on Awake nor on Start
		_virtualCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;

		if (_virtualCamera == null) {
			return;
		}

		float targetZoom = ZoomInOnAction ? MaxZoom : MinZoom;

		if (FrozenOnPlayer) {
			CombatCenterFollow.transform.localPosition = Vector2.zero;
		}
		else {
			CombatCenterFollow.transform.localPosition = CenterPoint();
			if (ZoomInOnAction)
				targetZoom = Mathf.Clamp(Mathf.Lerp(MinZoom, MaxZoom, MaxDist().magnitude / _collider.radius), MinZoom, MaxZoom);
			else {
				targetZoom = Mathf.Clamp(Mathf.Lerp(MaxZoom, MinZoom, MaxDist().magnitude / _collider.radius), MinZoom, MaxZoom);
			}
		}

		_virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, targetZoom, Time.deltaTime * ZoomSpeed);
	}

	Vector2 MaxDist() {
		if (_enemies.Count == 0) {
			return _collider.radius * Vector2.one;
		}

		Vector2 maxDist = Vector2.zero;
		foreach (GameObject enemy in _enemies) {
			Vector2 dist = (Vector2)(enemy.transform.position - transform.TransformPoint(Vector3.zero));
			if (dist.magnitude > maxDist.magnitude) {
				maxDist = dist;
			}
		}
		return maxDist;
	}

	Vector2 CenterPoint() {
		Vector2 center = Vector2.zero;
		foreach (GameObject enemy in _enemies) {
			center += (Vector2)(enemy.transform.position - transform.TransformPoint(Vector3.zero));
		}
		center /= _enemies.Count + 1;
		return center;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			_enemies.Add(other.gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			_enemies.Remove(other.gameObject);
		}
	}
}
