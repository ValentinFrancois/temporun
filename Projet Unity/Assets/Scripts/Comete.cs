using UnityEngine;
using System.Collections;
using System;

public class Comete : MonoBehaviour {
    public Vector3[] positions;
    public GameObject CometeExplosion;
    private GameObject FloorManager;
    public Transform Trou;
    void Start()

    {
        FloorManager = GameObject.FindGameObjectWithTag("SolMan");     
            int randomNumber = UnityEngine.Random.Range(0, positions.Length);
            transform.position = positions[randomNumber];
            
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
       
        transform.position = transform.position + new Vector3(-0.07f, -0.1f, 0);
        Vector3 move = new Vector3();
        transform.position += move;

    }
}
