using UnityEngine;

[CreateAssetMenu(fileName = "new WeaponStats", menuName = "OhMyCult/Items/new WeaponStats", order = 0)]
public class WeaponStats : ScriptableObject {
	public float Damage;
	public float AttackSpeed;
	public float Range;
}