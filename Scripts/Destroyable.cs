using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {

  void OnCollisionEnter (Collision collision) {
    if (!IsSquare(collision)) {
      return;
    }

    OnSquareHit(collision.gameObject);
    RemoveRenderer();
    ShowExplosion();
    RemovePhysics();
    Destroy(collision.gameObject);
  }

  protected void ShowExplosion () {
    gameObject.GetComponent<ParticleEmitter>().emit = true;
  }

  protected void RemoveRenderer () {
    gameObject.GetComponent<MeshRenderer>().enabled = false;
  }

  protected void RemovePhysics () {
    Destroy(gameObject.GetComponent<Rigidbody>());
    Destroy(gameObject.GetComponent<SphereCollider>());
  }

  protected virtual void OnSquareHit (GameObject square) {
  }

  bool IsSquare (Collision collision) {
    if (!collision.gameObject) {
      return false;
    }
    return collision.gameObject.GetComponent<SquareMove>() != null;
  }
}
