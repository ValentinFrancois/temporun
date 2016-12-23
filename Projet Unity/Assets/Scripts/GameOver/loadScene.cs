using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using System.Xml;
using System.Text;

public class loadScene : MonoBehaviour {
	public void QuitterEnEffacant(string name){
		XmlDocument doc = new XmlDocument();
		doc.Load(Application.persistentDataPath + "/sauv.xml");
		doc.DocumentElement.RemoveChild(doc.DocumentElement.LastChild);
		using(TextWriter sw = new StreamWriter(Application.persistentDataPath + "/sauv.xml", false, Encoding.ASCII)) //Set encoding
		{
			doc.Save(sw);
		}
		SceneManager.LoadScene(name);
	}
	public void QuitterSansEffacer(string name){
		SceneManager.LoadScene(name);
	}
}
