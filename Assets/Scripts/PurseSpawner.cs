using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PurseSpawner : MonoBehaviour
{
    public Tile purse;
    public Vector3 position;
    public Tilemap tilemap;
    public StateManager stateManager;
    public List<GameObject> spawnPoints = new List<GameObject>();
    public bool playerIsClose;

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
