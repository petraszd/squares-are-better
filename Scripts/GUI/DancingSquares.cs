using UnityEngine;
using System.Collections;

public class DancingSquares : MonoBehaviour {

  public Texture2D mTexture;

  Dancer[] mDancers;

  void Start () {
    float size = GetDancerSize();
    float half = size * 0.5f;
    Vector2 center = GetFloorCenter();

    float[] y = new float[4];
    for (int i = 0; i < 4; ++i) { y[i] = center.y + i * size; }

    float[] x = new float[13];
    for (int i = 0; i < 13; ++i) { x[i] = center.x - half + (i - 6) * size; }

    mDancers = new Dancer[23];
    // Y
    mDancers[0] = new Dancer(x[0], y[0], size);
    mDancers[1] = new Dancer(x[0], y[1], size);
    mDancers[2] = new Dancer(x[2], y[0], size);
    mDancers[3] = new Dancer(x[2], y[1], size);
    mDancers[4] = new Dancer(x[1], y[2], size);
    mDancers[5] = new Dancer(x[1], y[3], size);

    // A
    mDancers[6] = new Dancer(x[5], y[0], size);
    mDancers[7] = new Dancer(x[5] - half, y[1], size);
    mDancers[8] = new Dancer(x[5] + half, y[1], size);
    mDancers[9] = new Dancer(x[4], y[2], size);
    mDancers[10] = new Dancer(x[6], y[2], size);
    mDancers[11] = new Dancer(x[5], y[2] + half, size);
    mDancers[12] = new Dancer(x[4] - half, y[3], size);
    mDancers[13] = new Dancer(x[6] + half, y[3], size);

    // Y
    mDancers[14] = new Dancer(x[8], y[0], size);
    mDancers[15] = new Dancer(x[8], y[1], size);
    mDancers[16] = new Dancer(x[10], y[0], size);
    mDancers[17] = new Dancer(x[10], y[1], size);
    mDancers[18] = new Dancer(x[9], y[2], size);
    mDancers[19] = new Dancer(x[9], y[3], size);

    // !
    mDancers[20] = new Dancer(x[12], y[0], size);
    mDancers[21] = new Dancer(x[12], y[1], size);
    mDancers[22] = new Dancer(x[12], y[3], size);

    for (int i = 0; i < mDancers.Length; ++i) {
      mDancers[i].T = -i * 0.1f;
    }
  }

  void OnGUI () {
    foreach (Dancer dancer in mDancers) {
      GUI.DrawTexture(dancer.Rect, mTexture);
    }
  }

  void Update () {
    foreach (Dancer dancer in mDancers) {
      dancer.Move(Time.deltaTime);
    }
  }

  float GetDancerSize () {
    return MenuCanvas.GetCanvasHeight() * 0.5f / 4.0f * 0.8f;
  }

  Vector2 GetFloorCenter () {
    float x = Screen.width * 0.5f;
    float y = Screen.height * 0.5f;
    return new Vector2(x, y);
  }
}
