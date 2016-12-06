using UnityEngine;
using System.Collections;
using System;

public class Comete : MonoBehaviour {
    public Vector3[] positions;
    public GameObject particules;

    private bool canGen = true; // Peut-on créer un astéroide ?
    void Start()

    {
        
            int randomNumber = UnityEngine.Random.Range(0, positions.Length);
            transform.position = positions[randomNumber];
            
    }


    void OnTriggerEnter(Collider col)
    {
                
       
                      Destroy(col.gameObject);
        Destroy(this.gameObject);
       
    }
    


    // Update is called once per frame
    void Update() {
       // rigidbody.AddForce(Vector3.forward * Time.deltaTime * 8);
        transform.position = transform.position + new Vector3(-0.07f, -0.1f, 0);
        //Création d'un nouveau vecteur de déplacement
        Vector3 move = new Vector3();

        // On applique le mouvement à l'objet
        transform.position += move;

    }
}
