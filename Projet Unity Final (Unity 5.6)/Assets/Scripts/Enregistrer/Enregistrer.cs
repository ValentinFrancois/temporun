using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml;
using System.IO;
using System;
using System.Text;
using System.Text.RegularExpressions;

public class Enregistrer : MonoBehaviour {
	
	public void sauvegarder(){
		int i;
		int j;
		XmlElement partie=null;
		string name = GameObject.Find("NomPartie").GetComponent<Text>().text;
		XmlDocument doc = new XmlDocument();
		doc.Load(Application.persistentDataPath + "/sauv.xml");
		
		XmlNodeList noms = doc.DocumentElement.ChildNodes;
		
		
		int NombreParties = noms.Count;
		for (j=0; j<NombreParties; j++){
			XmlElement current = noms[j] as XmlElement;
			if (current.GetAttribute("name")=="SampleRun-Automatic-Temporary-Save"){
				partie = noms[j] as XmlElement;
				break;
			}
		}

		
		if (name!=""){
			int NameExists = 0;
			if (name=="SampleRun-Automatic-Temporary-Save"){
				name+="-0";
			}
			for (i=0; i<noms.Count; i++)
			{
				XmlElement currentNode = noms[i] as XmlElement;
				Regex exp = new Regex("^"+name+"(-[0-9]*)?$");
				if(exp.IsMatch(currentNode.GetAttribute("name")))
				 {
					NameExists += 1;
				 }
			}
			if (NameExists>0){
				NameExists += 1;
				string suffixe = NameExists.ToString() ;
				name = name+"-"+suffixe;
			}
			XmlElement sauv = partie;
			sauv.SetAttribute("name",name);
			doc.DocumentElement.RemoveChild(noms[j]);
			doc.DocumentElement.PrependChild(sauv);
			using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.UTF8)) //Set encoding
			{
				
				doc.Save(sw);
			}
			SceneManager.LoadScene("Menu");
		}
	}
	
}
