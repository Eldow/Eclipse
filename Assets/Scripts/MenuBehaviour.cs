using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {

	public void LoadLevelByIndex(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadLevelByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
