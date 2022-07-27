using UnityEngine;


public class ColorfullBackground : MonoBehaviour {

  public Texture2D mTexture;

  void OnGUI () {
    Rect fullScreen = new Rect(0, 0, Screen.width, Screen.height);
    GUI.DrawTexture(fullScreen, mTexture);
  }

  void Start () {
    camera.backgroundColor = MakeRandomColor();
  }

  Color MakeRandomColor () {
    float h = Random.value;
    float s = CFG.BACK_SATURATION;
    float v = CFG.BACK_VALUE;
    return ToRgb(h, s, v);
  }

  Color ToRgb (float h, float s, float v) {
    if (s == 0.0f) { // Never happens, but still...
      return new Color(v, v, v, 1.0f);
    }

    float hi = Mathf.FloorToInt(h * 6.0f);
    float f = h * 6.0f - hi;
    float p = v * (1.0f - s);
    float q = v * (1.0f - (f * s));
    float t = v * (1.0f - ((1.0f - f) * s));

    if (hi == 0) {
      return new Color(v, t, p);
    } else if (hi == 1) {
      return new Color(q, v, p);
    } else if (hi == 2) {
      return new Color(p, v, t);
    } else if (hi == 3) {
      return new Color(p, q, v);
    } else if (hi == 4) {
      return new Color(t, p, v);
    } else if (hi == 5) {
      return new Color(v, p, q);
    }
    return Color.black;
  }
}
