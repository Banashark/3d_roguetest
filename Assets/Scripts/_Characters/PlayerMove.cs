using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	GameObject player;
	int playerMask = 1 << 9;
	//LayerMask notEnvironment;
	RaycastHit hit;
	//Jake
	Vector3 movPos;
	Vector3 movementStep;
	Vector3 playerYHeight;

	bool moving;
	float movementSpeed = 5;
	float lastDistance, currentDistance, snapToDistance;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerMask = ~playerMask;
		playerYHeight = Vector3.up * player.transform.position.y;
		snapToDistance = movementSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Mouse0) && !moving) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 1000, playerMask)) {
				Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 200);
				/*print("Hit environment");
				Debug.Log(hit.point.x + " " + hit.point.y + " " + hit.point.z);
				Debug.Log(hit.collider);*/

				movPos = hit.point + playerYHeight; //destination
				Vector3 playerToPointVect = (movPos-player.transform.position); //vector from player to destination
				movementStep = playerToPointVect.normalized; //normalized vector
				lastDistance = Vector3.Distance(player.transform.position,movPos); //distance between player and the destination
			
				Debug.Log ("Starting position: "+(player.transform.position));
				Debug.Log ("Destination position: "+movPos);
				player.transform.Translate(movementStep * Time.deltaTime * movementSpeed, Space.World);
				moving = true;

			}
		}else if(moving){
			currentDistance = Vector3.Distance(player.transform.position,movPos); //new distance between player's current position and the destination

			//animation
			if(lastDistance < currentDistance){
				player.transform.position = movPos;
				moving = false;
			}else{
				lastDistance = currentDistance;
				player.transform.Translate(movementStep * Time.deltaTime * movementSpeed, Space.World);
			}
		}
	}
}
