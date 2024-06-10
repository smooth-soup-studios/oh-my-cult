using UnityEngine;

public class SwordItem : WeaponItem {
	public override void PrimaryAction(GameObject source) {
		base.PrimaryAction(source);
		EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "SwordSwing");
	}

	protected override void DoPrimaryDamage(HealthController enemy) {
		base.DoPrimaryDamage(enemy);
		Debug.Log("SwordItem DoPrimaryDamage");
		EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "SwordHit");
	}

}