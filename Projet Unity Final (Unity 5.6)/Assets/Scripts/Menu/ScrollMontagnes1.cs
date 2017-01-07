using UnityEngine;
using System.Collections;

public class ScrollMontagnes1 : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		MeshRenderer mr = GetComponent<MeshRenderer>();
		Material mat = mr.material;
		Vector2 offset = mat.mainTextureOffset;
		offset.x += Time.deltaTime / 200;
		mat.mainTextureOffset = offset;
	}
}