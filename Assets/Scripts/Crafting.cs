using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that handles crafting for all objects
 * 
 * @Author Brayden Brackett, September 9-26-2021
 * 
 * **/
public class Crafting : MonoBehaviour
{
    //Resource trackers
    public int NORMAL_WOOD = 0;
    public int ROCK = 0;
    public int METAL = 0;

    //Craftables
    public GameObject farm;
    public GameObject mine;

    //Max of each craftable
    public int maxFarms = 3, currentFarms = 0;
    public int maxMines = 3, currentMines = 0;

    //Checks for certain build conditions to build objects. If they're true, it builds the object.
    private void Update()
    {
        if(NORMAL_WOOD >= 3 && currentFarms <= maxFarms)
        {
            GameObject f = Instantiate(farm, gameObject.transform.position, Quaternion.identity);
            f.gameObject.GetComponent<StructureScript>().addCreator(this.gameObject);
            currentFarms++;
            removeNW(3);
        }

        if(ROCK >= 5 && currentMines <= maxMines)
        {
            GameObject r = Instantiate(mine, gameObject.transform.position, Quaternion.identity);
            r.gameObject.GetComponent<StructureScript>().addCreator(this.gameObject);
            currentMines++;
            removeRock(5);
        }
    }

    //WOOD
    public void addnormalWood()
    {
        NORMAL_WOOD++;
    }
    public void removeNW(int amt)
    {
        NORMAL_WOOD -= amt;
        if(NORMAL_WOOD < 0)
        {
            NORMAL_WOOD = 0;
        }
    }

    //FARM
    public void removeFarm()
    {
        currentFarms--;
    }

    //ROCK
    public void addRock()
    {
        ROCK++;
    }
    public void addIron()
    {
        METAL++;
    }
    public void removeRock(int amt)
    {
        ROCK -= amt;
        if (ROCK < 0)
        {
            ROCK = 0;
        }
    }

    //MINE
    public void removeMine()
    {
        currentMines--;
    }

}
