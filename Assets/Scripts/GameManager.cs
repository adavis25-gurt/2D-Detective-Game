using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<NPC> allNPCs;
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
        
        person1.witness = person2;
        person2.witness = person1;
    }
}
    
    void WriteDialogue()
{
    foreach (NPC npc in allNPCs)
    {
        if (npc.isCulprit)
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
        else if (npc.witness != null)
        {
            string[] possibleWitnessLines = new string[]
            {
                "I was with " + npc.witness.npcName + " the whole time.",
                npc.witness.npcName + " and I were just hanging out all evening.",
                "We stayed together the entire night, " + npc.witness.npcName + " can vouch for me.",
                "I spent the night with " + npc.witness.npcName + ", nothing suspicious happened."
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


    public void Accuse(NPC accused)
    {
        if (accused == culprit)
        {
            Debug.Log("YOU WIN!");
        }
        else
        {
            Debug.Log("WRONG! GAME OVER!");
        }
    }
    
    public void AccuseCurrentNPC()
{
    if (currentNPC != null)
    {
        Accuse(currentNPC);
    }
}

}