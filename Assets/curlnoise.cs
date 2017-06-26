using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curlnoise : MonoBehaviour {


	public Material mat;
	public Material blur;
	public Material quad;
	public Texture noiseTexture;
	public Texture stamp;
	public float StampSize = 0.4f;
	public RenderTexture buffer;
	public RenderTexture temp;
	public Material blend;

	// Use this for initialization
	void Start () {
		
		mat.SetTexture (Shader.PropertyToID ("_NoiseTex"), noiseTexture);
		//Graphics.Blit (stamp, Camera.main.activeTexture);

		temp = RenderTexture.GetTemporary (buffer.width, buffer.height, 24);

		//blur = new Material (Shader.Find ("Hidden/BlurEffect"));
		//blend = new Material (Shader.Find ("Custom/Additive"));
	}
	
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (buffer == null) {
			buffer = new RenderTexture (source.width,
				source.height,
				24);
			quad.mainTexture = buffer;
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

		//Graphics.Blit (source, destination);
	}

	//correctly stamping to a rendertexture
	//http://answers.unity3d.com/questions/327984/graphicsdrawtexture-to-rendertexture-not-working.html
	void Update ()
	{
		
		if (Input.GetMouseButton(0))
		{
				var currentActiveRT =RenderTexture.active;
				RenderTexture.active = buffer;                      //Set my RenderTexture active so DrawTexture will draw to it.
				GL.PushMatrix ();                                //Saves both projection and modelview matrices to the matrix stack.
				GL.LoadPixelMatrix (0, buffer.width, buffer.height, 0);            //Setup a matrix for pixel-correct rendering.
				//Draw my stampTexture on my RenderTexture positioned by posX and posY.
				Graphics.DrawTexture (
				new Rect (Input.mousePosition.x - stamp.width *StampSize,
					(buffer.height - Input.mousePosition.y) - stamp.height *StampSize,
					stamp.width*StampSize,
					stamp.height*StampSize), stamp);
				GL.PopMatrix ();                                //Restores both projection and modelview matrices off the top of the matrix stack.
				RenderTexture.active = currentActiveRT;   


		}


		                 //De-activate my RenderTexture.
	}


}
