using UnityEngine;
using System.Collections;

public class SortieEnnemis : MonoBehaviour {
	int pos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider col){
		if (col.tag=="mine" || col.tag =="fusee" || col.tag=="note"){
			pos = (int)col.transform.position.z;
			pos = -pos-1;
			if (col.tag=="fusee"){pos=pos-5;}
			EntreeEnnemis.tableauEnnemis[pos]-=1;
		}
		if (col.tag=="portail"){
			EntreeEnnemis.tableauEnnemis[0]-=1;
			EntreeEnnemis.tableauEnnemis[1]-=1;
			EntreeEnnemis.tableauEnnemis[2]-=1;
			EntreeEnnemis.tableauEnnemis[3]-=1;
		}
	}
	
}
