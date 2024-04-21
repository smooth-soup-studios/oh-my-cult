using System.Collections.Generic;
using UnityEngine;

namespace Controllers {
public class MusicChangeController : MonoBehaviour {
	Dictionary<string, EventType> _collision;

	void Awake() {
		_collision = new Dictionary<string, EventType> {
			{ "EnterChurch", EventType.ENTER_CHURCH },
			{ "EnterVillage", EventType.ENTER_VILLAGE }
		};
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (_collision.TryGetValue(other.gameObject.tag, out EventType eventType)) {
			EventBus.Instance.TriggerEvent(eventType);
		}
	}
}
}