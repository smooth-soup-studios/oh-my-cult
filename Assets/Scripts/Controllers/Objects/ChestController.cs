
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChestController : MonoBehaviour {
	public Sprite ChestOpen;
	private ItemPickupGlowController _glowController;


	public SpriteRenderer SpriteRenderer;
	void Start() {
		SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		_glowController = GetComponent<ItemPickupGlowController>();
	}
	void ChangeSprite() {
		SpriteRenderer.sprite = ChestOpen;


	}
	public void OpenChest(GameObject target) {
		if (SpriteRenderer.sprite == ChestOpen) {
			Logger.Log($"Chest", "Already opened this chest");
			return;
		}
		Logger.Log($"{target}", "Chest");
		ChangeSprite();

	}


}