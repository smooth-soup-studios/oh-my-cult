using System;
using System.Collections;
using Managers;
using UnityEditor;
using UnityEngine;

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
		ActivateDoor(col.gameObject);
	}

	public void ActivateDoor(GameObject target) {
		if (NoEnter | _isTransporting) return;

		if (target.CompareTag("Player")) {
			StateMachine sm = target.GetComponent<StateMachine>();

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

			EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "DoorOpen");
			StartCoroutine(_loadDoor());

			IEnumerator _loadDoor() {
				if (TransportTo == TransportDestination.Special) {
					if (BlackFadeManager.Instance) {
						BlackFadeManager.Instance.Blacken(1.5f);
						yield return new WaitForSeconds(1.5f);
						BlackFadeManager.Instance.UnblackenOnLoad = true;
					}
				}
				else if (SceneWipeManager.Instance) {
					SceneWipeManager.Instance.WipeIn();
					yield return new WaitForSeconds(SceneWipeManager.WipeTime);
					SceneWipeManager.Instance.ShouldWipeOffWhenStart = true;
				}


				_isTransporting = false;

				GameManager.Instance.LoadScene(
					TransportTo switch {
						TransportDestination.House => SceneDefs.HouseInteriorLevel,
						TransportDestination.Special => SceneDefs.ChurchCutscene,
						_ => SceneDefs.VillageLevel
					}
				);
			}
		}
	}

	public void OnTriggerExit2D(Collider2D col) {
		ExitDoor(col.gameObject);
	}

	public void ExitDoor(GameObject target) {
		if (target.CompareTag("Player") && !_isTransporting) {
			target.GetComponent<StateMachine>().LatestDoor = -1;
			AlreadyActivated = false;
		}
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, Vector3.one);

		string doorString = "<color=";
		doorString += RequiresKey ? "yellow" : "green";
		doorString += ">Door" + ArbitraryId + "</color>";
		doorString += "\n<color=grey>To " + TransportTo.ToString() + "</color>";
#if UNITY_EDITOR
		Handles.color = Color.green;
		Handles.Label(transform.position, doorString, new GUIStyle() { fontSize = 12, alignment = TextAnchor.MiddleCenter, richText = true });
#endif
	}
}

public enum TransportDestination {
	House, Overworld, Special
}