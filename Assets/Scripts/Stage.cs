using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour {
    public PlayerData data = new PlayerData();
    public static Stage instance;
    public AudioClip stageMusic;
    public GameObject options;
    // Use this for initialization
    void Awake()
    {
        instance = this;
        GetComponent<Fade>().alpha = 1.0f;
        if (stageMusic != null)
            SoundManager.instance.PlayMusic(stageMusic);
        else
            SoundManager.instance.PlayMusic(null);
        Save();
        Switcher.instance.prof = GameObject.Find("Prof");
        Switcher.instance.profShadow = GameObject.Find("ProfShadow");
        Switcher.instance.Setup();
    }
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Menu") && options != null)
        {
            if (!options.activeInHierarchy)
                options.SetActive(true);
            else
                options.SetActive(false);
        }
	}

    public void ResetStage()
    {
        if(options != null)
            options.SetActive(false);
        StartCoroutine(FadedLoad(data.stage));
    }

    IEnumerator FadedLoad(int stage)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(stage);
    }
    public void NextStage()
    {
        StartCoroutine(FadedLoad(data.stage + 1));
    }

    public void MainMenu()
    {
        options.SetActive(false);
        StartCoroutine(FadedLoad(0));
    }

    public void Resume()
    {
        options.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }

    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        bf.Serialize(file, data);
        file.Close();
    }

}
