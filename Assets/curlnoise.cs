using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curlnoise : MonoBehaviour {


	public Material mat;
	public Texture noiseTexture;
	public Texture stamp;

	// Use this for initialization
	void Start () {
		
		mat.SetTexture (Shader.PropertyToID ("_NoiseTex"), noiseTexture);
		Graphics.Blit (stamp, Camera.main.activeTexture);
		 
	}
	
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
			Graphics.Blit (source, destination, mat);
			
	}
}
