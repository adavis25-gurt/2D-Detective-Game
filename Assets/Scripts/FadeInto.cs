using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInto : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public TextMeshProUGUI conditionText;
    public float wordSpeed = 0.06f;
    public PlayerController playerController;

    private string text = "You are a detective. Your goal is to speak to everyone, figure out what crime was committed, where it happened and who did it. But beware.. falsely accusing someone will lose you your job!";

    private void Awake()
    {
        StartCoroutine(Typing(text));
    }

    public async void FadeUI()
    {
        while (CanvasGroup.alpha < 1f)
        {
            CanvasGroup.alpha += 0.5f * Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

        CanvasGroup.alpha = 1f;
    }

    private void FadeOutUI() 
    {
        FadeText();
        FadeBackground();
    }

    async void FadeText()
    {
        conditionText.color = new Color(conditionText.color.r, conditionText.color.g, conditionText.color.b, 1);
        while (conditionText.color.a > 0f)
        {
            conditionText.color = new Color(conditionText.color.r, conditionText.color.g, conditionText.color.b, conditionText.color.a - (Time.deltaTime));
            await Awaitable.NextFrameAsync();
        }
        conditionText.color = new Color(conditionText.color.r, conditionText.color.g, conditionText.color.b, 0);
        playerController.canMove = true;

        await Awaitable.WaitForSecondsAsync(2);

        conditionText.text = "";
        conditionText.color = new Color(conditionText.color.r, conditionText.color.g, conditionText.color.b, 1);
    }

    async void FadeBackground()
    {
        while (CanvasGroup.alpha > 0f)
        {
            CanvasGroup.alpha -= 0.5f * Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }

        CanvasGroup.alpha = 0f;
    }

    IEnumerator Typing(string Text)
    {
        foreach (char letter in Text.ToCharArray())
        {
            conditionText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        if (conditionText.text == Text)
        {
            yield return new WaitForSeconds(5);
            while (conditionText.alpha < 1f)
            {
                conditionText.alpha += 0.5f * Time.deltaTime;
                yield return new WaitForSeconds(0.1f);
            }
        }

        FadeOutUI();
    }
}
    
