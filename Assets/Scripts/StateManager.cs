using UnityEngine;

public class StateManager : MonoBehaviour
{
    public GameManager gameManager;
    public bool hasGotLocation, hasFoundPurse, hasGotLocation2;
    public string location1, purseLocation, location2, culprit;



    void Start()
    {
        int locationIndex = Random.Range(0, gameManager.allLocations.Count);
        location1 = gameManager.allLocations[locationIndex].name;
    }

}
