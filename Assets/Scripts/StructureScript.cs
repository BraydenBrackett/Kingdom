using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Controlling class for structures and their behaviors
 * 
 * @Author Brayden Brackett, September 9-26-2021
 * 
 * **/
public class StructureScript : MonoBehaviour
{
    public GameObject creator; //The character that created the building
    public float timer = 40f; //Universal resoruce timer that controls how long until an object produces something
    public float health = 100f; //Health of the object
    private float placeholder; //Secondary timer controlling variable

    //Determines team
    public bool isBlue; //Bool for if an object is of team blue
    private void Start()
    {
        placeholder = timer;
    }
    void Update()
    {
        //Checks for destruction and removes objects if health is less than 0
        if (health <= 0)
        {
            if (tag.Equals("Farm"))
            {
                creator.gameObject.GetComponent<Crafting>().removeFarm();
            }
            if (tag.Equals("Mine"))
            {
                creator.gameObject.GetComponent<Crafting>().removeMine();
            }
            Destroy(this.gameObject);
        }

        //Gives creator of object resources over timer time length
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (tag.Equals("Farm"))
            {
                creator.gameObject.GetComponent<Crafting>().addnormalWood();
            }
            if (tag.Equals("Mine"))
            {
                creator.gameObject.GetComponent<Crafting>().addIron();
                creator.gameObject.GetComponent<Crafting>().addRock();
            }
            timer = placeholder;
        }
    }
    
    /*
     * Sets the creator of an object
     * **/
    public void addCreator(GameObject creatorPerson)
    {
        creator = creatorPerson;
    }
}
