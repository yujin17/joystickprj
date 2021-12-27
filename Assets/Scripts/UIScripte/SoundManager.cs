using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
     static public SoundManager instance;

    public AudioClip[] soundclips;

    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
    public void Awake()
    {

        //musicsource = backMusic.GetComponent<AudioSource>(); //������� �����ص�
        //if (instance!=null) //��������� ����ǰ� �ִٸ� �н�
        //{
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //    instance = this;
        //}
    }

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(int playMusicTrack)
    {
        source.clip = soundclips[playMusicTrack];
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
    }
}
