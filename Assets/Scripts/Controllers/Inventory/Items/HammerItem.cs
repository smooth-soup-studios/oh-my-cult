using UnityEngine;

public class HammerItem : WeaponItem {
	public override void PrimaryAction(GameObject source) {
		base.PrimaryAction(source);
		// EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "HammerBonk");
	}

	protected override void DoPrimaryDamage(HealthController enemy) {
		base.DoPrimaryDamage(enemy);
		EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "HammerBonk");
	}

}