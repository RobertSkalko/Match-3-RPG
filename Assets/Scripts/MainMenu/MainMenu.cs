using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    public void Quit() {

        Application.Quit();

    }

    public void Options()
    {

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("LoadGame");
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene("NewGame");

    }
        



}
