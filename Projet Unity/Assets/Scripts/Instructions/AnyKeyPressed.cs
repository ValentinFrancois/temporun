using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AnyKeyPressed : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        if (Input.anyKey)
            SceneManager.LoadScene("JouerTest");	
	}
}
