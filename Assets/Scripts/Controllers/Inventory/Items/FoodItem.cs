using UnityEngine;

public class FoodItem : InteractableItem {
	public FoodStats FoodStats;
	public GameObject ParticlePrefab;

	public override void PrimaryAction(GameObject source) {
		if (source.TryGetComponent<Inventory>(out Inventory inv) && source.TryGetComponent<HealthController>(out HealthController hpcontroller)) {
			inv.RemoveItem(ItemData);
			hpcontroller.AddHealth(FoodStats.FoodData.HealthAmount);
			GameObject pp = Instantiate(ParticlePrefab, source.transform);
			pp.GetComponent<ParticleSystemRenderer>().material.mainTexture = ItemData.InvData.ItemIcon.texture;
		}
	}

	public override void SecondaryAction(GameObject source) {
		PrimaryAction(source);
	}
}