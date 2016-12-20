using UnityEngine;
using System.Collections;

public class ScrollClouds : MonoBehaviour {
	public TextAsset XMLFile;
	void Start(){
		if (!System.IO.File.Exists(Application.persistentDataPath + "/sauv.xml")){
			System.IO.File.WriteAllText(Application.persistentDataPath + "/sauv.xml", XMLFile.text);
		 }		
	}

	// Update is called once per frame
	void Update () {
		

		MeshRenderer mr = GetComponent<MeshRenderer>();
		Material mat = mr.material;
		Vector2 offset = mat.mainTextureOffset;
		offset.x += Time.deltaTime / 50;
		mat.mainTextureOffset = offset;
	}
}