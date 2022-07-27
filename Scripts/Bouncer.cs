using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {

  // TODO: rewrite this mess
  void OnCollisionEnter (Collision collision) {
    SquareMove move = collision.gameObject.GetComponent<SquareMove>();

    float x = Mathf.Cos(transform.eulerAngles.x * Mathf.Deg2Rad);
    float y = Mathf.Sin(transform.eulerAngles.x * Mathf.Deg2Rad);
    if (transform.eulerAngles.x < 180.0f) { // Upside down
      x *= -1.0f;
    }
    Vector3 normal = new Vector3(x, y, 0.0f);
    Vector3 newDir = move.Direction - normal * Vector3.Dot(normal, move.Direction) * 2.0f;

    move.Direction = newDir.normalized;
  }

  Vector3 GetNormal () {
    transform.Rotate(new Vector3(0.0f, 0.0f, 90.0f), Space.World);
    Vector3 result = transform.localEulerAngles.normalized;
    transform.Rotate(new Vector3(0.0f, 0.0f, -90.0f), Space.World);

    return result;
  }
}
