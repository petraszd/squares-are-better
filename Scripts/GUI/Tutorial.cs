using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

  float mSpeed;

	void Start () {
    mSpeed = 0.0f;
    StartCoroutine(DoWaitAntStartSlideUp());
	}

	void Update () {
    transform.Translate(0.0f, 0.0f, -1.0f * Time.deltaTime * mSpeed);
	}

  IEnumerator DoWaitAntStartSlideUp () {
    yield return new WaitForSeconds(CFG.TUTORIAL_WAIT_PERIOD);
    mSpeed = CFG.TUTORIAL_SPEED;
  }
}
