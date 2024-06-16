using UnityEngine;

[RequireComponent(typeof(Dropper))]
public class HammerInTheRock : MonoBehaviour {
	[SerializeField, Range(1, 100)] private int _hitsTillBreak = 1;
	[SerializeField, Range(0, 50)] private int _shakeIntensity = 25;

	private void Awake() {
		EventBus.Instance.Subscribe<(GameObject target, GameObject source)>(EventType.HIT, OnHit);
	}



	protected void OnHit((GameObject target, GameObject source) arg) {
		GameObject target = arg.target;

		if (target == gameObject) {
			EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "RockHit");
			if (ScreenShakeManager.Instance) {
				ShakeLayer DamageShakeLayer = ScreenShakeManager.Instance.GetOrAddLayer("RockShake", true);
				DamageShakeLayer.SetShakeThenStop(_shakeIntensity / _hitsTillBreak + 1, 2);
			}
			_hitsTillBreak -= 1;
			if (_hitsTillBreak == 0) {
				Break();
			}
		}

	}

	protected void Break() {
		gameObject.SetActive(false);
		GetComponent<Dropper>().Drop();
	}


}