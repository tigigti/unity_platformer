using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

	private float offset;
	private GameObject playerObject;
	private Controlls playerControlls;

	// Change this Variables to move Camera when player walks
	private bool playerWalking;
	private bool playerGrounded;
	private float horizontalOffset;

	// Use this for initialization
	void Start () {
		offset = -10;
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerControlls = playerObject.GetComponent<Controlls>();
	}
	
	// Update is called once per frame
	void Update () {
		getPlayerInfo();
		transform.position = new Vector3(playerObject.transform.position.x + horizontalOffset,playerObject.transform.position.y,offset);
	}

	void getPlayerInfo(){
		playerWalking = playerControlls.isWalking;
		playerGrounded = playerControlls.playerOnGround;
		float playerSpeed = Mathf.Sqrt(Mathf.Pow(playerControlls.getRigidbody().velocity.x,2));

		if(playerWalking && playerGrounded && playerSpeed > 4f){
			if(playerControlls.facingRight){
				horizontalOffset = Mathf.Min(3,horizontalOffset + 0.1f);
			}

			else {
				horizontalOffset = Mathf.Max(-3,horizontalOffset - 0.1f);
			}
		}
	}
}
