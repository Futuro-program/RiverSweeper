using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio inst;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TocarAudio(AudioClip clipe, float volume = 1)
    {
        float volAbs = PlayerPrefs.GetFloat("Sons", 50) / 50;

        fontesAudio[0].clip = clipe;
        fontesAudio[0].volume = volume * volAbs;
        fontesAudio[0].PlayScheduled(AudioSettings.dspTime + 0.2);
        fontesAudio[0].clip = null;
    }

    public void TocarAudioLoop(AudioClip clipe, float volume = 1)
    {
        float volAbs = PlayerPrefs.GetFloat("Sons", 50) / 50;

        fontesAudio[1].clip = clipe;
        fontesAudio[0].volume = volume * volAbs;
        fontesAudio[1].Play();
    }

    public void PararAudioLoop()
    {
        float volAbs = PlayerPrefs.GetFloat("Sons", 50) / 50;

        fontesAudio[1].Stop();
        fontesAudio[1].volume = volAbs;
        fontesAudio[1].clip = null;
    }
}
