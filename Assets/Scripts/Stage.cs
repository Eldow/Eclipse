using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour {
    public PlayerData data = new PlayerData();
    public static Stage instance;
    public AudioClip stageMusic;
    // Use this for initialization
    void Awake()
    {
        //Check if there is already an instance of Stage
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set Stage to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
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
        if (Input.GetKeyDown("escape"))
        {
            ResetStage();
        }
	}

    public void ResetStage()
    {
        StartCoroutine(FadedLoad(data.stage));
    }

    IEnumerator FadedLoad(int stage)
    {
        float fadeTime = gameObject.GetComponent<Fade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(stage);
    }
    public void NextStage()
    {
        StartCoroutine(FadedLoad(data.stage + 1));
    }

    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        bf.Serialize(file, data);
        file.Close();
    }

}
