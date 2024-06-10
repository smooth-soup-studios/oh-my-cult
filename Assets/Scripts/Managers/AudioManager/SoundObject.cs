using System;
using UnityEngine;

[Serializable]
public class SoundObject {
	public string ClipName;
	public AudioClip Clip;

	[Range(0f, 1f)]
	public float Volume = 1;
	[Range(.1f, 3f)]
	public float Pitch = 1;

	public bool PlayOnAwake;
	public bool Loop;
	
	public AudioType SoundType;
	[HideInInspector] public AudioSource Source;
}


public enum AudioType {
	Master,
	Music,
	FX
}