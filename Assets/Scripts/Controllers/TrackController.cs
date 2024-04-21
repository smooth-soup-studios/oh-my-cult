using Managers;
using UnityEngine;

namespace Controllers {
public class TrackController : MonoBehaviour {
	[SerializeField] AudioClip churchMusic;
	[SerializeField] AudioClip villageMusic;
	void OnEnable() {
		EventBus.Instance.Subscribe(EventType.ENTER_VILLAGE, OnPlayerEntersVillageArea);
		EventBus.Instance.Subscribe(EventType.ENTER_CHURCH, OnPlayerEntersChurchArea);
	}

	void OnDisable() {
		EventBus.Instance.Unsubscribe(EventType.ENTER_VILLAGE, OnPlayerEntersVillageArea);
		EventBus.Instance.Unsubscribe(EventType.ENTER_CHURCH, OnPlayerEntersChurchArea);
	}
	void OnPlayerEntersChurchArea() {
		AudioManager.Instance.PlayTrack(churchMusic, 1f, 1f, 0f, 112f);
	}

	void OnPlayerEntersVillageArea() {
		AudioManager.Instance.PlayTrack(villageMusic, 1f, 1f, 0f, 10f);
	}
}
}