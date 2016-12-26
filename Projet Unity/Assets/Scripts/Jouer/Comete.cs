using UnityEngine;
using System.Collections;
using System;

public class Comete : MonoBehaviour {
  public GameObject CometeExplosion;
  private GameObject FloorManager;
  public Transform Trou;
	public Transform TrouTaille2;
	public Transform Debris;
	Transform hole1, hole2;
	int compteur;
	Vector3 position;
	
    void Start(){
			
			compteur = 0;
			FloorManager = GameObject.FindGameObjectWithTag("SolMan");  
			hole2 = Instantiate(TrouTaille2, new Vector3(16f, 20f, 10f), Quaternion.identity) as Transform;
			hole1 = Instantiate(Trou, new Vector3(16f, 20f, 10f), Quaternion.identity) as Transform;
			position = new Vector3(0f,0f,0f);
            
    }


    void OnTriggerEnter(Collider col){
			
 /* if (col.tag == "sol")
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
			if (col.tag == "sol"){
				compteur ++;
				position+=col.transform.position;
				Destroy(col.gameObject);
			}
			
    }
    


   
    void Update() {
			
      if (compteur==2){
				hole2.position = new Vector3(position.x/2, position.y/2, position.z/2)+ new Vector3(0,0.15f,0);
				Instantiate(CometeExplosion, transform.position, transform.rotation);
				Instantiate(Debris, transform.position + new Vector3(-0.4f,0.3f,0), Quaternion.identity);
				Destroy(this.gameObject);
				Destroy(hole1.gameObject);
			}
			if (compteur==1){
				hole1.position = new Vector3(position.x, position.y, position.z)+ new Vector3(0,0.15f,0);
				Instantiate(CometeExplosion, transform.position, transform.rotation);
				Instantiate(Debris, transform.position + new Vector3(-0.4f,0.3f,0), Quaternion.identity);
				Destroy(this.gameObject);
				Destroy(hole2.gameObject);
			}
			if (transform.position.y<-20){
				Destroy(this.gameObject);
			}
			transform.position = transform.position + new Vector3(-3.5f*Time.deltaTime, -5f*Time.deltaTime, 0);

    }
		
}



