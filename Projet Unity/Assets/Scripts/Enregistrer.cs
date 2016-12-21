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
		string name = GameObject.Find("NomPartie").GetComponent<Text>().text;
		XmlDocument doc = new XmlDocument();
		doc.Load(Application.persistentDataPath + "/sauv.xml");
		
		XmlElement partie = doc.DocumentElement.LastChild as XmlElement;
		XmlNodeList noms = doc.DocumentElement.ChildNodes;
		
		if (name!=""){
			int NameExists = 0;
			for (int i=0; i<noms.Count; i++)
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
			partie.SetAttribute("name",name);
			using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.ASCII)) //Set encoding
			{
				doc.Save(sw);
			}
			SceneManager.LoadScene("Menu");
		}
	}
	
}
