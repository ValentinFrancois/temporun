using UnityEngine;
using System.Collections;

public class debris : MonoBehaviour {
	float compteur;
	// Use this for initialization
	void Start () {
		compteur = Time.time+4;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > compteur){
			Destroy(this.gameObject);
		}
	}
}
