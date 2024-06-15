using UnityEngine;

public class SwordItem : WeaponItem {
	public override void PrimaryAction(GameObject source) {
		base.PrimaryAction(source);
		EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "SwordSwing");
	}

	protected override void DoPrimaryDamage(HealthController enemy) {
		base.DoPrimaryDamage(enemy);
		if (VibrationManager.Instance) {
			VibrationManager.Instance.GetOrAddLayer(VibrationLayerNames.DoPrimaryDamage, true).SetShakeThenStop(0, .75f, .25f);
		}
		EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, "SwordHit");
	}

}