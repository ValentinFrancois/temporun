using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System;

public class parser : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		XmlDocument doc = new XmlDocument();
		doc.Load(Application.persistentDataPath + "/sauv.xml"); // chemin où j'enregistre mon .xml

		XmlElement root = doc.DocumentElement; // racine du xml

		// la plupart des fonctions renvoient des objets de type XmlNode,
		// mais on a besoin d'objets de type XmlElement pour lire leurs attributs donc on fait la conversion à chaque fois.

		XmlElement FirstPartie = root.FirstChild as XmlElement; // premier enfant de la racine (donc première partie)
		XmlElement LastPartie = root.LastChild as XmlElement; // dernier enfant de la racine (donc dernière partie)

		XmlNodeList ToutesLesParties = root.ChildNodes; // liste des enfants de la racine (donc toutes les parties)

		// pour avoir le nombre d'éléments de la liste :
		int NombreEnfants = ToutesLesParties.Count;
		
		// pour parcourir la liste :

		for (int i=0; i<ToutesLesParties.Count; i++){
			XmlElement currentNode = ToutesLesParties[i] as XmlElement;
			String nom = currentNode.GetAttribute("name");
		}
		
		// pour tester si un noeud a des enfants :
		
		if (FirstPartie.HasChildNodes){}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
