using UnityEngine;
using System.Collections;

public class MoveFromTo : MonoBehaviour {

  public Vector3[] mPoints;

  public float mTime; // Time to travel

  public float mT = 0.0f;
  int mFrom = 0;
  int mTo = 1;

	void Update () {
    if (mPoints.Length < 2) {
      return;
    }

    transform.position = GetCurrentPosition();
    mT = NextT();
    if (mT > 1.0f) {
      mT = 0.0f;
      StepUp();
    }
	}

  Vector3 GetCurrentPosition () {
    return mPoints[mFrom] * (1.0f - mT) + mT * mPoints[mTo];
  }

  float NextT () {
    if (Time.deltaTime == 0.0) {
      return mT;
    }
    return mT + (Time.deltaTime / mTime);
  }

  void StepUp () {
    mFrom = mTo;
    mTo++;
    if (mTo == mPoints.Length) {
      mTo = 0;
    }
  }
}
