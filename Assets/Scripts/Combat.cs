using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for handeling player combat
 * 
 * @Author Brayden Brackett, September 9-26-2021
 * 
 * **/
public class Combat : MonoBehaviour
{
    public int strength; //Chracter attack power
    public int kills = 0; //Number of opponents defeated

    //Randomly generates strength of player
    private void Start()
    {
        strength = Random.Range(0, 10);
    }
    public int getStrength()
    {
        return strength;
    }
    public void addKill()
    {
        kills++;
    }

}
