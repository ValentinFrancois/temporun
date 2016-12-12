using UnityEngine;
using System.Collections;

public class translate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (/*-0.130f*/ -3.9f*Time.deltaTime, 0, 0); 
		if (transform.position.x < -20) {
			Destroy (this.gameObject);
		}

	}


}
