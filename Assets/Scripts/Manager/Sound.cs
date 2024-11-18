using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Sound {

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume;

	[Range(-3f, 3f)]
	public float pitch;

	public bool loop = false;

	[HideInInspector]
    public List<AudioSource> sources = new List<AudioSource>();

    public int maxSources = 5;

}
