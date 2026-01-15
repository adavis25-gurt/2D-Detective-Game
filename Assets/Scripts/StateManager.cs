using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class StateManager : MonoBehaviour
{
    public GameManager gameManager;
    public bool hasGotLocation, hasFoundPurse, hasGotLocation2;
    public string location1, purseLocation, location2;
    public NPC culprit;



    void Start()
    {
        Setup();
    }

    async void Setup()
    {
        List<GameObject> locations = gameManager.allLocations;

        int locationIndex = Random.Range(0, locations.Count);
        location1 = gameManager.allLocations[locationIndex].name;
        locations.Remove(locations[locationIndex]);

        locationIndex = Random.Range(0, locations.Count);
        purseLocation = locations[locationIndex].name;
        locations.Remove(locations[locationIndex]);

        locationIndex = Random.Range(0, locations.Count);
        location2 = locations[locationIndex].name; 
        locations.Remove(locations[locationIndex]);

        int culpritIndex = Random.Range(0, gameManager.allNPCs.Count);
        NPC possibleCulprit = gameManager.allNPCs[culpritIndex];

        while (possibleCulprit.transform.parent.name != location2)
        {
            culpritIndex = Random.Range(0, gameManager.allNPCs.Count);
            possibleCulprit = gameManager.allNPCs[culpritIndex];
            await Awaitable.NextFrameAsync();
        }

        culprit = possibleCulprit;

        print("LOCATION1: "+ location1 + " PURSELOCATION: " + purseLocation + " LOCATION2: " + location2 + " CULPRIT: " + culprit);
    }
}


//The robbery happened in location1, when you speak to the people in location1 they say that the purse is in the area
//After interacting with the purse, people will start saying they saw someone who lives in location2 with the purse
//You go to location2 and people there have the default text that we have already.