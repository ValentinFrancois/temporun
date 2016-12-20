using UnityEngine;
using System.Collections;
using System;

public class Comete : MonoBehaviour {
    public GameObject CometeExplosion;
    private GameObject FloorManager;
    public Transform Trou;
	
    void Start()
    {
        FloorManager = GameObject.FindGameObjectWithTag("SolMan");  
            
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "sol")
        {
            Instantiate(Trou, col.transform.position + new Vector3(0,0.15f,0), Quaternion.identity, FloorManager.transform);
            Destroy(col.gameObject);
            Destroy(this.gameObject);
            Instantiate(CometeExplosion, transform.position, transform.rotation);
        }
    }
    


   
    void Update() {
        transform.position = transform.position + new Vector3(-3.5f*Time.deltaTime, -5f*Time.deltaTime, 0);
		if (transform.position.y<-20){
			Destroy(this.gameObject);
		}

    }
}
