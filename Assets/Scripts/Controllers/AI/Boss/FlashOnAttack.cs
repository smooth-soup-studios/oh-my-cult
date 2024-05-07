using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnAttack : MonoBehaviour {
	public IEnumerator FlashSlamAttack() {
		Logger.Log("Return", "Flash"); 
		yield return new WaitForSeconds(0.5f);

	}
}
