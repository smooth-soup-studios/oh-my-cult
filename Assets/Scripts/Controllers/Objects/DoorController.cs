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

	[Tooltip("If true, this door is only used as a target to teleport to when the player enters this scene from another door, and cannot be used to teleport to another scene.")]
	public bool NoEnter = false;

	public bool AlreadyActivated = false;

	private bool _isTransporting = false;

	public void OnTriggerEnter2D(Collider2D col) {
		if (AlreadyActivated || NoEnter) return;

		if (col.gameObject.CompareTag("Player")) {
			StateMachine sm = col.gameObject.GetComponent<StateMachine>();

			if (RequiresKey) {
				if (!sm.HasDoorKey) {
					UIManager.Instance.ShowDialogBox("Locked Door", "You need a key to unlock this door.", 2f);
					Logger.Log(_logName, "Player does not have key for door " + ArbitraryId);
					return;
				}
				else {
					sm.HasDoorKey = false;
				}
			}

			Logger.Log(_logName, "Player entered door " + ArbitraryId);

			sm.LatestDoor = ArbitraryId;

			AlreadyActivated = true;
			_isTransporting = true;
			
			GameManager.Instance.LoadScene(
				TransportTo switch {
					TransportDestination.House => "houses",
					TransportDestination.Special => "boss_arenas",
					_ => "level_0"
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