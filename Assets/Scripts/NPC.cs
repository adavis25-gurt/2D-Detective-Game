using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueNPCName;
    public GameManager gameManager;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    public string npcName;
    public bool isCulprit = false;
    public StateManager stateManager;
    public NPC witness;
    public GameObject alibiLocation;
    
    private bool isTyping = false;
    private bool canSkip = false;
    private bool dialogueOpen = false;
    
    void Start()
    {
        dialogueText.text = "";
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !dialogueOpen)
        {
            gameManager.WriteDialogue();
            dialoguePanel.SetActive(true);
            dialogueOpen = true;
            canSkip = true;
            dialogueNPCName.text = npcName;
            StartCoroutine(Typing());
            if (gameManager != null)
            {
                gameManager.currentNPC = this;
            }

            if (gameManager.currentNPC.transform.parent.name == stateManager.location1)
            {
                stateManager.hasGotLocation = true;
            }
        }
        
        if (Input.GetMouseButtonDown(0) && canSkip && !IsPointerOverUIButton())
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
    
    private bool IsPointerOverUIButton()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
            {
                return true;
            }
        }
        return false;
    }
    
    public void ZeroText()
    {
        dialogueText.text = "";
        dialogueNPCName.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        canSkip = false;
        isTyping = false;
        dialogueOpen = false;
    }
    
    IEnumerator Typing()
    {
        isTyping = true;
        
        foreach(char letter in dialogue[index].ToCharArray())
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
            ZeroText();
        }
    }
}