using UnityEngine;
using System.Collections;

public class WaitAndNext : MonoBehaviour {

  public string mNext = null;
  public string mPrev = null;
  public float mWaitTime = CFG.PAUSE_BEFORE_NEXT;
  public bool mAllowTouchForward = true;

	void Update () {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Prev();
    }
    if (mAllowTouchForward && Input.touches.Length > 0) {
      Next();
    }
    StartCoroutine(DoWaitAndNext());
	}

  void Next () {
    Application.LoadLevel(GetNextLevel());
  }

  void Prev () {
    Application.LoadLevel(GetPrevLevel());
  }

  string GetNextLevel () {
    if (string.IsNullOrEmpty(mNext)) {
      return PlayerPrefs.GetString("Next");
    }
    return mNext;
  }

  string GetPrevLevel () {
    if (string.IsNullOrEmpty(mPrev)) {
      return PlayerPrefs.GetString("Prev");
    }
    return mPrev;
  }

  IEnumerator DoWaitAndNext () {
    yield return new WaitForSeconds(mWaitTime);
    Next();
  }
}
