using UnityEngine;

public class Dropper : MonoBehaviour {
	public GameObject DroppingItemPrefab;
	public GameObject PickupPointInteractable;
	public ItemStack ItemToDrop;
	public void Drop() {
		if (DroppingItemPrefab != null && PickupPointInteractable != null && ItemToDrop != null) {
			GameObject dip = Instantiate(DroppingItemPrefab, transform.position, Quaternion.identity);
			dip.GetComponent<DroppingItemController>().PickupPointInteractable = PickupPointInteractable;
			dip.GetComponent<DroppingItemController>().ItemToDrop = ItemToDrop;
		}
	}
}