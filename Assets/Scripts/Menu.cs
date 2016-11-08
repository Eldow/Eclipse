using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Menu : MonoBehaviour {

    private int stage = 1;

    void Start()
    {
        GameObject start = GameObject.Find("Start");
        Load(); //Get current stage from playerInfo.dat
        if(stage <= 1)
        {
            //Write "Start Game"
            stage = 1;
            start.GetComponentInChildren<Text>().text = "START";

        } else if(stage > 1)
        {
            //Write "Continue"
            start.GetComponentInChildren<Text>().text = "CONTINUE";
        }
    }

    public void Resume()
    {
        SceneManager.LoadScene(stage+1);
    }

    public void Reset()
    {
        ResetSave(); //Reset current stage to 1
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadSandbox()
    {
        SceneManager.LoadScene(1);
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            stage = data.stage;
        }
    }

    void ResetSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData p = new PlayerData();
        p.stage = 1;
        bf.Serialize(file, p);
        file.Close();
    }
}
