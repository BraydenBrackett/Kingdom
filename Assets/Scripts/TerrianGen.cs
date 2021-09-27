using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for generating random terrian of level
 * 
 * @Author Brayden Brackett, September 9-26-2021
 * 
 * **/
public class TerrianGen : MonoBehaviour
{
    //Tree properties
    public int numberOfTrees = 50;
    public GameObject tree;
    public GameObject pineTree;
    public int numberOfPineTrees = 50;

    //Rock properties
    public GameObject rock;
    public int numberOfRocks = 200;

    //River properties
    public GameObject river;
    public int riverRotationMin, riverRotationMax;

    /*
     * Generates given objects at random locations on map
     * **/
    void Start()
    {
        //Normal trees
        for(int i = 0; i < numberOfTrees; i++){
            
            Instantiate(tree, GeneratedPosition(-400, 400), Quaternion.identity);
        }

        //Pine trees
        for (int i = 0; i < numberOfPineTrees; i++)
        {

            Instantiate(pineTree, GeneratedPosition(-400, 400), Quaternion.identity);
        }

        //Rocks
        for (int i = 0; i < numberOfRocks; i++)
        {

            Instantiate(rock, GeneratedPosition(-400, 400), Quaternion.identity);
        }

        //Randomly generates river
        rotateRiver(riverRotationMin, riverRotationMax);

    }

    /*
     * Creates random map postion
     * 
     * @return Vector3 of new position
     * **/
    Vector3 GeneratedPosition(int min, int max)
    {
        int x, z;
        x = Random.Range(min, max);
        z = Random.Range(min, max);
        return new Vector3(x, 0, z);
    }

    //Destroys trees if they collide with eachother
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Tree"))
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }

    //Randomly rotates river on map
    void rotateRiver(int min, int max)
    {
        river.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(min, max), 0));
    }
}
