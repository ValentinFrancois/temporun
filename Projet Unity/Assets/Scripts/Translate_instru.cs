using UnityEngine;
using System.Collections;

public class Translate_instru : MonoBehaviour {

	// Use thisfor initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Translate (/*-0.130f*/ -3.4f*Time.deltaTime, 0, 0); 
		if (transform.position.x < -13) {
			Destroy (this.gameObject);
		}

	}


}
