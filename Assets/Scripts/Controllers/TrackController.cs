using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers {
public class TrackController : MonoBehaviour {
	Dictionary<string, Action> _collision;
	[SerializeField] List<Event> _events;

	void Awake() {
		_collision = new Dictionary<string, Action> {
			{ "EnterChurch", PlayVillageOST },
			{ "EnterVillage", PlayChurchOST }
		};
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (_collision.TryGetValue(other.gameObject.tag, out Action callback)) {
			callback.Invoke();
		}
	}

	void PlayVillageOST() {

	}

	void PlayChurchOST() {

	}
}
}