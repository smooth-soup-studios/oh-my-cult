using System;
using System.Collections.Generic;

[Serializable]
public class SerializableList<T> {
	public List<T> Data = new();
}

public static class ListExtentions {
	// Allows easy conversion to serializable list from a normal list
	public static SerializableList<T> ToSerializable<T>(this List<T> list) {
		return new SerializableList<T>() { Data = list };
	}

	public static List<T> ToRegular<T>(this SerializableList<T> list) {
		return list.Data;
	}
}