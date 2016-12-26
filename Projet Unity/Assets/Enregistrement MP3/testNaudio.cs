using UnityEngine;
using System.Collections;
using NAudio;
using NAudio.Wave; 
using System;
using System.IO;

public class testNaudio : MonoBehaviour {
	// TEST : jouer 1.wav et 2.wav en même temps puis joueur 3.wav après 8 secondes
	// les fichiers sont à mettre dans le dossier Assets/Resources
	
	
	
	private void Merge()
	{
		// création des pistes
		WaveFileReader piste1 = new WaveFileReader(Application.dataPath+"/Resources/1.wav");
		WaveFileReader piste2 = new WaveFileReader(Application.dataPath+"/Resources/2.wav");
		WaveFileReader piste3 = new WaveFileReader(Application.dataPath+"/Resources/3.wav");
		
		// création du mixer
		var mixer = new WaveMixerStream32();
		mixer.AutoStop = true;
		 
		 
		var piste1stream = new WaveChannel32(piste1);
		piste1stream.PadWithZeroes = false;
		piste1stream.Volume = 0.5f; // choix du volume (je laisse pour l'instant mais on mettra sûrement tout à 1)
		
		var piste2stream = new WaveChannel32(piste2);
		piste2stream.PadWithZeroes = false;
		piste2stream.Volume = 0.5f; // choix du volume (je laisse pour l'instant mais on mettra sûrement tout à 1)
		
		var piste3decalage = new WaveOffsetStream(
		piste3, TimeSpan.FromSeconds(8), TimeSpan.Zero, piste3.TotalTime); // décalage de 8 secondes à partir du début
		var piste3stream = new WaveChannel32(piste3decalage);
		piste3stream.PadWithZeroes = false;
		piste3stream.Volume = 0.5f; // choix du volume (je laisse pour l'instant mais on mettra sûrement tout à 1)
		
		// ajout des pistes au mixer
		mixer.AddInputStream(piste1stream);
		mixer.AddInputStream(piste2stream);
		mixer.AddInputStream(piste3stream);
		 

		// enregistrement du mixer
		var wave32 = new Wave32To16Stream(mixer);
		WaveFileWriter.CreateWaveFile(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)+"/resultat.wav", wave32);
		// fichier créé sur le bureau

	 
	}
	
	void Start(){
		Merge();
	}
}
