using UnityEngine;

public class SwordItem : InteractableItem {
	[SerializeField] private WeaponStats _weaponStats;

	public override void PrimaryAction(GameObject source) {
		Logger.Log("Sword", "Primary action");
	}

	public override void SecondaryAction(GameObject source) {
		Logger.Log("Sword", "Secondary action");
	}
}