using UnityEngine;
using System.Collections;
using System;

public class Comete : MonoBehaviour {
    public GameObject CometeExplosion;
    private GameObject FloorManager;
    public Transform Trou;
	public Transform TrouTaille2;
	int compteur;
	float delta;
	Vector3 position;
	
    void Start()
    {
		compteur = 0;
		delta = 0f;
		position = new Vector3(0f,0f,0f);
        FloorManager = GameObject.FindGameObjectWithTag("SolMan");  
            
    }


    void OnTriggerEnter(Collider col)
    {
 /*       if (col.tag == "sol")
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
	*/
	if (col.tag == "sol")
        {
			compteur ++;
			delta = Time.time;
			position+=col.transform.position;
			Destroy(col.gameObject);
        }
    }
    


   
    void Update() {
        if (compteur==2){
			Instantiate(TrouTaille2, new Vector3(position.x/2/*-(Time.time-delta)*4f*/, position.y/2, position.z/2)+ new Vector3(0,0.15f,0), Quaternion.identity, FloorManager.transform);
			Instantiate(CometeExplosion, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}
		if (compteur==1){
			Instantiate(Trou, new Vector3(position.x/*-(Time.time-delta)*4f*/, position.y, position.z)+ new Vector3(0,0.15f,0), Quaternion.identity, FloorManager.transform);
			Instantiate(CometeExplosion, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}
		if (transform.position.y<-20){
			Destroy(this.gameObject);
		}
		transform.position = transform.position + new Vector3(-3.5f*Time.deltaTime, -5f*Time.deltaTime, 0);

    }
}



