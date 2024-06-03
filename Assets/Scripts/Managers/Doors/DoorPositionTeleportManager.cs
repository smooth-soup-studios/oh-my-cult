using UnityEngine;
using UnityEngine.SceneManagement;

public class HousePositionTeleportManager : MonoBehaviour {
	private static string _logName = "HousePositionTeleportManager";
	const int _yOffset = 0;

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
			plr.transform.position = new Vector2(position.x, position.y - _yOffset);

			Logger.Log(_logName, "Teleported to door " + plrsm.LatestDoor + " at " + plr.transform.position);
		}


		// Hacky way to reassign the camera follow target, but works for playtest purposes :D
		GameObject.Find("Vcam-Player").GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = plr.transform;
		plrsm.LatestDoor = -1;
	}
}