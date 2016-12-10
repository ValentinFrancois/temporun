using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
    
	public GameObject Comete;
    public GameObject Fusée;
    public GameObject Mine;
    public float intervalComete = 2f;
    public float intervalFusée = 4f;
    public float intervalMine = 1f;
	
	void Start () {
      
      
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
