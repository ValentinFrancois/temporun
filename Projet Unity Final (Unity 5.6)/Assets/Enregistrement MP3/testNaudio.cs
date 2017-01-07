using UnityEngine;
using System.Collections;
using NAudio;
using NAudio.Wave; 
using System;
using System.IO;
using System.Xml;
using NAudio.Wave.SampleProviders;
using UnityEngine.UI;

public class testNaudio : MonoBehaviour {
	bool fadeIn;
	public bool Merge(string nompartie){
		
		if (nompartie != ""){
			
			XmlDocument doc = new XmlDocument();
			doc.Load(Application.persistentDataPath + "/sauv.xml");
			XmlElement root = doc.DocumentElement;
			if (root.HasChildNodes){
				
				XmlNodeList ToutesLesParties = root.ChildNodes;
				int NombreParties = ToutesLesParties.Count;
				for (int i=0; i<NombreParties; i++){
					
					XmlElement Partie = ToutesLesParties[i] as XmlElement;
					String nom = Partie.GetAttribute("name");
					if (nom == nompartie){
						
						WaveFileReader[] batterie;
						WaveFileReader[] synthe;
						WaveFileReader[] melodie;
						WaveFileReader[] guitare;
						WaveFileReader[] basse;
						WaveFileReader[] piano;
						
						WaveChannel32[]  batterieStream;
						WaveChannel32[]  syntheStream;
						WaveChannel32[]  melodieStream;
						WaveChannel32[]  guitareStream;
						WaveChannel32[]  basseStream;
						WaveChannel32[]  pianoStream;
						
						WaveOffsetStream[] batterieDecalage;
						WaveOffsetStream[] syntheDecalage;
						WaveOffsetStream[] melodieDecalage;
						WaveOffsetStream[] guitareDecalage;
						WaveOffsetStream[] basseDecalage;
						WaveOffsetStream[] pianoDecalage;
						
						int decalage = 0;
						
						if (Partie.HasChildNodes){
							
							var mixer = new WaveMixerStream32();
							mixer.AutoStop = true;
							XmlNodeList ToutesLesSequences = Partie.ChildNodes;
							int NombreSequences = ToutesLesSequences.Count;
							
							batterie = new WaveFileReader[NombreSequences];
							synthe = new WaveFileReader[NombreSequences];
							melodie = new WaveFileReader[NombreSequences];
							guitare = new WaveFileReader[NombreSequences];
							basse = new WaveFileReader[NombreSequences];
							piano = new WaveFileReader[NombreSequences];
							
							piano[0]=null;
							batterie[0]=null;
							synthe[0]=null;
							melodie[0]=null;
							guitare[0]=null;
							basse[0]=null;
														
							batterieDecalage = new WaveOffsetStream[NombreSequences];
							syntheDecalage = new WaveOffsetStream[NombreSequences];
							melodieDecalage = new WaveOffsetStream[NombreSequences];
							guitareDecalage = new WaveOffsetStream[NombreSequences];
							basseDecalage = new WaveOffsetStream[NombreSequences];
							pianoDecalage = new WaveOffsetStream[NombreSequences];
							
							batterieStream = new WaveChannel32[NombreSequences];
							syntheStream = new WaveChannel32[NombreSequences];
							melodieStream = new WaveChannel32[NombreSequences];
							guitareStream = new WaveChannel32[NombreSequences];
							basseStream = new WaveChannel32[NombreSequences];
							pianoStream  = new WaveChannel32[NombreSequences];
							
							string[] batterieSrc = new string[NombreSequences];
							string[] syntheSrc = new string[NombreSequences];
							string[] melodieSrc = new string[NombreSequences];
							string[] guitareSrc = new string[NombreSequences];
							string[] basseSrc = new string[NombreSequences];
							string[] pianoSrc = new string[NombreSequences];

							for (int k=0; k<NombreSequences; k++){
								batterieSrc[k] = "";
								syntheSrc[k] = "";
								melodieSrc[k] = "";
								guitareSrc[k] = "";
								basseSrc[k] = "";
								pianoSrc[k] = "";
							}
							
							for (int j=0; j<NombreSequences; j++){
								
								XmlElement Sequence = ToutesLesSequences[j] as XmlElement;
								decalage = j*8;
								XmlElement Instru = Sequence.FirstChild as XmlElement;
								string type = Instru.GetAttribute("type");
								string src = Instru.GetAttribute("src");
								switch (type){
									case "piano" : 
										if (src != ""){
											piano[j] = new WaveFileReader(Application.dataPath+"/Resources/"+src);
										}
										pianoSrc[j] = src;
										if(j>0){
											if(batterieSrc[j-1] != ""){
												batterie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+batterieSrc[j-1]);
												batterieSrc[j] = batterieSrc[j-1];
											}
											if(syntheSrc[j-1] != ""){
												synthe[j] = new WaveFileReader(Application.dataPath+"/Resources/"+syntheSrc[j-1]);
												syntheSrc[j] = syntheSrc[j-1];
											}
											if(guitareSrc[j-1] != ""){
												guitare[j] = new WaveFileReader(Application.dataPath+"/Resources/"+guitareSrc[j-1]);
												guitareSrc[j] = guitareSrc[j-1];
											}
											if(basseSrc[j-1] != ""){
												basse[j] = new WaveFileReader(Application.dataPath+"/Resources/"+basseSrc[j-1]);
												basseSrc[j] = basseSrc[j-1];
											}
											if(melodieSrc[j-1] != ""){
												melodie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+melodieSrc[j-1]);
												melodieSrc[j] = melodieSrc[j-1];
											}
										}
									break;
									case "basse" : 
										if (src != ""){
											basse[j] = new WaveFileReader(Application.dataPath+"/Resources/"+src);
										}
										basseSrc[j] = src;
										if(j>0){
											if(batterieSrc[j-1] != ""){
												batterie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+batterieSrc[j-1]);
												batterieSrc[j] = batterieSrc[j-1];
											}
											if(syntheSrc[j-1] != ""){
												synthe[j] = new WaveFileReader(Application.dataPath+"/Resources/"+syntheSrc[j-1]);
												syntheSrc[j] = syntheSrc[j-1];
											}
											if(guitareSrc[j-1] != ""){
												guitare[j] = new WaveFileReader(Application.dataPath+"/Resources/"+guitareSrc[j-1]);
												guitareSrc[j] = guitareSrc[j-1];
											}
											if(melodieSrc[j-1] != ""){
												melodie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+melodieSrc[j-1]);
												melodieSrc[j] = melodieSrc[j-1];
											}
											if(pianoSrc[j-1] != ""){
												piano[j] = new WaveFileReader(Application.dataPath+"/Resources/"+pianoSrc[j-1]);
												pianoSrc[j] = pianoSrc[j-1];
											}
										}
									break;
									case "guitare" : 
										if (src != ""){
											guitare[j] = new WaveFileReader(Application.dataPath+"/Resources/"+src);
										}
										guitareSrc[j] = src;
										if(j>0){
											if(batterieSrc[j-1] != ""){
												batterie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+batterieSrc[j-1]);
												batterieSrc[j] = batterieSrc[j-1];
											}
											if(syntheSrc[j-1] != ""){
												synthe[j] = new WaveFileReader(Application.dataPath+"/Resources/"+syntheSrc[j-1]);
												syntheSrc[j] = syntheSrc[j-1];
											}
											if(basseSrc[j-1] != ""){
												basse[j] = new WaveFileReader(Application.dataPath+"/Resources/"+basseSrc[j-1]);
												basseSrc[j] = basseSrc[j-1];
											}
											if(melodieSrc[j-1] != ""){
												melodie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+melodieSrc[j-1]);
												melodieSrc[j] = melodieSrc[j-1];
											}
											if(pianoSrc[j-1] != ""){
												piano[j] = new WaveFileReader(Application.dataPath+"/Resources/"+pianoSrc[j-1]);
												pianoSrc[j] = pianoSrc[j-1];
											}
										}
									break;
										case "drum1" : 
										if (src != ""){
											batterie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+src);
										}
										batterieSrc[j] = src;
										if(j>0){
											if(syntheSrc[j-1] != ""){
												synthe[j] = new WaveFileReader(Application.dataPath+"/Resources/"+syntheSrc[j-1]);
												syntheSrc[j] = syntheSrc[j-1];
											}
											if(guitareSrc[j-1] != ""){
												guitare[j] = new WaveFileReader(Application.dataPath+"/Resources/"+guitareSrc[j-1]);
												guitareSrc[j] = guitareSrc[j-1];
											}
											if(basseSrc[j-1] != ""){
												basse[j] = new WaveFileReader(Application.dataPath+"/Resources/"+basseSrc[j-1]);
												basseSrc[j] = basseSrc[j-1];
											}
											if(melodieSrc[j-1] != ""){
												melodie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+melodieSrc[j-1]);
												melodieSrc[j] = melodieSrc[j-1];
											}
											if(pianoSrc[j-1] != ""){
												piano[j] = new WaveFileReader(Application.dataPath+"/Resources/"+pianoSrc[j-1]);
												pianoSrc[j] = pianoSrc[j-1];
											}
										}
									break;
									case "synthe" :
										if (src != ""){
											synthe[j] = new WaveFileReader(Application.dataPath+"/Resources/"+src);
										}
										syntheSrc[j] = src;
										if(j>0){
											if(batterieSrc[j-1] != ""){
												batterie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+batterieSrc[j-1]);
												batterieSrc[j] = batterieSrc[j-1];
											}
											if(guitareSrc[j-1] != ""){
												guitare[j] = new WaveFileReader(Application.dataPath+"/Resources/"+guitareSrc[j-1]);
												guitareSrc[j] = guitareSrc[j-1];
											}
											if(basseSrc[j-1] != ""){
												basse[j] = new WaveFileReader(Application.dataPath+"/Resources/"+basseSrc[j-1]);
												basseSrc[j] = basseSrc[j-1];
											}
											if(melodieSrc[j-1] != ""){
												melodie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+melodieSrc[j-1]);
												melodieSrc[j] = melodieSrc[j-1];
											}
											if(pianoSrc[j-1] != ""){
												piano[j] = new WaveFileReader(Application.dataPath+"/Resources/"+pianoSrc[j-1]);
												pianoSrc[j] = pianoSrc[j-1];
											}
										}
									break;
									case "melodie" :
										if (src != ""){
											melodie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+src);
										}
										melodieSrc[j] = src;
										if(j>0){
											if(batterieSrc[j-1] != ""){
												batterie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+batterieSrc[j-1]);
												batterieSrc[j] = batterieSrc[j-1];
											}
											if(syntheSrc[j-1] != ""){
												synthe[j] = new WaveFileReader(Application.dataPath+"/Resources/"+syntheSrc[j-1]);
												syntheSrc[j] = syntheSrc[j-1];
											}
											if(guitareSrc[j-1] != ""){
												guitare[j] = new WaveFileReader(Application.dataPath+"/Resources/"+guitareSrc[j-1]);
												guitareSrc[j] = guitareSrc[j-1];
											}
											if(basseSrc[j-1] != ""){
												basse[j] = new WaveFileReader(Application.dataPath+"/Resources/"+basseSrc[j-1]);
												basseSrc[j] = basseSrc[j-1];
											}
											if(pianoSrc[j-1] != ""){
												piano[j] = new WaveFileReader(Application.dataPath+"/Resources/"+pianoSrc[j-1]);
												pianoSrc[j] = pianoSrc[j-1];
											}
										}
									break;
									case "none" :
										if(j>0){
											if(batterieSrc[j-1] != ""){
												batterie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+batterieSrc[j-1]);
												batterieSrc[j] = batterieSrc[j-1];
											}
											if(melodieSrc[j-1] != ""){
												melodie[j] = new WaveFileReader(Application.dataPath+"/Resources/"+melodieSrc[j-1]);
												melodieSrc[j] = melodieSrc[j-1];
											}
											if(syntheSrc[j-1] != ""){
												synthe[j] = new WaveFileReader(Application.dataPath+"/Resources/"+syntheSrc[j-1]);
												syntheSrc[j] = syntheSrc[j-1];
											}
											if(guitareSrc[j-1] != ""){
												guitare[j] = new WaveFileReader(Application.dataPath+"/Resources/"+guitareSrc[j-1]);
												guitareSrc[j] = guitareSrc[j-1];
											}
											if(basseSrc[j-1] != ""){
												basse[j] = new WaveFileReader(Application.dataPath+"/Resources/"+basseSrc[j-1]);
												basseSrc[j] = basseSrc[j-1];
											}
											if(pianoSrc[j-1] != ""){
												piano[j] = new WaveFileReader(Application.dataPath+"/Resources/"+pianoSrc[j-1]);
												pianoSrc[j] = pianoSrc[j-1];
											}
										}
									break;
								}
								if (piano[j] != null){
									pianoDecalage[j] = new WaveOffsetStream(piano[j], TimeSpan.FromSeconds(decalage), TimeSpan.Zero, TimeSpan.FromSeconds(8));
									pianoStream[j]  = new WaveChannel32(pianoDecalage[j]);
									pianoStream[j].PadWithZeroes = false;
									mixer.AddInputStream(pianoStream[j]);
								}
								if (basse[j] != null){
									basseDecalage[j] = new WaveOffsetStream(basse[j], TimeSpan.FromSeconds(decalage), TimeSpan.Zero, TimeSpan.FromSeconds(8));
									basseStream[j] = new WaveChannel32(basseDecalage[j]);
									basseStream[j].PadWithZeroes = false;
									mixer.AddInputStream(basseStream[j]);
								}
								if (guitare[j] != null){
									guitareDecalage[j] = new WaveOffsetStream(guitare[j], TimeSpan.FromSeconds(decalage), TimeSpan.Zero, TimeSpan.FromSeconds(8));
									guitareStream[j] = new WaveChannel32(guitareDecalage[j]);
									guitareStream[j].PadWithZeroes = false;
									mixer.AddInputStream(guitareStream[j]);
								}
								if (batterie[j] != null){
									batterieDecalage[j] = new WaveOffsetStream(batterie[j], TimeSpan.FromSeconds(decalage), TimeSpan.Zero, TimeSpan.FromSeconds(8));
									batterieStream[j] = new WaveChannel32(batterieDecalage[j]);
									batterieStream[j].PadWithZeroes = false;
									mixer.AddInputStream(batterieStream[j]);
								}
								if (melodie[j] != null){
									melodieDecalage[j] = new WaveOffsetStream(melodie[j], TimeSpan.FromSeconds(decalage), TimeSpan.Zero, TimeSpan.FromSeconds(8));
									melodieStream[j] = new WaveChannel32(melodieDecalage[j]);
									melodieStream[j].PadWithZeroes = false;
									mixer.AddInputStream(melodieStream[j]);
								}
								if (synthe[j] != null){
									syntheDecalage[j] = new WaveOffsetStream(synthe[j], TimeSpan.FromSeconds(decalage), TimeSpan.Zero, TimeSpan.FromSeconds(8));
									syntheStream[j] = new WaveChannel32(syntheDecalage[j]);
									syntheStream[j].PadWithZeroes = false;
									mixer.AddInputStream(syntheStream[j]);
								}
								
							}
							var wave32 = new Wave32To16Stream(mixer);
							string nomSecure = nompartie;
							foreach (char c in System.IO.Path.GetInvalidFileNameChars()){
								 nomSecure = nompartie.Replace(c, '_');
							}
							WaveFileWriter.CreateWaveFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)+"/"+nomSecure+".wav", wave32);
							GameObject.Find("haut").GetComponent<Text>().text = "Le fichier "+nomSecure+".wav";
							return true;
						}
						
					}
					
				}	
				
			}
		
		}
		return false;
	 
	}
	
	public void Exporter(){
		string getNom = GameObject.Find("NomPartie").GetComponent<Text>().text;
		if (Merge(getNom)){
			GameObject.Find("EnregistrementReussi").GetComponent<CanvasGroup>().alpha = 1;
		}
		fadeIn = true;
	}
	
	void Start(){
		fadeIn = false;
	}
	
	void Update(){
		if (fadeIn){
			if (GameObject.Find("EnregistrementReussi").GetComponent<CanvasGroup>().alpha>0.6){
				GameObject.Find("EnregistrementReussi").GetComponent<CanvasGroup>().alpha -= Time.deltaTime/6;
			}
			else if (GameObject.Find("EnregistrementReussi").GetComponent<CanvasGroup>().alpha>0){
				GameObject.Find("EnregistrementReussi").GetComponent<CanvasGroup>().alpha -= Time.deltaTime/3;
			}
			else{
				fadeIn = false;
			}
		}
	}
	
}
