using UnityEngine;
using System.Collections;

public class spawn : MonoBehaviour {
    
	public GameObject Sphere;
    public GameObject Fusée;
	public float interval = 2f;
    public float intervalFusée = 4f;
	
	void Start () {
		InvokeRepeating ("SpawnSphere", interval, interval);
       InvokeRepeating("SpawnFusée", intervalFusée, intervalFusée);
	}
    
    void SpawnSphere()
	{
		Instantiate (Sphere);
       
	}
    void SpawnFusée()
    {
        Instantiate(Fusée);
    }
}
