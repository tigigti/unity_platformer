using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnOutOfMap : MonoBehaviour {
	public float threshold;

	void  FixedUpdate () {
		if (transform.position.y < threshold)
			Application.LoadLevel(Application.loadedLevel);
	}

}