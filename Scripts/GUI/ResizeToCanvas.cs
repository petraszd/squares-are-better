using UnityEngine;
using System.Collections;

public class ResizeToCanvas : MonoBehaviour {

  public float mHeightInScreen = 1.0f;

	void Start () {
    GUITexture tex = GetComponent<GUITexture>();
    Rect inset = tex.pixelInset;
    float h = MenuCanvas.GetCanvasHeight() * mHeightInScreen;
    float ratio = h / (float)inset.height;
    float w = inset.width * ratio;
    tex.pixelInset = new Rect(-w / 2, -h / 2, w, h);
	}

  void OnApplicationFocus () {
    Start();
  }
}
