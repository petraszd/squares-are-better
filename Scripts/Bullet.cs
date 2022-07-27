using UnityEngine;
using System.Collections;

public class Bullet : Destroyable {

  Vector3 mDirection;
  public Vector3 Direction { set { mDirection = value; } }

  public float mSpeed = 0.0f;

  void Update () {
    transform.Translate(mDirection * mSpeed * Time.deltaTime, Space.World);
  }

  void OnBecameInvisible () {
    // Do not interupt particle effects
    if (GetComponent<MeshRenderer>().enabled) {
      Destroy(gameObject);
    }
  }
}
