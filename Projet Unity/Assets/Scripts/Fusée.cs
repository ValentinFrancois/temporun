using UnityEngine;
using System.Collections;

public class Fusée : MonoBehaviour {

    // Use this for initialization
    public Vector3[] positions;

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
    void Update () {
        transform.position = transform.position + new Vector3(-0.2f, 0, 0);
        //Création d'un nouveau vecteur de déplacement
        Vector3 move = new Vector3();

        // On applique le mouvement à l'objet
        transform.position += move;
    }
}
