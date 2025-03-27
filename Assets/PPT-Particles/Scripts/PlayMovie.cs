using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class PlayMovie : MonoBehaviour {

	Texture texture;
	// Use this for initialization
	void Start () {
		texture = transform.GetComponent<Renderer> ().material.mainTexture;
	}

}
