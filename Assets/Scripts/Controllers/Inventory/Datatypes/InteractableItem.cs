using UnityEngine;

[SerializeField]
public abstract class InteractableItem : MonoBehaviour {
	public InventoryItem ItemData;
	public float UsageCooldown;

	public InventoryItem GetInventoryItem() => ItemData;
	public abstract void PrimaryAction(StateMachine source);
	public abstract void SecondaryAction(StateMachine source);
}