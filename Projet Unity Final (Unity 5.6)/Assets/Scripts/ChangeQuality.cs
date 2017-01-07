using UnityEngine;
using System.Collections;

public class ChangeQuality : MonoBehaviour {

	public void setQuality(int range){
		QualitySettings.SetQualityLevel(range, true);
	}
}
