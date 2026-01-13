using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public CanvasGroup CanvasGroup;
    public TextMeshProUGUI conditionText;
    public float wordSpeed = 0.06f;

    private string failText =
        "yea bro lowkeniunely you failed and like ill chganege this script at some point in the future (probably) so like yea you fired bro";

    private string winText =
        "ok ur kinda cool i guess you figured oiut the right guy";

    public async void FadeUI(bool Win)
    {
        while (CanvasGroup.alpha < 1f)
        {
            CanvasGroup.alpha += 0.5f * Time.deltaTime;
            await Awaitable.NextFrameAsync();
            print("its working");
        }

        CanvasGroup.alpha = 1f;

        if (Win)
        {
            StartCoroutine(Typing(winText));
        }
        else
        {
            StartCoroutine(Typing(failText));
        }
       
    }

    IEnumerator Typing(string Text)
    {
        foreach(char letter in Text.ToCharArray())
        {
            conditionText.text += letter;
            print(wordSpeed);
            yield return new WaitForSeconds(wordSpeed);
        }
    }
}
