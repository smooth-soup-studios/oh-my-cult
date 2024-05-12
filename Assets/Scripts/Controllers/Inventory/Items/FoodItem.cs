using UnityEngine;

public class FoodItem : InteractableItem {
	public FoodStats FoodStats;
	public GameObject ParticlePrefab;

	public override void PrimaryAction(StateMachine machine) {
		machine.GetComponent<HealthController>().AddHealth(FoodStats.FoodData.HealthAmount);
		machine.PlayerInventory.RemoveItem(ItemData);

		GameObject pp = Instantiate(ParticlePrefab, machine.transform);
		pp.GetComponent<ParticleSystemRenderer>().material.mainTexture = ItemData.InvData.ItemIcon.texture;
	}

	public override void SecondaryAction(StateMachine machine) {
		PrimaryAction(machine);
	}
}