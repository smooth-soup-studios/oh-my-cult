using System.Collections.Generic;
using UnityEngine;

public class DoorCameraPoint : MonoBehaviour {
	private List<DoorController> _doors = new();
	[SerializeField] private DoorController _assignedDoor;
}