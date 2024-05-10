public class FoodItem : InteractableItem {
	public FoodStats FoodStats;

	public override void PrimaryAction(StateMachine machine) {
		machine.GetComponent<HealthController>().AddHealth(FoodStats.FoodData.HealthAmount);
		machine.PlayerInventory.RemoveItem(ItemData);
	}

	public override void SecondaryAction(StateMachine machine) {
		PrimaryAction(machine);
	}
}