using UnityEngine;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

public class EraseTempSauv : MonoBehaviour {

	public TextAsset XMLFile;
	void Start(){
		if (!System.IO.File.Exists(Application.persistentDataPath + "/sauv.xml")){
			System.IO.File.WriteAllText(Application.persistentDataPath + "/sauv.xml", XMLFile.text);
		 }		
		else {	

		 int found = -1;
		 XmlDocument doc = new XmlDocument();
		 doc.Load(Application.persistentDataPath + "/sauv.xml");
		 if(doc.DocumentElement.HasChildNodes){
			 	XmlNodeList noms = doc.DocumentElement.ChildNodes;
				int NombreParties = noms.Count;
				for (int i=0; i<NombreParties; i++){
					XmlElement current = noms[i] as XmlElement;
					if (current.GetAttribute("name")=="SampleRun-Automatic-Temporary-Save"){
						found = i;
						break;
						//Debug.Log(doc.DocumentElement.ChildNodes[i].OuterXml);
					}
				}
				if (found >-1){
					doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[found]);
					using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.UTF8)){
						doc.Save(sw);
					}
				}
		 }

			/*
			XmlDocument doc = new XmlDocument();
			doc.Load(Application.persistentDataPath + "/sauv.xml");
			if(doc.DocumentElement.HasChildNodes){
			 	XmlNodeList noms = doc.DocumentElement.ChildNodes;
				int NombreParties = noms.Count;
				for (int i=0; i<NombreParties; i++){
					XmlElement current = noms[i] as XmlElement;
					if (current.GetAttribute("name")=="SampleRun-Automatic-Temporary-Save"){
						doc.DocumentElement.RemoveChild(doc.DocumentElement.ChildNodes[i]);
						using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.UTF8)){
							doc.Save(sw);
						}
						break;
					}
				}
			}
			*/
		}
	}
}
