using System;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "new FoodStats", menuName = "OhMyCult/Items/new FoodStats", order = 0)]
public class FoodStats : ScriptableObject {
	public FoodData FoodData;
}


[Serializable]
public class FoodData {
	[InspectorName("HealthAmount (0-100)")]
	public float HealthAmount;
}