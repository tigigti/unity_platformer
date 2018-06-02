using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour {
	
		void OnTriggerEnter2D(Collider2D other) {
          if(other.gameObject.tag == "Player")
		  	Application.LoadLevel(Application.loadedLevel);

		}
	}
	
