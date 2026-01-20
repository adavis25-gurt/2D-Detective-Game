using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PurseSpawner : MonoBehaviour
{
    public Tile purse;
    public Vector3 position;
    public Tilemap tilemap;
    public GameManager gameManager;
    public PlayerController playerController;
    public StateManager stateManager;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueNPCName;
    public Button accuse;
    public string[] dialogue;
    private int index;
    public float wordSpeed;
    public bool playerIsClose;
    public string npcName;
    public GameObject purseParent;

    private bool isTyping = false;
    private bool canSkip = false;
    private bool dialogueOpen = false;
    public List<GameObject> spawnPoints = new List<GameObject>();
    private void Start()
    {
        dialogueText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !dialogueOpen && stateManager.hasGotLocation)
        {
            dialogue = new string[] { "This must be the purse that was stolen! *ACQUIRED PURSE*" };
            dialoguePanel.SetActive(true);
            accuse.gameObject.SetActive(false);
            dialogueOpen = true;
            canSkip = true;
            dialogueNPCName.text = npcName;
            stateManager.hasFoundPurse = true;
            StartCoroutine(Typing());
            gameManager.WriteDialogue();
            playerController.canMove = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !dialogueOpen && !stateManager.hasGotLocation)
        {
            dialogue = new string[] { "Hm someone must've dropped their purse!" };
            accuse.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            dialogueOpen = true;
            canSkip = true;
            dialogueNPCName.text = npcName;
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

    public void SpawnPurse()
    {
        string location = stateManager.purseLocation;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (spawnPoint.transform.parent.parent.name == location)
            {
                List<GameObject> availableSpawns = new List<GameObject>();
                availableSpawns.Add(spawnPoint);
                int randomIndex = Random.Range(0, availableSpawns.Count);
                Vector3 spawnLocationTmp = availableSpawns[randomIndex].transform.position;
                Vector3Int spawnLocation = new Vector3Int(Mathf.RoundToInt(spawnLocationTmp.x), Mathf.RoundToInt(spawnLocationTmp.y), Mathf.RoundToInt(spawnLocationTmp.z));

                tilemap.SetTile(spawnLocation, purse);
                print("tile set");
            }
            else
            {
                print(location + " " + spawnPoint.transform.parent.parent.name);
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
        if (stateManager.hasFoundPurse)
        {
            purseParent.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
            print(playerIsClose);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            print(playerIsClose);
        }
    }
}
