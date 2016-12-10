using UnityEngine;
using System.Collections;

public class Fusée : MonoBehaviour {

    public Vector3[] positions;
    public GameObject FuséeExplosion;
    void Start()

    {

        int randomNumber = UnityEngine.Random.Range(0, positions.Length);
        transform.position = positions[randomNumber];

    }


    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "perso")
        {
            
            Destroy(this.gameObject);
            Instantiate(FuséeExplosion, transform.position, transform.rotation);
        }

        if (col.tag == "FinFusée")
        {
            Destroy(this.gameObject);
        }
    }
    
    void Update () {
        transform.position = transform.position + new Vector3(-0.1f, 0, 0);
        Vector3 move = new Vector3();
        transform.position += move;
		if (transform.position.x < -12) {
			Destroy (this.gameObject);
		}

    }
}
