using UnityEngine;
using System.Collections;

public class Fusée : MonoBehaviour {

    public Vector3[] positions;
    public GameObject FuséeExplosion;
    public GameObject FloorManager ;
    void Start()

    {
        FloorManager = GameObject.FindGameObjectWithTag("SolMan");

        int randomNumber = UnityEngine.Random.Range(0, positions.Length);
        transform.position = positions[randomNumber];

    }


    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "perso")
        {
            Instantiate(FuséeExplosion, transform.position + new Vector3 (0,0,5), Quaternion.identity, FloorManager.transform);
            Destroy(this.gameObject);
            
        }


    }
    
    void Update () {
        transform.position = transform.position + new Vector3(-0.1f, 0, 0);
        Vector3 move = new Vector3();
        transform.position += move;
		if (transform.position.x < -20) {
			Destroy (this.gameObject);
		}

    }
}
