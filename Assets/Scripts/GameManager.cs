using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public List<NPC> allNPCs;
    public List<GameObject> allLocations;
    public NPC currentNPC;
    
    private NPC culprit;

    public Fade Fade;
    public StateManager stateManager;
    public UnityEvent onCulpritDecided;

     public void PickCulprit()
    {
        culprit = stateManager.culprit;
        culprit.isCulprit = true;
        
        Debug.Log("Culprit: " + culprit);

        WriteDialogue();
    }
    
    public void WriteDialogue()
    {
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
            else if (stateManager.hasGotLocation && npc.transform.parent.name == stateManager.purseLocation)
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
                            "Yea I seen a purse around here somewhere",
                            "I did think it was really odd that someone left their purse...",
                            "I vaguely recall seeing something along those lines",
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
                            "Never seen that purse in my life.",
                            "Huh? Seen that purse before? No! Never!"
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