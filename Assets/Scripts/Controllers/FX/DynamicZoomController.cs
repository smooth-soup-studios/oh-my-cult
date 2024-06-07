using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
public class DynamicZoomController : MonoBehaviour {
	public GameObject CombatCenterFollow;
	public float ZoomSpeed = 5;
	public float MinZoom = 80;
	public float MaxZoom = 95;

	private List<GameObject> _enemies = new();
	private CinemachineVirtualCamera _virtualCamera;
	private CircleCollider2D _collider;

	void Awake() {
		Debug.Log("DynamicZoomController Awak");
		if (CombatCenterFollow == null) {
			CombatCenterFollow = GameObject.Find("CombatCenterFollow");
		}
		if (!TryGetComponent(out _collider)) {
			throw new System.Exception("DynamicZoomController must have a CircleCollider2D component");
		}

	}

	// Start is called before the first frame update
	// IEnumerable Start() {
	void Start() {
		// yield return new WaitUntil(() => CinemachineCore.Instance.GetActiveBrain(0) != null);
		Debug.Log("DynamicZoomController Start");
	}

	// Update is called once per frame
	void Update() {
		_virtualCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;

		if (_virtualCamera == null) {
			return;
		}

		CombatCenterFollow.transform.localPosition = CenterPoint();
		float targetZoom = Mathf.Clamp(Mathf.Lerp(50, 95, MaxDist().magnitude / _collider.radius), MinZoom, MaxZoom);
		_virtualCamera.m_Lens.OrthographicSize =
		 Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, targetZoom, Time.deltaTime * 5);
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
			// Debug.Log("Enemy enter");
			// _virtualCamera.m_Lens.OrthographicSize = 50;
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			_enemies.Remove(other.gameObject);
			// Debug.Log("Enemy exit");
			// _virtualCamera.m_Lens.OrthographicSize = 95;
		}
	}
}
