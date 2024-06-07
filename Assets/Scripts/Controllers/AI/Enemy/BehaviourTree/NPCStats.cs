using UnityEngine;

[CreateAssetMenu(fileName = "new ActorStats", menuName = "OhMyCult/Actor/ActorStats", order = 0)]
public class ActorStats : ScriptableObject {
	public float Speed = 2f;
	public float DetectionRange = 12.5f;
	public float AttackRange = 1.5f;

	[Tooltip("Change from MeleeEnemy to the desired enemy type")]
	public WeaponItem EnemyWeapon;

}

public enum ActorType {
	NPC,
	MeleeEnemy,
	RangedEnemy
}