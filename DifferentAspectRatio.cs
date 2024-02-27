using UnityEngine;

public class DifferentAspectRatio : MonoBehaviour {

	void Start () 
	{
		float targetaspect = 16.0f / 9.0f;

		float windowaspect = (float)Screen.width / (float)Screen.height;

		float scaleheight = windowaspect / targetaspect;

		Camera camera = GetComponent<Camera>();

		if (scaleheight < 1.0f)
		{  
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleheight;
			rect.x = 0;
			rect.y = (1.0f - scaleheight) / 2.0f;

			camera.rect = rect;
		}
	}
}
