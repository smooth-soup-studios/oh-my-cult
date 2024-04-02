using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OhMyCult/Actor/EnemyStats")]
public class EnemyStatsSO : ScriptableObject {
	[Header("Patrol State")]
	public float Speed;
	public float ObstacleDetectDistance;


	[Header("Player Detection")]

	public float DetectionPauseTime;

	public float PlayerDetectDistance;

	[Header("Charge State")]
	public float ChargeSpeed;
	public float MeleeDetectDistance;

	[Header ("Attack State")]
	public float DamageAmount;
}
