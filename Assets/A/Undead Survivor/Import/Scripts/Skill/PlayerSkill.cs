using System.Collections.Generic;
using UnityEngine;

public struct Ability
{
  public string name;
  public string description;
  public int requiredLevel;
  public bool unlocked;

  // define a method to initialize the skill with 4 values
  public void Initialize(string name, string description, int requiredLevel, bool unlocked)
  {
    this.name = name;
    this.description = description;
    this.requiredLevel = requiredLevel;
    this.unlocked = unlocked;
  }
}

public class PlayerSkill : MonoBehaviour
{
    public PlayerController player;
  // define a list of skills that the player can unlock
  public List<Ability> skills;

  // define a variable for the player's current level
  public int level;

  void Start()
  {
    // initialize the list of skills
    skills = new List<Ability>();

    // create a new skill and initialize it with 4 values
    Ability skill = new Ability();
    skill.Initialize("Fireball", "Shoot a fireball at your enemies.", 3, false);
    skills.Add(skill);

    // create another skill and initialize it with 4 values
    skill = new Ability();
    skill.Initialize("Heal", "Heal yourself or a ally.", 5, false);
    skills.Add(skill);
  }

  // define a function to handle unlocking new skills for the player
  public void CheckForNewSkills()
  {
    // loop through the list of skills
    for (int i = 0; i < skills.Count; i++)
    {
      // check if the player has reached the required level to unlock the skill
      if (player.level >= skills[i].requiredLevel && !skills[i].unlocked)
      {
        // unlock the skill
        //skill[i].
        Debug.Log(skills[i].name + " unlocked!");
      }
    }
  }

 

  // define a function to load the list of skills from a file
  //public List<Skill> LoadSkills()
 // {

    // add code to load the list of skills from a file here
  //}
}