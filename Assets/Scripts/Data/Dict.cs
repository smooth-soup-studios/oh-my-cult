using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Dict<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver {

	[SerializeField] private List<Tkey> _keys = new();
	[SerializeField] private List<Tvalue> _values = new();

	public Dict() { }
	public Dict(List<Tkey> keys, List<Tvalue> values) {
		for (int i = 0; i < keys.Count; i++) {
			Add(keys[i], values[i]);
		}
	}

	public Dict(Dictionary<Tkey, Tvalue> inputDict) : this(inputDict.Keys.ToList(), inputDict.Values.ToList()) { }

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

public static class DictionaryExtentions {
	public static Dict<T, T> ToSerializable<T>(this Dictionary<T, T> inputDict) {
		return new Dict<T, T>(inputDict);
	}

	public static Dictionary<T, T> ToRegular<T>(this Dict<T, T> inputDict) {
		return inputDict;
	}
}