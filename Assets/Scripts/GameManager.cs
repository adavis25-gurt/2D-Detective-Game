using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<NPC> allNPCs;
    public List<GameObject> allLocations;
    public NPC currentNPC;
    
    private NPC culprit;

    public Fade Fade;
    public StateManager stateManager;


    async void Start()
    {
        await PickCulprit();
        GiveEveryoneElseAWitness();
        await WriteDialogue();
    }
     async Task PickCulprit()
    {
        while (stateManager.culprit == null)
        {
            await Task.Yield();
        }


        culprit = stateManager.culprit;
        culprit.isCulprit = true;
        
        Debug.Log("Culprit: " + culprit);
    }
    
    void GiveEveryoneElseAWitness()
    {

        string location = stateManager.location1;

        List<NPC> innocentNPCs = new List<NPC>();
        
        foreach (NPC npc in allNPCs)
        {
            if (npc != culprit)
            {
                innocentNPCs.Add(npc);
            }
        }
        
        List<NPC> unpaired = new List<NPC>(innocentNPCs);
        
        while (unpaired.Count >= 2)
        {
            NPC person1 = unpaired[0];
            unpaired.RemoveAt(0);
            
            int randomIndex = Random.Range(0, unpaired.Count);
            NPC person2 = unpaired[randomIndex];
            unpaired.RemoveAt(randomIndex);
            
            int locationIndex = Random.Range(0, allLocations.Count);
            GameObject sharedLocation = allLocations[locationIndex];
            
            person1.witness = person2;
            person1.alibiLocation = sharedLocation;
            
            person2.witness = person1;
            person2.alibiLocation = sharedLocation;
        }
        
        if (culprit != null)
        {
            List<NPC> availableNPCs = new List<NPC>(innocentNPCs);
            
            if (availableNPCs.Count > 0)
            {
                int fakeWitnessIndex = Random.Range(0, availableNPCs.Count);
                culprit.witness = availableNPCs[fakeWitnessIndex];
                
                int fakeLocationIndex = Random.Range(0, allLocations.Count);
                culprit.alibiLocation = allLocations[fakeLocationIndex];
            }
        }
    }
    
    public async Task WriteDialogue()
    {
        await Task.Delay(3000);

        foreach (NPC npc in allNPCs)
        {
            if (!stateManager.hasGotLocation && !stateManager.hasFoundPurse && !stateManager.hasGotLocation2) //Start of the game
            {
                if (npc.transform.parent.name == stateManager.location1)
                {
                    string[] possibleLines = new string[]
                        {
                            "Yea I heard some crime happened here.. apparently theres a purse in " + stateManager.purseLocation,
                            "I'm scared to go out at night! A woman got robbed for her purse in this area!",
                            "A little birdy told me you can find the purse in " + stateManager.purseLocation + " still.. not sure how true it is though.",
                            "Please solve this! I feel so unsafe...",
                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                }
                else
                {
                    string[] possibleLines = new string[]
                        {
                            "It's been all over the news, someone got robbed somewhere near " + stateManager.location1,
                            stateManager.location1 + " has been getting more unsafe day by day!",
                            "I'll never go to visit " + stateManager.location1 + " anymore.. not after what happened..",
                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                }
            }
            else if (stateManager.hasGotLocation2 && stateManager.hasFoundPurse && npc.transform.parent.name == stateManager.location2) //End of the game
            {
                if (npc.isCulprit)
                {
                    string[] possibleLines = new string[]
                        {
                            "Purse? What purse? I haven't seen any purse laying around!",
                            "I ain't seen no purse! Why are you asking me?",
                            "Nope! No purse here haha! Nothing here! haha.. ha..",
                            "I haven't seen a purse..",
                            "Purse? W-What? uhh I- I don't know.."
                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                }
                else
                {
                    string[] possibleLines = new string[]
                    {
                        "Purse? No I'd never steal a purse!"
                    };
                    int randomIndex = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[randomIndex] };
                }
            }
        }
    }
    
    public void Accuse()
    {
        NPC accused = currentNPC;
        Debug.Log(accused);
        if (accused == culprit)
        {
            Debug.Log("yea you right");
            Fade.FadeUI(true);
        }
        else
        {
            Debug.Log("WRONG! GAME OVER!");
            Fade.FadeUI(false);
        }
    }
}