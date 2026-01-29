using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup CanvasGroup;

    public void ExitButton()
    {
        Application.Quit();
    }

    public async void StartGame()
    {
        while (CanvasGroup.alpha < 1f)
        {
            CanvasGroup.alpha += 0.5f * Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

        CanvasGroup.alpha = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
