using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource musicsource;


    public void Awake()
    {
        
        //musicsource = backMusic.GetComponent<AudioSource>(); //������� �����ص�
        if (musicsource.isPlaying) return; //��������� ����ǰ� �ִٸ� �н�
        else
        {
            musicsource.Play();
            DontDestroyOnLoad(musicsource); //������� ��� ����ϰ�(���� ��ư�Ŵ������� ����)
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;

    }
}
