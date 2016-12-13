using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
    
	public GameObject Comete;
    public GameObject Fusée;
    public GameObject Mine;
    public float intervalComete = 20f;
    public float intervalFusée = 60f;
    public float intervalMine = 60f;
   // private GameObject FloorManager;

    void Start () {
        //FloorManager = GameObject.FindGameObjectWithTag("SolMan");

        InvokeRepeating("SpawnComete", intervalComete, intervalComete);
       InvokeRepeating("SpawnFusée", intervalFusée, intervalFusée);
       InvokeRepeating("SpawnMine", intervalMine, intervalMine);
	}
    
    void SpawnComete()
	{
		Instantiate (Comete);
       
	}

    void SpawnFusée()
    {
        Instantiate(Fusée);
    }

    void SpawnMine()
    {
        Instantiate(Mine);
    }

}
