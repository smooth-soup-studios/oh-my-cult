using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "OhMyCult/Actor/PlayerStats")]
public class PlayerStatsSO : ScriptableObject {
	[Header("Movement State")]
	public float MovementSpeed;

	[Header("Dash State")]
	public float DashSpeed;

	[Header("Idle State")]
	public float PlayerHealth;
}
