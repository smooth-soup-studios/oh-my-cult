using UnityEngine;

[CreateAssetMenu(fileName = "new ActorStats", menuName = "OhMyCult/Actor/ActorStats", order = 0)]
public class ActorStats : ScriptableObject {
	public float Speed = 2f;
	public float ChargeSpeed = 1;
	public float DetectionRange = 12.5f;
	public float AttackRange = 1.5f;
	public float RetreatRange = 1f;
	public float AttackSpeed = 0.56f;

	[Tooltip("Change from MeleeEnemy to the desired enemy type")]
	public WeaponItem EnemyWeapon;
	public GameObject EnemyProjectile;

}

public enum ActorType {
	NPC,
	MeleeEnemy,
	RangedEnemy,
	BearEnemy
}