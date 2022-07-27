using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

  public float mTimeout;
  Vector3 mDirection;

  void Start () {
    float x = Mathf.Cos(transform.eulerAngles.x * Mathf.Deg2Rad);
    if (transform.eulerAngles.y < 180.0f) { // Upside down
      x *= -1.0f;
    }
    float y = Mathf.Sin(transform.eulerAngles.x * Mathf.Deg2Rad);
    mDirection = (new Vector3(x, y, 0.0f)).normalized;

    StartCoroutine(MakeBulletCo());
  }

  void MakeBullet () {
    ShootBullet(CloneBullet().GetComponent<Bullet>());
  }

  IEnumerator MakeBulletCo () {
    yield return new WaitForSeconds(mTimeout);
    MakeBullet();
    StartCoroutine(MakeBulletCo());
  }

  Transform CloneBullet () {
    Transform bullet = (Transform) Instantiate(transform.Find("Bullet"));
    bullet.position = transform.position;
    bullet.localRotation = transform.localRotation;
    bullet.GetComponent<SphereCollider>().enabled = true;
    return bullet;
  }

  void ShootBullet (Bullet bullet) {
    bullet.enabled = true;
    bullet.Direction = mDirection;
  }

  void OnCollisionEnter (Collision collision) {
    SquareMove move = collision.gameObject.GetComponent<SquareMove>();
    if (!move) {
      return;
    }

    // TODO: Finish
  }
}
