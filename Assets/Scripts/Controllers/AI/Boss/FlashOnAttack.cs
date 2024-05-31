using System.Collections;
using UnityEngine;

public class FlashOnAttack : MonoBehaviour {
	public MovementDirection Direction;
	public BossAttackType AttackType;
	public IEnumerator FlashSlamAttack() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.5f);
		GetComponent<SpriteRenderer>().color = Color.clear;
	}
	public IEnumerator FlashRoarAttack() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.15f);
		GetComponent<SpriteRenderer>().color = Color.clear;
	}
	public IEnumerator FlashChargeAttack() {
		GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds(0.15f);
		GetComponent<SpriteRenderer>().color = Color.clear;
	}
}
