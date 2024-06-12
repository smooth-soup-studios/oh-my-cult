using System;
using System.Linq;
using UnityEngine;

public class DoorPositionTeleportManager : MonoBehaviour {
	private static string _logName = "DoorPositionTeleportManager";

	[Header("Settings")]
	[SerializeField] private bool _useCameraSystem = true;

	void Start() {
		GameObject plr = GameObject.FindGameObjectWithTag("Player");
		StateMachine plrsm = plr.GetComponent<StateMachine>();

		if (plrsm.LatestDoor == -1) return;

		GameObject[] doors = GameObject.FindGameObjectsWithTag("DoorInteractor");

		foreach (GameObject door in doors) {
			DoorController dc = door.GetComponent<DoorController>();
			if (dc.ArbitraryId != plrsm.LatestDoor) continue;

			dc.AlreadyActivated = true;
			Vector3 position = door.transform.position;
			plr.transform.position = (Vector2)position;

			Logger.Log(_logName, "Teleported to door " + plrsm.LatestDoor + " at " + plr.transform.position);
		}


		// Hacky way to reassign the camera follow target, but works for playtest purposes :D
		// It's still hacky but now we have a nice method to handle it :) -W
		if (_useCameraSystem) {
			HandleCameras(plr, plrsm.LatestDoor);
		}
		plrsm.LatestDoor = -1;
	}

	protected void HandleCameras(GameObject player, int lastDoor) {
		DoorCameraPoint foundCamPoint = FindObjectsOfType<DoorCameraPoint>().FirstOrDefault(x => x.AssignedDoor.ArbitraryId == lastDoor);
		Cinemachine.CinemachineVirtualCamera vcam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
		Camera mainCam = FindObjectOfType<Camera>();

		Transform target = foundCamPoint ? foundCamPoint.TrackingPoint : player.transform;

		if (vcam) {
			vcam.Follow = target;
		}
		else if (mainCam) {
			mainCam.transform.parent = target;
			mainCam.transform.position = target.position + Vector3.back;
		}
		else {
			Logger.LogWarning(_logName, "No camera found in scene! You should probably add one.");
		}
	}
}