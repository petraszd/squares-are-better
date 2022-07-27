using UnityEngine;
using System.Collections;

public class Director : MonoBehaviour {

  enum LevelStatus{PLAY, WIN, LOSE, UNCONTROLLABLE};

  LevelStatus mStatus = LevelStatus.PLAY;
  int mSquareCounts = 0;
  int mTargeteCounts = 0;

  // All these statics hides ugly hack that
  // prevents NullPointerException on Application.LoadLevel

  public static bool IsDirectorInScene () {
    return Camera.main && Camera.main.GetComponent<Director>() != null;
  }

  public static void AddSquare () {
    if (IsDirectorInScene()) {
      Camera.main.GetComponent<Director>().OnSquareStarted();
    }
  }

  public static void RemoveSquare () {
    if (IsDirectorInScene()) {
      Camera.main.GetComponent<Director>().OnSquareDestroyed();
    }
  }

  public static void AddTarget () {
    if (IsDirectorInScene()) {
      Camera.main.GetComponent<Director>().OnTargetStarted();
    }
  }

  public static void RemoveTarget () {
    if (IsDirectorInScene()) {
      Camera.main.GetComponent<Director>().OnTargetDestroyed();
    }
  }

  void Update () {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.LoadLevel("LevelList");
    }
  }

  public void OnSquareStarted () {
    mSquareCounts++;
  }

  public void OnSquareDestroyed () {
    mSquareCounts--;
    if (mSquareCounts == 0 && !HasEnded()) {
      mStatus = LevelStatus.LOSE;
      StartCoroutine(Lose());
    }
  }

  public void OnTargetStarted () {
    mTargeteCounts++;
  }

  public void OnTargetDestroyed () {
    mTargeteCounts--;
    if (mTargeteCounts == 0 && !HasEnded()) {
      mStatus = LevelStatus.WIN;
      SaveProgress();
      StartCoroutine(Win());
    }
  }

  void OnApplicationQuit () {
    mStatus = LevelStatus.UNCONTROLLABLE;
  }

  bool IsControlable () {
    return mStatus != LevelStatus.UNCONTROLLABLE;
  }

  bool HasEnded () {
    return mStatus == LevelStatus.WIN || mStatus == LevelStatus.LOSE;
  }

  IEnumerator Lose () {
    yield return new WaitForSeconds(CFG.DIRECTOR_TIME_BEFORE_END);
    PlayerPrefs.SetString("Next", Application.loadedLevelName);
    Application.LoadLevel("Lose");
  }

  IEnumerator Win () {
    yield return new WaitForSeconds(CFG.DIRECTOR_TIME_BEFORE_END);
    if (HasNextLevel()) {
      PlayerPrefs.SetString("Next", GetNextLevelName());
      Application.LoadLevel("Win");
    } else if (IsFirstFinish()) {
      PlayerPrefs.SetInt("HaveFinished", 1);
      Application.LoadLevel("Finish");
    } else {
      Application.LoadLevel("LevelList");
    }
  }

  void SaveProgress () {
    int levelIndex = GetCurrentLevelIndex();

    if (PlayerPrefs.GetInt("Completed") < levelIndex + 1) {
      PlayerPrefs.SetInt("Completed", levelIndex + 1);
    }
  }

  int GetCurrentLevelIndex () {
    string name = Application.loadedLevelName;
    return int.Parse(name.Remove(0, 5));
  }

  string GetNextLevelName () {
    return "Level" + (GetCurrentLevelIndex() + 1);
  }

  bool HasNextLevel () {
    int total = Application.levelCount - CFG.LEVELS_HELPER_COUNT;
    return GetCurrentLevelIndex() < total - 1;
  }

  bool IsFirstFinish () {
    return !PlayerPrefs.HasKey("HaveFinished");
  }
}
