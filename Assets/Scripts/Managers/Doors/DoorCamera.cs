using UnityEngine;

public class DoorCameraPoint : MonoBehaviour {
	[Header("Settings")]
	public DoorController AssignedDoor;
	public bool TrackTarget = false;
	public Transform TrackingPoint;
	public Transform TrackingTarget;
	public Bounds BoundingBox;


	private void Awake() {
		// If no point has been assigned, create it.
		if (TrackingPoint == null) {
			GameObject go = Instantiate(new GameObject(), transform);
			go.name = "CameraTrackingPoint";
			TrackingPoint = go.transform;
		}
		if (TrackingTarget == null) {
			TrackingTarget = FindAnyObjectByType<StateMachine>().gameObject.transform;
		}
	}

	private void Update() {
		if (TrackTarget) {
			TrackingPoint.position = BoundingBox.ClosestPoint(TrackingTarget.transform.position);
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(transform.position, Vector3.one * 3);

		if (TrackTarget) {
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireCube(transform.position, BoundingBox.size);
		}
	}
}