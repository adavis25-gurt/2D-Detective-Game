using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<NPC> allNPCs;
    public List<GameObject> allLocations;
    public NPC currentNPC;
    
    private NPC culprit;
    
    void Start()
    {
        PickCulprit();
        GiveEveryoneElseAWitness();
        WriteDialogue();
    }
    
    void PickCulprit()
    {
        int random = Random.Range(0, allNPCs.Count);
        culprit = allNPCs[random];
        culprit.isCulprit = true;
        
        Debug.Log("Culprit: " + culprit.npcName);
    }
    
    void GiveEveryoneElseAWitness()
    {
        List<NPC> innocentNPCs = new List<NPC>();
        
        foreach (NPC npc in allNPCs)
        {
            if (!npc.isCulprit)
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
    
    void WriteDialogue()
    {
        foreach (NPC npc in allNPCs)
        {
            if (npc.isCulprit)
            {
                if (npc.witness != null && npc.alibiLocation != null)
                {
                    string[] possibleCulpritLines = new string[]
                    {
                        "I was with " + npc.witness.npcName + " in " + npc.alibiLocation.name + " all day.",
                        npc.witness.npcName + " and I hung out in " + npc.alibiLocation.name + " yesterday.",
                        "I spent the whole time with " + npc.witness.npcName + " over in " + npc.alibiLocation.name + ".",
                        npc.witness.npcName + " can vouch for me, we were in " + npc.alibiLocation.name + " together."
                    };
                    int randomIndex = Random.Range(0, possibleCulpritLines.Length);
                    npc.dialogue = new string[] { possibleCulpritLines[randomIndex] };
                }
                else
                {
                    string[] possibleCulpritLines = new string[]
                    {
                        "I was at the market.",
                        "I was walking my dog.",
                        "I was reading a book at home.",
                        "I don't remember much about that night."
                    };
                    int randomIndex = Random.Range(0, possibleCulpritLines.Length);
                    npc.dialogue = new string[] { possibleCulpritLines[randomIndex] };
                }
            }
            else if (npc.witness != null)
            {
                string[] possibleWitnessLines = new string[]
                {
                    "I was with " + npc.witness.npcName + " in " + npc.alibiLocation.name + " the whole time.",
                    npc.witness.npcName + " and I were hanging out in " + npc.alibiLocation.name + " all evening.",
                    "We stayed together in " + npc.alibiLocation.name + " the entire night, " + npc.witness.npcName + " can vouch for me.",
                    "I spent the night with " + npc.witness.npcName + " in " + npc.alibiLocation.name + ", nothing suspicious happened."
                };
                int randomIndex = Random.Range(0, possibleWitnessLines.Length);
                npc.dialogue = new string[] { possibleWitnessLines[randomIndex] };
            }
            else
            {
                npc.dialogue = new string[] { "I don't know anything." };
            }
        }
    }
    
    public void Accuse()
    {
        NPC accused = currentNPC;
        Debug.Log(accused);
        if (accused == culprit)
        {
            Debug.Log("YOU WIN!");
        }
        else
        {
            Debug.Log("WRONG! GAME OVER!");
        }
    }
}