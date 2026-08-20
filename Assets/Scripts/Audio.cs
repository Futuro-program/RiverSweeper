using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    public static Audio inst;
    public float VolumeSons
    {
        get
        {
            return PlayerPrefs.GetFloat("Sons", 50) / 50;
        }
        set
        {
            PlayerPrefs.SetFloat("Sons", value);
            PlayerPrefs.Save();
        }
    }
    public float VolumeMusica {
        set {
            PlayerPrefs.SetFloat("Música", value);
            fontesAudio[2].volume = value / 50;
            PlayerPrefs.Save();
        }
    }
    AudioSource[] fontesAudio;

    void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        fontesAudio = GetComponents<AudioSource>();
    }

    public void TocarAudio(AudioClip clipe, float volume = 1)
    {
        fontesAudio[0].clip = clipe;
        fontesAudio[0].volume = volume * VolumeSons;
        fontesAudio[0].Play();
        fontesAudio[0].volume = VolumeSons;
    }

    public void TocarAudioLoop(AudioClip clipe, float volume = 1)
    {
        if (fontesAudio[1].clip == null)
        {
            fontesAudio[1].clip = clipe;
            fontesAudio[1].volume = volume * VolumeSons;
            fontesAudio[1].Play();
        }
    }

    public void PararAudioLoop()
    {
        fontesAudio[1].Stop();
        fontesAudio[1].volume = VolumeSons;
        fontesAudio[1].clip = null;
    }
}
