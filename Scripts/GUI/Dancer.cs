using UnityEngine;

public class Dancer : object {

  Vector2 mEndPos;
  Vector2 mStartPos;
  Rect mRect;
  float mT;
  float mWait;
  float mMax;
  float mSize;

  public float T { set { mT = value; }}

  public Rect Rect { get {

    float t = Mathf.Clamp(mT, 0.0f, mMax);

    mRect.x = mStartPos.x * (1.0f - t) + mEndPos.x * t;
    mRect.y = mStartPos.y * (1.0f - t) + mEndPos.y * t;
    return mRect;
  }}

  public Dancer (float x, float y, float size) {
    mEndPos = new Vector2(x, y);
    mStartPos = new Vector2(0.0f - size * 1.1f, y);
    mT = 0.0f;
    mMax = 1.0f;
    mSize = size;

    mRect = new Rect(0.0f, 0.0f, size, size);
  }

  public void Move (float deltaT) {
    mT += deltaT * CFG.DANCE_TIME_FACTOR;
    if (mT > CFG.DANCE_PAUSE) {
      mT = 0.0f;
      if (mEndPos.x > Screen.width) {
        mEndPos = mStartPos;
        mStartPos = new Vector2(0.0f - mSize * 1.1f, mEndPos.y);
      } else {
        mStartPos = mEndPos;
        mEndPos = new Vector2(Screen.width + mSize * 1.1f, mStartPos.y);
      }
    }
  }
}
