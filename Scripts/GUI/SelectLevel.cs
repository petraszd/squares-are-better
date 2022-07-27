using UnityEngine;
using System.Collections;


public class SelectLevel : TouchableItems {

  public Texture2D mCompleted;
  public Texture2D mNotCompleted;
  public Texture2D mClosed;
  public Texture2D mNotInDemo;

  ArrayList mOpenLevels;
  ArrayList mLevels;

	void Start () {
    mOpenLevels = new ArrayList();
    mLevels = new ArrayList();

    for (int i = 0; i < GetLevelsCount(); ++i) {
      GUITexture level = CreateLevel(i);
      if (IsPlayable(i)) {
        mOpenLevels.Add(level);
      }
      mLevels.Add(level);
    }
	}

  void Update () {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.LoadLevel("Start");
    }
    ProccessTouches();
  }

  GUITexture CreateLevel (int index) {
    GameObject level = new GameObject("Level" + index);

    GUITexture tex = level.AddComponent("GUITexture") as GUITexture;
    tex.texture = GetTexture(index);
    tex.pixelInset = GetPixelInset();

    tex.transform.position = GetPosition(index);
    tex.transform.localScale = Vector3.zero;

    return tex;
  }

  Rect GetPixelInset () {
    // TODO: rewrite this mess
    float w = MenuCanvas.GetCanvasWidthInViewport();
    int perRow = CFG.LEVELS_IMG_IN_ROW;
    float xDelta = w / (perRow + 2 - 1) * 0.86f;

    Vector3 r = Camera.main.ViewportToScreenPoint(new Vector3(xDelta, 0.0f, 0.0f));
    float size = r.x;

    return new Rect(-size/2, -size/2, size, size);
  }

  Vector3 GetPosition (int index) {
    // TODO: rewrite this mess
    float w = MenuCanvas.GetCanvasWidthInViewport();
    float h = MenuCanvas.GetCanvasHeightInViewport();
    int perRow = CFG.LEVELS_IMG_IN_ROW;
    int perCol = GetLevelsCount() / perRow;

    float xStart = 0.5f - w / 2.0f;
    float yStart = 0.5f + h / 2.0f;
    float xDelta = w / (perRow + 2 - 1);
    float yDelta = -h / (perCol + 2 - 1);

    float x = xStart + (index % perRow + 1) * xDelta;
    float y = yStart + (index / perRow + 1) * yDelta;

    return new Vector3(x, y, 0.0f);
  }

  protected override void OnItemSelected (GUITexture tex) {
    string levelName = tex.gameObject.name;
    Application.LoadLevel(levelName);
  }

  Texture2D GetTexture (int index) {
    if (index < GetCompletedCount()) {
      return mCompleted;
    } else if (index < GetUnlockedCount()) {
      return mNotCompleted;
    } else if (index < GetPlayableCount()) {
      return mClosed;
    }
    return mNotInDemo;
  }

  int GetLevelsCount () {
    // Allows demo to appear to have more levels
    // than it actually has
    return CFG.LEVELS_COUNT;
  }

  int GetCompletedCount () {
    if (!PlayerPrefs.HasKey("Completed")) {
      PlayerPrefs.SetInt("Completed", 0);
    }
    return PlayerPrefs.GetInt("Completed");
  }

  int GetUnlockedCount () {
    int completed = GetCompletedCount() + 1;
    if (completed > GetPlayableCount()) {
      return GetPlayableCount();
    }
    if (completed > GetLevelsCount()) {
      return GetLevelsCount();
    }
    return completed;
  }

  int GetPlayableCount () {
    return Application.levelCount - CFG.LEVELS_HELPER_COUNT;
  }

  bool IsPlayable (int index) {
    return index < GetUnlockedCount();
  }

  protected override ArrayList GetGUITextures () {
    return mOpenLevels;
  }
}
