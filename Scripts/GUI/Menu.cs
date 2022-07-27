using UnityEngine;
using System.Collections;


public class Menu : TouchableItems {

  public GUITexture mPlayItem;
  public GUITexture mQuitItem;
  public GUITexture mSoundItem;
  public GUITexture mHeyzapItem;

  public Texture2D mSoundOnTex;
  public Texture2D mSoundOffTex;

  ArrayList mItems;

  void Start () {
    HeyzapLib.load();
    mItems = new ArrayList();
    mItems.Add(mPlayItem);
    mItems.Add(mQuitItem);
    mItems.Add(mSoundItem);
    mItems.Add(mHeyzapItem);

    SetSoundItemTexture();
  }

  void Update () {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.Quit();
    }
    ProccessTouches();
  }

  protected override ArrayList GetGUITextures () {
    return mItems;
  }

  protected override void OnItemSelected (GUITexture tex) {
    if (tex == mPlayItem) {
      OnPlayItemSelected();
    } else if (tex == mQuitItem) {
      OnQuitItemSelected();
    } else if (tex == mSoundItem) {
      OnSoundItemSelected();
    } else if (tex == mHeyzapItem) {
      HeyzapLib.checkin();
    }
  }

  void OnPlayItemSelected () {
    Application.LoadLevel("LevelList");
  }

  void OnSoundItemSelected() {
    if (SoundController.IsOn()) {
      SoundController.Mute();
    } else {
      SoundController.Unmute();
    }
    SetSoundItemTexture();
  }

  void SetSoundItemTexture () {
    if (SoundController.IsOn()) {
      mSoundItem.texture = mSoundOnTex;
    } else {
      mSoundItem.texture = mSoundOffTex;
    }
  }

  void OnQuitItemSelected () {
    Application.Quit();
  }
}

