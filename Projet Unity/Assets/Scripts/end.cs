using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision c){
		if (c.gameObject.tag == "perso") {
			SceneManager.LoadScene("GameOver"); 
		}
	}
}
