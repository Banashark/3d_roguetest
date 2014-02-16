using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	GameObject player;
	int playerMask = 1 << 9;
	//LayerMask notEnvironment;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerMask = ~playerMask;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Mouse0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


			if (Physics.Raycast(ray, out hit, 1000, playerMask)) {
				Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 200);
				/*print("Hit environment");
				Debug.Log(hit.point.x + " " + hit.point.y + " " + hit.point.z);
				Debug.Log(hit.collider);*/

				Vector3 movPos = new Vector3(hit.point.x - player.transform.position.x, hit.point.y + 0.5f - player.transform.position.y, hit.point.z - player.transform.position.z);
				player.transform.Translate(movPos * Time.deltaTime * 1);

				//Vector3 movPos = new Vector3(hit.point.x , hit.point.y + 0.5f, hit.point.z);
				//player.transform.position = Vector3.Slerp(player.transform.position, movPos, Time.deltaTime * 1);


			}
		}

	}
}
