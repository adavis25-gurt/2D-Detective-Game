using System.Collections.Generic;
using UnityEngine;

public class NameRandomiser : MonoBehaviour
{
    public GameManager gameManager;
    public List<NPC> allNPCs;
    private List<string> names = new List<string>
    {
    "Olivia","Liam","Emma","Noah","Ava","Lucas","Sophia","Mason","Isabella","Ethan",
        "Mia","Logan","Charlotte","James","Amelia","Benjamin","Harper","Elijah","Evelyn","Alexander",
     "Abigail","William","Ella","Daniel","Scarlett","Michael","Grace","Matthew","Chloe","Henry",
     "Victoria","Jackson","Riley","Sebastian","Aria","Jack","Lily","Owen","Aurora","Samuel",
    "Zoey","David","Penelope","Joseph","Lillian","Carter","Addison","Wyatt","Natalie","Caleb",
    "Hannah","Luke","Zoe","Gabriel","Leah","Isaac","Stella","Anthony","Nora","Dylan",
    "Ellie","Nathan","Hazel","Christian","Violet","Jonathan","Savannah","Levi","Aaron","Brooklyn",
    "Thomas","Bella","Charles","Claire","Cameron","Skylar","Connor","Lucy","Adrian","Paisley",
    "Jordan","Audrey","Nicholas","Anna","Evan","Sadie","Ian","Genesis","Hunter","Aaliyah",
    "Eli","Alice","Landon","Kennedy","Caroline","Nolan","Emery","Adam","Quinn","Leo",
    "Peyton","Elias","Ruby","Xavier","Sophie","Jeremiah","Nevaeh","Josiah","Eva","Christopher",
    "Autumn","Colton","Madeline","Angel","Hailey","Roman","Maya","Brayden","Riley","Ashton",
    "Gabriella","Nathaniel","Sarah","Gavin","Allison","Tyler","Madelyn","Miles","Ariana","Lincoln",
    "Ezekiel","Kaylee","Jason","Molly","Ryan","Reagan","Calvin","Paisley","Chase","Savannah",
    "Austin","Naomi","Hunter","Aubrey","Diego","Lydia","Jaxon","Elena","Mateo","Carolina",
    "Sebastian","Aurora","Micah","Faith","Vincent","Makayla","Luis","Julia","Damian","Madeline",
    "Ryder","Willow","Dominic","Vera","Braxton","Clara","Jace","Liliana","Everett","Catherine",
    "Silas","Mackenzie","Maxwell","Ruby","Emmett","Bailey","Zachary","Isla","Tristan","Delilah",
    "Eric","Josephine","Nicolas","Valeria","Kingston","Gianna","Peter","Margaret","Bryce","Samantha",
    "Jude","Eliana","Cole","Lila","Elliot","Amaya","Asher","Eden","Beau","Sienna",
    "Declan","Rylee","Cody","Katherine","Tobias","Vivian","Grayson","Harmony","Sawyer","Jasmine",
    "Harrison","Brianna","Miles","Clara","Gabriel","Aurora","Alexis","Levi","Madison","Sophia",
    "Nicholas","Scarlett","Maverick","Penelope","Camila","Ezra","Elizabeth","Mateo","Addison","Luna",
    "Carson","Aubrey","Julian","Hazel","Easton","Violet","Cooper","Nova","Lincoln","Aurora",
    "Landon","Bella","Hudson","Stella","Ashton","Sophie","Greyson","Nora","Jordan","Paisley"
    };

    void Awake()
    {
        allNPCs = gameManager.allNPCs;
        RandomiseNPCs();
    }

    void RandomiseNPCs()
    {
        foreach (NPC npc in allNPCs)
        {
            int randomIndex = Random.Range(0, names.Count);
            string name = names[randomIndex];
            npc.npcName = name;
            npc.name = name;
            names.Remove(name);
        }
    }
}
