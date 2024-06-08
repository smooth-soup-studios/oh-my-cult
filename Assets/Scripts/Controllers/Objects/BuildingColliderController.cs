using Controllers.Player;
using UnityEngine;

namespace Controllers {
public class BuildingColliderController : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name == "Player") {
			EventBus.Instance.TriggerEvent(PlayerEvents.EnterBuildingCover);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.name == "Player") {
			EventBus.Instance.TriggerEvent(PlayerEvents.ExitBuildingCover);
		}
	}
}
}