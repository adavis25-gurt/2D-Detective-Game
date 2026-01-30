using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<NPC> allNPCs;
    public List<GameObject> allLocations;
    public NPC currentNPC;

    private NPC culprit;

    public Fade Fade;
    public StateManager stateManager;
    public PurseSpawner purseSpawner;
    public UnityEvent onCulpritDecided;

    public Button accuse;

    public void PickCulprit()
    {
        culprit = stateManager.culprit;
        culprit.isCulprit = true;


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
                            "I'm worried to walk around with any type of bag thanks to the crime in " + stateManager.purseLocation + ".",
                            "People keep whispering about that missing purse... they say it’s somewhere around " + stateManager.purseLocation + ".",
                            "I really hope you’re looking into that purse situation... everyone’s on edge lately.",
                            "Crime around here has gotten worse... that stolen purse is all anyone talks about.",
                            "Someone mentioned the purse might still be lying around in " + stateManager.purseLocation + ". Maybe check it out.",
                            "I can’t believe someone would do that here... please find the purse and help us feel safe again.",
                            "Rumor has it the thief dropped the purse near " + stateManager.purseLocation + ".",
                            "Ever since that purse went missing, I’ve been double‑checking my surroundings constantly.",
                            "People say the purse wasn’t taken far... maybe " + stateManager.purseLocation + " has clues.",
                            "I wish things were peaceful again... maybe recovering that purse will calm everyone down.",
                            "I heard shouting the night it happened...  maybe the purse ended up in " + stateManager.purseLocation + ".",
                            "Everyone’s been acting nervous since the purse was stolen... can you do something about it?",
                            "Some folks swear they saw someone running toward " + stateManager.purseLocation + " with a bag.",
                            "I don’t feel safe letting my kids out lately... please figure out what happened with that purse.",
                            "If you’re investigating, you might want to start with " + stateManager.purseLocation + ". Just saying."

                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                    purseSpawner.SpawnPurse();
                }
                else
                {
                    string[] possibleLines = new string[]
                        {
                            "It's been all over the news, someone got robbed somewhere near " + stateManager.location1,
                            stateManager.location1 + " has been getting more unsafe day by day!",
                            "I'll never go to visit " + stateManager.location1 + " anymore.. not after what happened..",
                            "It's scary hearing what happened around " + stateManager.location1 + ". I hope you're being careful.",
                            "People keep talking about that incident in " + stateManager.location1 + ". No one feels safe anymore.",
                            "I used to love going to " + stateManager.location1 + ", but now... I’m not so sure.",
                            "Everyone’s been on edge since that situation in " + stateManager.location1 + ".",
                            "I heard someone saw something suspicious in " + stateManager.location1 + " recently.",
                            "Whatever happened in " + stateManager.location1 + " has everyone worried.",
                            "Some folks say " + stateManager.location1 + " isn’t what it used to be.",
                            "I really hope things calm down in " + stateManager.location1 + ". It’s getting unsettling.",
                            "People are avoiding " + stateManager.location1 + " lately... can’t blame them.",
                            "I don’t like walking past " + stateManager.location1 + " anymore. Gives me a bad feeling.",
                            "Did you hear the rumors about " + stateManager.location1 + "? It’s all anyone talks about.",
                            "I wish the authorities would do something about what's happening in " + stateManager.location1 + ".",
                            "Something’s definitely off in " + stateManager.location1 + " these days.",
                            "Everyone keeps warning me to stay away from " + stateManager.location1 + ".",
                            "I hope you're not planning to head toward " + stateManager.location1 + " alone."

                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                }
            }
            else if (stateManager.hasGotLocation && !stateManager.hasFoundPurse) //if you got first location and you speak to anyone without the purse
            {
                if (npc.isCulprit)
                {
                    string[] possibleLines = new string[]
                        {
                            "I haven't seen no purse around here.",
                            "Have I seen a purse? Maybe? I dunno.",
                            "I mean... a purse? Not that I remember. Why?",
                            "Can’t say I’ve noticed any purse. Been busy... doing stuff.",
                            "A purse? Huh. Weird question. Why you asking?",
                            "Maybe I saw something... maybe I didn’t. Hard to say.",
                            "People lose things all the time. Doesn’t mean I saw it.",
                            "Could’ve been a purse... or maybe just trash. Who knows.",
                            "I don’t really pay attention to that kinda thing.",
                            "Someone mentioned a purse earlier, but I wasn’t really listening.",
                            "I might’ve seen someone carrying something... couldn’t tell what.",
                            "Depends... what does the purse look like?",
                            "I don’t think I saw a purse. Pretty sure. Mostly sure.",
                            "Why’s everyone suddenly asking me about a purse?",
                            "If there *was* a purse, it wasn’t here long.",
                            "I didn’t see anything unusual... well, nothing I’d call unusual.",
                            "Look, if I saw a purse, I’d tell you."

                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                }
                else if (npc.transform.parent.name == stateManager.purseLocation)
                {
                    string[] possibleLines = new string[]
                        {
                            "Yea I seen a purse on the floor somewhere.",
                            "I vaguely recall seeing something along the lines of a bag somewhere.",
                            "Maybe I saw a bag in this area? I'm not sure..",
                            "Sorry, I haven't seen anything.",
                            "EVERYONE CALLED ME CRAZY.. I KNOW I SAW A BAG IN THIS LOCATION!",
                            "There's a purse around here, you should question people when you have the bag",
                            "Yes! The purse is here! Please bring that criminal to justice!",
                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                }
                else
                {
                    string[] possibleLines = new string[]
                        {
                            "I think I walked past a purse at some point",
                            "I don't recall seeing any purse anywhere... sorry..",
                            "I know there's nothing in this area at least..",
                            "Nope, I've not seen a purse anywhere.",
                            "Uhh........ I've got nothing sorry.",
                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                }
            }
            else if (!stateManager.hasGotLocation2 && stateManager.hasFoundPurse && npc.transform.parent.name != stateManager.location2) //if you speak to anyone except the final location with the purse
            {
                string[] possibleLines = new string[]
                    {
                        "I saw that purse in the area somewhere",
                        "Yea that purse looks pretty familiar",
                        "Sorry I haven't seen that purse before",
                        "I think I walked past that purse at some point.",
                        "I just assumed someone had dropped their purse so I left it",
                        "I dont recognise that purse, sorry.",
                        "Nope, haven't seen it before.",
                        "I got nothing to do with that purse",
                        "Uh.. uhm.. I think I saw it?",
                        "Purse? I haven't even got one!",
                        "Purse-onally I havent seen it",
                        "You need some more Purse-nality! HAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHA I haven't seen that purse before."
                    };
                int randomIndex = Random.Range(0, possibleLines.Length);
                npc.dialogue = new string[] { possibleLines[randomIndex] };
            }
            else if (!stateManager.hasGotLocation2 && stateManager.hasFoundPurse && npc.transform.parent.name == stateManager.location2) //If you speak to anyone in the final location with the purse
            {
                if (npc.isCulprit)
                {
                    string[] possibleLines = new string[]
                        {
                            "Never seen that purse in my life.",
                            "Huh? Seen that purse before? No! Never!",
                            "Uhm.... that bag makes me recall nothing",
                            "That purse is completely foreign to me",
                            "That purse looks crazy expensive..",
                            "Who would steal such a nice purse? That's preposterous!",
                            "Genuinely I have no idea who's purse that is!"
                        };
                    int Index = Random.Range(0, possibleLines.Length);
                    npc.dialogue = new string[] { possibleLines[Index] };
                }
                else
                {
                    string[] possibleLines = new string[]
                    {
                        "Purse? Why would I steal a purse..",
                        "No sir, I haven't seen nor stolen that purse",
                        "I was at home all day when it happened",
                        "I wasn't even in the area at all..",
                        "I haven't got a need for a purse, so no, I didn't steal it.",
                        "That's my neighbours purse!!!",
                        "Sorry I've never really seen that purse before",
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
        accuse.gameObject.SetActive(false);
        if (accused == culprit)
        {
            Fade.FadeUI(true);
        }
        else
        {
            Fade.FadeUI(false);
        }
    }
}