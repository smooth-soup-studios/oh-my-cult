using UnityEngine;

[CreateAssetMenu(menuName = "OhMyCult/Actor/BossStats")]
public class BossStatsSO : ScriptableObject {
	[Header("Idle State")]
	public float SwitchTime;

	[Header("Charge Attack")]
	public float ChargeSpeed;
	public float ChargeTime;
	public float ChargeRange;
	public float ChargeAttack;

	[Header("Slam Attack")]
	public float SlamDamage;
	public float SlamRange;
	public float SlamTime;

	[Header("Roar Attack")]
	public float RoarAttack;
	public float RoarRange;
	public float RoarTime;
}
