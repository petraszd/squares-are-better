using UnityEngine;


public class SquareControl : MonoBehaviour {

  Vector3 mTouchPos;
  int mTouchId;
  bool mIsControlled;

  void Start () {
    mIsControlled = false;
  }

  void Update () {
    foreach (Touch touch in Input.touches) {
      mTouchPos = ToWorld(touch.position);
      if (IsTouchBegin(touch)) {
        OnTouchBegin(touch.fingerId);
      } else if (IsTouchMove(touch) && IsSameTouch(touch)) {
        OnTouchMoved();
      } else if (IsTouchEnd(touch) && IsSameTouch(touch)) {
        OnTouchEnd();
      }
    }

    if (mIsControlled && Input.touches.Length == 0) {
      OnTouchEnd();
    }
  }

  bool IsSameTouch (Touch touch) {
    return touch.fingerId == mTouchId;
  }

  bool IsTouched () {
    float length = GetLineSqrLength();
    float scale = GetCurrentScale();

    if (scale > CFG.SQUARE_BIGGEST_LIMIT) {
      return length < CFG.SQUARE_BIGGEST_TOUCH_R;
    } else if (scale > CFG.SQUARE_MEDIUM_LIMIT) {
      return length < CFG.SQUARE_MEDIUM_TOUCH_R;
    } else {
      return length < CFG.SQUARE_SMALLEST_TOUCH_R;
    }
  }

  bool IsTouchBegin(Touch touch) {
    if (!IsTouched()) {
      return false;
    }
    if (!mIsControlled && touch.phase == TouchPhase.Began) {
      return true;
    }
    if (!mIsControlled && touch.phase == TouchPhase.Moved) {
      return true;
    }
    return false;
  }

  bool IsTouchMove(Touch touch) {
    return mIsControlled && touch.phase == TouchPhase.Moved;
  }

  bool IsTouchEnd(Touch touch) {
    return mIsControlled && touch.phase == TouchPhase.Ended;
  }

  void OnMouseUp () {
    mTouchPos = ToWorld(Input.mousePosition);
    OnTouchEnd();
  }

  void OnMouseDrag () {
    mTouchPos = ToWorld(Input.mousePosition);
    OnTouchMoved();
  }

  void OnTouchBegin (int touchId) {
    mIsControlled = true;
    mTouchId = touchId;
    HighlitghOn();
  }

  void OnTouchEnd () {
    mIsControlled = false;

    HighlitghOff();
    DisableLine();
    if (CanBeSplitted() && IsSplitDelta()) {
      Split();
    } else if (!IsSplitDelta()) {
      Shoot();
    }
  }

  void OnTouchMoved () {
    if (IsSplitDelta()) {
      DisableLine();
    } else {
      EnableLine();
    }
  }

  void Split () {
    for (var x = -1; x < 2; x += 2) {
      for (var y = -1; y < 2; y += 2) {
        Spawn(GetNewPosition(x, y), x, y);
      }
    }
    Destroy(gameObject);
  }

  void Shoot () {
    Vector3 delta = GetDelta();

    SquareMove moveComp = gameObject.GetComponent<SquareMove>();
    moveComp.Direction = delta.normalized;
    moveComp.Speed = GetSpeed();

    if (delta.magnitude > CFG.SQUARE_SHOOT_FX_MIN_MAGNITUDE) {
      SoundController.PlayShootFx();
    }
  }

  float GetSpeed () {
    return Mathf.Clamp(
        Mathf.Exp(GetDelta().magnitude * CFG.SQUARE_SHOOT_SPEED_FACTOR),
        CFG.SQUARE_MIN_SPEED, CFG.SQUARE_MAX_SPEED);
  }

  Vector3 GetNewPosition (int x, int y) {
    float factor = GetCurrentScale() * CFG.SQUARE_CLONED_JUMP_AMOUNT;
    Vector3 startPos = new Vector3(x * factor, y * factor, 0.0f);
    return transform.position + startPos;
  }

  void Spawn (Vector3 pos, int x, int y) {
    Transform cloned = GetMiniClone(pos);
    SquareMove move = cloned.gameObject.GetComponent<SquareMove>();
    move.Direction = GetCloneDirection(x, y);
  }

  Vector3 GetCloneDirection (int x, int y) {
    Vector3 cannonicalDir = new Vector3(x, y, 0.0f).normalized;
    Vector3 cloneDir = cannonicalDir * CFG.SQUARE_CANONICAL_WEIGHT;
    Vector3 parentDir = GetDirection() * CFG.SQUARE_PARENT_WEIGHT;

    return (parentDir + cloneDir).normalized;
  }

  Transform GetMiniClone (Vector3 pos) {
    Transform cloned = (Transform) (Instantiate(transform, pos, transform.rotation));
    cloned.localScale = GetNewScale();
    return cloned;
  }

  Vector3 GetNewScale () {
    float newScale = GetCurrentScale() / 2.0f;
    return new Vector3(newScale, newScale, newScale);
  }

  float GetCurrentScale () {
    return transform.localScale.x;
  }

  bool CanBeSplitted () {
    return GetCurrentScale() > 1.0;
  }

  Vector3 GetDirection () {
    SquareMove move = gameObject.GetComponent<SquareMove>();
    return move.Direction;
  }

  Vector3 GetLineStart () {
    Vector3 start = transform.position;
    start.z = -2;
    return start;

  }

  Vector3 GetLineEnd () {
    Vector3 end = mTouchPos;
    end.z = -2;

    return end;
  }

  bool IsSplitDelta () {
    return CanBeSplitted() && GetLineLength() < CFG.SPLIT_MAX_DELTA;
  }

  Vector3 GetDelta () {
    return GetLineStart() - GetLineEnd();
  }

  float GetLineLength () {
    return GetDelta().magnitude;
  }

  float GetLineSqrLength () {
    return GetDelta().sqrMagnitude;
  }

  void HighlitghOff () {
    Highlitgh(false);
  }

  void HighlitghOn () {
    Highlitgh(true);
  }

  void Highlitgh (bool on) {
    Transform active = transform.Find("Active");
    active.GetComponent<MeshRenderer>().enabled = on;
  }

  void EnableLine () {
    LineRenderer line = GetLineObject();
    line.enabled = true;
    line.SetPosition(0, GetLineStart());
    line.SetPosition(1, GetLineEnd());
  }

  void DisableLine () {
    GetLineObject().enabled = false;
  }

  LineRenderer GetLineObject () {
    return transform.Find("Line").GetComponent<LineRenderer>();
  }

  Vector3 ToWorld (Vector2 screenPos) {
    Vector3 result = Camera.main.ScreenToWorldPoint(screenPos);
    result.z = 0;
    return result;
  }
}
