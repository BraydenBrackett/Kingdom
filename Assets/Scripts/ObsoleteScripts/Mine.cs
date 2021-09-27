using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject mineCreator;
    private float timer;
    public float health = 300f;
    private void Start()
    {
        timer = 40f;
    }
    void Update()
    {
        //checks for destruction
        if (health <= 0)
        {
            mineCreator.gameObject.GetComponent<Crafting>().removeMine();
            Destroy(this.gameObject);
        }

        //gives creator of farm resources over time
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            mineCreator.gameObject.GetComponent<Crafting>().addIron();
            mineCreator.gameObject.GetComponent<Crafting>().addRock();
            timer = 40f;
        }
    }
    //sets the creator of farm
    public void addCreator(GameObject creator)
    {
        mineCreator = creator;
    }
}
