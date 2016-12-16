using UnityEngine;
using System.Collections;
using System;

public class Comete : MonoBehaviour {
    public GameObject CometeExplosion;
    private GameObject FloorManager;
    public Transform Trou;
	int compteur;
	
    void Start()
    {
        FloorManager = GameObject.FindGameObjectWithTag("SolMan");  
            
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "sol" && compteur==0)
        {
            Instantiate(Trou, col.transform.position + new Vector3(0,0.15f,0), Quaternion.identity, FloorManager.transform);
			compteur+=1;
            Destroy(col.gameObject);
            Destroy(this.gameObject);
            Instantiate(CometeExplosion, transform.position, transform.rotation);
        }
    }
    


   
    void Update() {
        transform.position = transform.position + new Vector3(-3.5f*Time.deltaTime, -5f*Time.deltaTime, 0);
        Vector3 move = new Vector3();
        transform.position += move;
		if (transform.position.y<-20){
			Destroy(this.gameObject);
		}

    }
}
