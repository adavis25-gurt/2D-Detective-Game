using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
        print("game closed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
