using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnAttack : MonoBehaviour {
	public IEnumerator FlashSlamAttack() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.25f);
		GetComponent<SpriteRenderer>().color = Color.clear;
	}
	public IEnumerator FlashRoarAttack() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.15f);
		GetComponent<SpriteRenderer>().color = Color.clear;
	}
	public IEnumerator FlashChargeAttack() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.20f);
		GetComponent<SpriteRenderer>().color = Color.clear;
	}
}
