using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour
{
    public GameObject farmCreator;
    private float timer;
    public float health = 100f;
    private void Start()
    {
        timer = 40f;
    }
    void Update()
    {
        //checks for destruction
        if(health <= 0)
        {
            farmCreator.gameObject.GetComponent<Crafting>().removeFarm();
            Destroy(this.gameObject);
        }

        //gives creator of farm resources over time
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            farmCreator.gameObject.GetComponent<Crafting>().addnormalWood();
            timer = 40f;
        }
    }
    //sets the creator of farm
    public void addCreator(GameObject creator)
    {
        farmCreator = creator;
    }

}
