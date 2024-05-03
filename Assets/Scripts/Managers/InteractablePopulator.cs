using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractablePopulator : MonoBehaviour {
	private static string _logname = "InteractablePopulator";

	public GameObject LDtkLevel;
	public GameObject PrefabInteractor;

	public string InteractorLayerName = "InteractorLayer";
	public string EntityNameStartsWith = "CustomInteractable";

	void Awake() {
		if (LDtkLevel == null) {
			Logger.LogError(_logname, "LDtkLevel is null!");
		}
		else if (PrefabInteractor == null) {
			Logger.LogError(_logname, "Prefab is null!");
		}
	}

	// Start is called before the first frame update
	void Start() {
		Transform World = LDtkLevel.transform.Find("World");

		for (int i = 0; i < World.childCount; i++) {
			Transform levelLayer = World.GetChild(i);

			if (!levelLayer.gameObject.name.StartsWith("Level")) {
				continue;
			}

			Transform InteractorLayer = levelLayer.Find(InteractorLayerName);

			if (InteractorLayer == null) {
				continue;
			}

			for (int j = 0; j < InteractorLayer.childCount; j++) {
				Transform interactor = InteractorLayer.GetChild(j);

				// foreach (var c in Components) {
				// 	if (interactor.gameObject.name.StartsWith(c.Key)) {
				// 		Debug.Log("Add interactor " + c.Value.name + " to " + interactor.gameObject.name + " at " + interactor.position);
				// 		interactor.gameObject.AddComponent(c.Value.GetType());
				// 	}
				// }

				if (!interactor.gameObject.name.StartsWith(EntityNameStartsWith)) {
					continue;
				}

				Debug.Log("Add bush to " + interactor.gameObject.name + " at " + interactor.position);
				// T bushInteractor = (T)interactor.gameObject.AddComponent(Component.GetType());
				// interactor.gameObject.AddComponent(Behaviour.GetClass());
				Instantiate(PrefabInteractor, interactor);
			}
		}
	}
}
