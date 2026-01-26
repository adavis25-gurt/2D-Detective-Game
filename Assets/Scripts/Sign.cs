using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public Tile sign;
    public Vector3 position;
    public Tilemap tilemap;
    public PlayerController playerController;
    public StateManager stateManager;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueNPCName;
    public TextMeshProUGUI dialogueLocation;
    public Button accuse;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    public string npcName;

    private bool isTyping = false;
    private bool canSkip = false;
    private bool dialogueOpen = false;
    private void Start()
    {
        dialogueText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !dialogueOpen)
        {
            dialoguePanel.SetActive(true);
            accuse.gameObject.SetActive(false);
            dialogueOpen = true;
            canSkip = true;
            dialogueNPCName.text = npcName;
            dialogueLocation.text = "";
            index = 0;
            StartCoroutine(Typing());
            playerController.canMove = false;
        }

        if (Input.GetMouseButtonDown(0) && canSkip)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogue[index];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    IEnumerator Typing()
    {
        isTyping = true;

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false;
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }
    }

    public void ZeroText()
    {
        accuse.gameObject.SetActive(true);
        dialogueText.text = "";
        dialogueNPCName.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        canSkip = false;
        isTyping = false;
        dialogueOpen = false;
        playerController.canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
