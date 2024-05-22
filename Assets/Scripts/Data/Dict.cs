using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class Dict<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver {

	[SerializeField] private List<Tkey> _keys = new();
	[SerializeField] private List<Tvalue> _values = new();

	public void OnBeforeSerialize() {
		_keys.Clear();
		_values.Clear();

		foreach (KeyValuePair<Tkey, Tvalue> pair in this) {
			_keys.Add(pair.Key);
			_values.Add(pair.Value);
		}
	}

	public void OnAfterDeserialize() {
		Clear();

		if (_keys.Count != _values.Count) {
			Logger.LogError("SerializableDict", $"Couldn't deserialize dictionary, amount of keys {_keys.Count} doesn't match values count {_values.Count}");
		}

		for (int i = 0; i < _keys.Count; i++) {
			Add(_keys[i], _values[i]);
		}
	}
}