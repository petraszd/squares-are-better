using UnityEngine;
using System.Collections;


class SquareMove : MonoBehaviour {

  Vector3 mDirection;
  public Vector3 Direction {
    get { return mDirection; }
    set { mDirection = value; }
  }

  float mSpeed;
  public float Speed {
    get { return mSpeed; }
    set { mSpeed = value; }
  }

  void Start () {
    Director.AddSquare();
    SetTrailSize();
    mSpeed = CFG.SQUARE_INIT_SPEED;
    if (mDirection.sqrMagnitude <= 0.0f) {
      mDirection = Random.insideUnitCircle.normalized;
    }
  }

  void Update () {
    transform.Translate(mDirection * mSpeed * Time.deltaTime, Space.World);
  }

  void OnBecameInvisible () {
    Destroy(gameObject);
  }

  void OnDestroy () {
    Director.RemoveSquare();
  }

  IEnumerator EndLevel () {
    yield return new WaitForSeconds(2);
    Application.LoadLevel(Application.loadedLevel);
  }

  void SetTrailSize () {
    float scale = transform.localScale.x;

    TrailRenderer trail = GetTrail();
    trail.startWidth = scale * 10;
    trail.endWidth = scale * 10 * 0.8f;
  }

  TrailRenderer GetTrail () {
    return transform.Find("Trail").GetComponent<TrailRenderer>();
  }
}
