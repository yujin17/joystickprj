using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject settingGams;
    public GameObject infoGams;
    public GameObject music;
    public AudioSource musicsource;
    public GameObject mainmenuGame;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {

    }


    public void OnClickNewGame()
    {
        if (musicsource.isPlaying==false)
        {
            musicsource.Play();
            DontDestroyOnLoad(musicsource); //������� ��� ����ϰ�(���� ��ư�Ŵ������� ����)
        }
        
        SceneManager.LoadScene("GameScene");
    }
    
    public void OnClickSetting()
    {
        
        settingGams.SetActive(true);
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickInformation()
    {
        infoGams.SetActive(true);
    }

}
