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
}
