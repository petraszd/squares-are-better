using UnityEngine;


public class Target : Destroyable {

  void Start () {
    Director.AddTarget();
  }

  protected override void OnSquareHit (GameObject square) {
    Director.RemoveTarget();
  }
}
