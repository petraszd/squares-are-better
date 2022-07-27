using UnityEngine;
using System.Collections;

public class MenuCanvas : MonoBehaviour {

  public static int GetCanvasHeight () {
    return Screen.height;
  }

  public static int GetCanvasWidth () {
    return (int) (GetCanvasHeight() * CFG.CANVAS_W / CFG.CANVAS_H);
  }

  public static float GetCanvasWidthInViewport () {
    Vector3 v = new Vector3(GetCanvasWidth(), 0, 0);
    Vector3 r = Camera.main.ScreenToViewportPoint(v);
    return r.x;
  }

  public static float GetCanvasHeightInViewport () {
    Vector3 v = new Vector3(0, GetCanvasHeight(), 0);
    Vector3 r = Camera.main.ScreenToViewportPoint(v);
    return r.y;
  }

  public static Rect GetCanvasRect () {
    int h = GetCanvasHeight();
    int w = GetCanvasWidth();
    return new Rect(w / -2, h / -2, w, h);
  }
}
