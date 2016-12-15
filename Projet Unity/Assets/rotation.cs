using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour {
	// Use this for initialization
	private float y; 
	private float i; 

	private float k; 

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		y+=4;
		i -= 0.1f;
		this.transform.rotation = Quaternion.Euler(0,y,0);  
		this.transform.position = new Vector3(i,-2, -4 );

		if (transform.position.x < -20) {
			Destroy (this.gameObject);
		}
	}
}
