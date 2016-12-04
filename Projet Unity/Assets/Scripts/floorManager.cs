using UnityEngine;
using System.Collections;

public class floorManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void onTriggerEnter(Collision c){
		Destroy (c.gameObject); 
	}
}
