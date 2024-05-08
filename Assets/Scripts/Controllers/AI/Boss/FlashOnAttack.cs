using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnAttack : MonoBehaviour {
	public IEnumerator FlashSlamAttack() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.5f);
		GetComponent<SpriteRenderer>().color = Color.clear;
	}
}
