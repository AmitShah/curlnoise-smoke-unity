using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curlnoise : MonoBehaviour {


	public Material mat;
	public Material blur;
	public Texture noiseTexture;
	public Texture stamp;

	public RenderTexture buffer;
	public RenderTexture temp;

	// Use this for initialization
	void Start () {
		
		mat.SetTexture (Shader.PropertyToID ("_NoiseTex"), noiseTexture);
		//Graphics.Blit (stamp, Camera.main.activeTexture);

		temp = RenderTexture.GetTemporary (buffer.width, buffer.height, 24);
		blur = new Material (Shader.Find ("Hidden/BlurEffect"));

	}
	
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (buffer == null) {
			buffer = new RenderTexture (source.width,
				source.height,
				24);
			Graphics.Blit (source, buffer);
			Debug.Log ("Copied screen to buffer");
		} else
		{	
			//TODO add blur shader pass
			//Graphics.Blit (source, destination, mat);
			Graphics.Blit (buffer,temp, mat);
			//COPY buffer
			buffer.DiscardContents();
			Graphics.Blit (temp, buffer);
			Graphics.Blit (buffer, destination);
		}
	}


}
