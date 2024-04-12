using UnityEngine;
using UnityEngine.SceneManagement;

public class HousePositionTeleportManager : MonoBehaviour {
	private static string _logName = "HousePositionTeleportManager";

	void Start() {
		GameObject plr = GameObject.FindGameObjectWithTag("Player");
		StateMachine plrsm = plr.GetComponent<StateMachine>();

		if (plrsm.LatestDoor == -1) return;

		switch (SceneManager.GetActiveScene().name) {
			case "level_0_doors": // overworld
				GameObject[] doors = GameObject.FindGameObjectsWithTag("DoorInteractor");

				foreach (GameObject door in doors) {
					DoorController dc = door.GetComponent<DoorController>();
					if (dc.ArbitraryId == plrsm.LatestDoor) {
						dc.AlreadyActivated = true;
						plr.transform.position = door.transform.position;

						Logger.Log(_logName, "Teleported to door " + plrsm.LatestDoor + " at " + plr.transform.position);
						return;
					}
				}
				break;
			default: // houses || special
				plr.transform.position = new Vector3((float)plrsm.LatestDoor * 1000, 0, 0);
				Logger.Log(_logName, "Teleported to house " + plrsm.LatestDoor);
				break;
		}
	}
}