using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractablePopulator : MonoBehaviour {
	private static string _logname = "InteractablePopulator";

	public GameObject PrefabInteractor;
	public string TileNameStartsWith = "Interactable";

	void Awake() {
		if (PrefabInteractor == null) {
			Logger.LogError(_logname, "Prefab is null!");
		}
	}

	void Start() {
		if (!TryGetComponent(out Tilemap map)) {
			Logger.LogError(_logname, "Tilemap is null!");
			return;
		}

		for (int x = map.cellBounds.xMin; x < map.cellBounds.xMax; x++) {
			for (int y = map.cellBounds.yMin; y < map.cellBounds.yMax; y++) {
				Vector3Int localPlace = new(x, y, (int)map.transform.position.y);
				Vector3 place = map.CellToWorld(localPlace) + map.cellSize / 2f;

				if (!map.HasTile(localPlace)) {
					continue;
				}

				if (!map.GetTile(localPlace).name.StartsWith(TileNameStartsWith)) {
					continue;
				}

				GameObject instance = Instantiate(PrefabInteractor, place, Quaternion.identity);
				instance.transform.parent = map.transform;
			}
		}
	}
}