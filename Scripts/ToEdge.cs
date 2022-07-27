using UnityEngine;
using System.Collections;

public class ToEdge : MonoBehaviour {

  public enum Edge { TOP, BOTTOM, LEFT, RIGHT };

  public Edge mEdge;

	void Start () {
    transform.position = GetNewPosition();
	}

  Vector3 GetNewPosition () {
    Vector3 oldPos = transform.position;

    if (mEdge == Edge.TOP) {
      Vector3 edge = Camera.main.ViewportToWorldPoint(Vector3.up);
      return new Vector3(oldPos.x, edge.y, oldPos.z);
    } else if (mEdge == Edge.BOTTOM) {
      Vector3 edge = Camera.main.ViewportToWorldPoint(Vector3.zero);
      return new Vector3(oldPos.x, edge.y, oldPos.z);
    } else if (mEdge == Edge.LEFT) {
      Vector3 edge = Camera.main.ViewportToWorldPoint(Vector3.zero);
      return new Vector3(edge.x, oldPos.y, oldPos.z);
    } else { // RIGHT
      Vector3 edge = Camera.main.ViewportToWorldPoint(Vector3.right);
      return new Vector3(edge.x, oldPos.y, oldPos.z);
    }
  }
}
