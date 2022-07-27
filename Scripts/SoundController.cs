using UnityEngine;


public class SoundController: MonoBehaviour {

  static AudioSource Music = null;
  static AudioSource ShootFx = null;

  public static bool IsOn () {
    if (!Music || !ShootFx) { // For debuging purpose only
      return false;
    }
    return !Music.mute && !ShootFx.mute;
  }

  public static void Mute () {
    SetMute(true);
  }

  public static void Unmute () {
    SetMute(false);
  }

  public static void PlayShootFx () {
    if (ShootFx && IsOn()) {
      ShootFx.Play();
    }
  }

  static void SetMute (bool val) {
    if (Music) {
      Music.mute = val;
    }
    if (ShootFx) {
      ShootFx.mute = val;
    }
  }

  public AudioClip mSoundFxClip;

  void Awake () {
    if (Music == null && ShootFx == null) {
      AudioSource[] sources =GetComponents<AudioSource>();

      foreach (AudioSource source in sources) {
        if (source.clip == mSoundFxClip) {
          ShootFx = source;
        } else {
          Music = source;
        }
      }
      DontDestroyOnLoad(Music);
      DontDestroyOnLoad(ShootFx);
    }
  }
}
