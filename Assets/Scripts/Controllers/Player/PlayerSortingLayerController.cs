using System;
using UnityEngine;

namespace Controllers.Player {
public class PlayerSortingLayerController : MonoBehaviour {
	SpriteRenderer _spriteRenderer;

	void Start() {
		_spriteRenderer = GetComponent<SpriteRenderer>();

		EventBus.Instance.Subscribe(PlayerEvents.EnterBuildingCover, EnterBuildingCover);
		EventBus.Instance.Subscribe(PlayerEvents.ExitBuildingCover, ExitBuildingCover);
	}

	void OnDestroy() {
		EventBus.Instance.Unsubscribe(PlayerEvents.EnterBuildingCover, EnterBuildingCover);
		EventBus.Instance.Unsubscribe(PlayerEvents.ExitBuildingCover, ExitBuildingCover);
	}

	void EnterBuildingCover() {
		_spriteRenderer.sortingLayerName = "BehindStructure";
	}

	void ExitBuildingCover() {
		_spriteRenderer.sortingLayerName = "Player";
	}
}
}