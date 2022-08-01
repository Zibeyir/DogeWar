using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("AA/AUDIO")]


public class AUDIO : MonoBehaviour
{
    public static AUDIO instance;

    public bool IsSoundOn = true;
    public bool IsMusicOn = true;

    [SerializeField] AudioSource bg_music;
    [SerializeField] AudioSource hairCut;
    [SerializeField] AudioSource fail;
    [SerializeField] AudioSource masinka;
    [SerializeField] AudioSource w_sound;
    //[SerializeField] AudioSource pan_sound;
    [SerializeField] AudioSource banana_sound;
    [SerializeField] AudioSource trasform_sound;
    [SerializeField] AudioSource wine_sound;
    [SerializeField] AudioSource bead_collect_sound;
    [SerializeField] AudioSource ark_sound;
    [SerializeField] AudioSource finish_win_sound;
    [SerializeField] AudioSource finish_clap_sound;
    [SerializeField] AudioSource smaylik_sound;
    [SerializeField] AudioSource break_sound;
    [SerializeField] AudioSource touch_sound;

    private float bg_music_initial_volume;
    private float bg_music_target_volume;
    [SerializeField] GameObject[] Sounds;
    [SerializeField] GameObject[] Music;
    public void BgActivated(bool bg)
    {
        if (bg)
        {
            bg_music.mute = false;
        }
        else
        {
            bg_music.mute = true;

        }
    }
    public enum SoundName
    {
        TRANSFORM,
        BANANA,
        CIRCLE,
        WINE,
        MASINKA,
        BACKGROUND,
        HAIRCUT,
        FAIL,
        BEADCOLLECT,
        ARK_SOUND,
        FINISH_WIN,
        FINISH_CLAP,
        SMAYLIK,
        BREAK,
        TOUCH
    }

    void Awake()
    {
        // print("AUDIOCODE");
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
       

    }

    private void Start()
    {
        bg_music.Play();
   

    }




    public void Play(SoundName name)
    {

        if (IsSoundOn)
        {


            switch (name)
            {
                case SoundName.TOUCH:
                    touch_sound.Play();
                    break;
                case SoundName.SMAYLIK:

                    smaylik_sound.Play();
                    break;
                case SoundName.BREAK:
                    break_sound.Play();
                    break;
                case SoundName.CIRCLE:
                    hairCut.Play();
                    break;
                case SoundName.WINE:
                    wine_sound.Play();
                    break;
                case SoundName.BANANA:
                    banana_sound.Play();
                    break;
                case SoundName.TRANSFORM:
                    trasform_sound.Play();
                    break;
                case SoundName.BACKGROUND:
                    bg_music.Play();
                    break;
                case SoundName.MASINKA:


                    // money_sound.volume = 1;
                    masinka.Play();
                    break;
                case SoundName.HAIRCUT:
                    hairCut.Play();
                    break;
                case SoundName.FAIL:
                    fail.Play();
                    break;
                case SoundName.BEADCOLLECT:
                    bead_collect_sound.Play();
                    break;
                case SoundName.ARK_SOUND:
                    ark_sound.Play();
                    break;
                case SoundName.FINISH_WIN:
                    finish_win_sound.Play();
                    break;
                case SoundName.FINISH_CLAP:
                    finish_clap_sound.Play();
                    break;
            }
        }
    }

    public void PlayArkSoundPitchUp()
    {
        ark_sound.pitch += 0.005f;
        ark_sound.Play();
    }

    public void ResetAudioSources()
    {
        ark_sound.pitch = 1.0f;
        bead_collect_sound.pitch = 1.0f;
        BgMusicDecrease(false);
    }

    public void BgMusicDecrease(bool val)
    {
        if (val) bg_music_target_volume = 0.0f;
        else bg_music_target_volume = bg_music_initial_volume;
    }
}
