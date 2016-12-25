using UnityEngine;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

public class ScrollClouds : MonoBehaviour {
	public TextAsset XMLFile;
	void Start(){
		if (!System.IO.File.Exists(Application.persistentDataPath + "/sauv.xml")){
			System.IO.File.WriteAllText(Application.persistentDataPath + "/sauv.xml", XMLFile.text);
		 }		
		else {		 
			XmlDocument doc = new XmlDocument();
			doc.Load(Application.persistentDataPath + "/sauv.xml");
			 if(doc.DocumentElement.HasChildNodes){
				 XmlElement last = doc.DocumentElement.FirstChild as XmlElement;
				 if (last.GetAttribute("name")=="SampleRun-Automatic-Temporary-Save"){
					 doc.DocumentElement.RemoveChild(doc.DocumentElement.FirstChild);
					 using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.UTF8)) //Set encoding
					{
						doc.Save(sw);
					}
				 }
			 }
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