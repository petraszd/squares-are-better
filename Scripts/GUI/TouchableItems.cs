using UnityEngine;
using System.Collections;


abstract public class TouchableItems : MonoBehaviour {

  GUITexture mActive = null;

  void HighlitghOn (GUITexture tex) {
    mActive = tex;
    Rect rect = new Rect(
        tex.pixelInset.x * CFG.TOUCHABLE_ITEMS_HIGH,
        tex.pixelInset.y * CFG.TOUCHABLE_ITEMS_HIGH,
        tex.pixelInset.width * CFG.TOUCHABLE_ITEMS_HIGH,
        tex.pixelInset.height * CFG.TOUCHABLE_ITEMS_HIGH
        );
    tex.pixelInset = rect;
  }

  void OnApplicationPause () {
    mActive = null;
  }

  void OnApplicationFocus () {
    mActive = null;
  }

  void HighlitghOff (GUITexture tex) {
    Rect rect = new Rect(
        tex.pixelInset.x / CFG.TOUCHABLE_ITEMS_HIGH,
        tex.pixelInset.y / CFG.TOUCHABLE_ITEMS_HIGH,
        tex.pixelInset.width / CFG.TOUCHABLE_ITEMS_HIGH,
        tex.pixelInset.height / CFG.TOUCHABLE_ITEMS_HIGH
        );
    tex.pixelInset = rect;
  }

  protected virtual void ProccessTouches () {
    if (mActive) {
      HighlitghOff(mActive);
    }
    mActive = null;
    foreach (Touch touch in Input.touches) {
      foreach (GUITexture tex in GetGUITextures()) {
        if (!tex.HitTest(touch.position)) {
          continue;
        }

        if (touch.phase == TouchPhase.Ended) {
          OnItemSelected(tex);
        }
        HighlitghOn(tex);
        break;
      }
      break;
    }
  }

  protected abstract ArrayList GetGUITextures ();
  protected abstract void OnItemSelected (GUITexture tex);
}
