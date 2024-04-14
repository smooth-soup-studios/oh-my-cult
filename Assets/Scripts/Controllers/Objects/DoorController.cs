using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {
	private static string _logName = "DoorController";

	public TransportDestination TransportTo = TransportDestination.House;

	public int ArbitraryId = 0;

	public bool RequiresKey = false;

	public bool AlreadyActivated = false;

	private bool _isTransporting = false;

	public void OnTriggerEnter2D(Collider2D col) {
		if (AlreadyActivated) return;

		if (col.gameObject.CompareTag("Player")) {
			StateMachine sm = col.gameObject.GetComponent<StateMachine>();

			if (RequiresKey && !sm.HasDoorKey) {
				Logger.Log(_logName, "Player does not have key for door " + ArbitraryId);
				return;
			}

			Logger.Log(_logName, "Player entered door " + ArbitraryId);

			sm.LatestDoor = ArbitraryId;

			AlreadyActivated = true;
			_isTransporting = true;

			GameManager.Instance.LoadScene(
				TransportTo switch {
					TransportDestination.House => "houses",
					_ => "level_0_doors"
				}
			);
		}
	}

	public void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player") && !_isTransporting) {
			col.GetComponent<StateMachine>().LatestDoor = -1;
			AlreadyActivated = false;
		}
	}
}

public enum TransportDestination {
	House, Overworld, Special
}