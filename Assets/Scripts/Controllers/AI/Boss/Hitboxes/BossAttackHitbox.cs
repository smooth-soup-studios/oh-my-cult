using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttackHitbox : WeaponHitbox {
	[SerializeField] private WeaponStats _weaponData;
	public MovementDirection Direction;
	public BossAttackType AttackType;
}

public enum BossAttackType {
	SLAM,
	ROAR
}