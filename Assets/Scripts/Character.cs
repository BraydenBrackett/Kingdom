using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for handleing the player and their abilties
 * 
 * @Author Brayden Brackett, September 9-26-2021
 * 
 * **/
public class Character : MonoBehaviour
{
    //Adjust the speed for the application.
    public float speed = 1.0f;

    //The target position.
    public Transform target = null;

    //The sphere collider checking for nearby objects
    public SphereCollider radius;

    //Nearby objects
    public List<GameObject> targets = new List<GameObject>();

    //Time without randomRotation
    public float randomMoveTime = 5f;

    //Enabling Character after objects have spawned in
    private float searchTimer = 1.5f;
    private bool flag = true;

    //Universal wait timer tools
    private float waitTimer;
    private bool checker = true;


    void Start()
    {
        radius.enabled = false;   
    }
    void Update()
    {
        //Setting up search radius
        if (flag)
        {
            searchTimer -= Time.deltaTime;
            if (searchTimer <= 0)
            {
                radius.enabled = true;
                flag = false;
            }
        }

        //Look for what's nearby
        findTargets();

        //If nothing is nearby
        if(target == null)
        {
            //Move our position forward
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            randomMoveTime -= Time.deltaTime;
            if(randomMoveTime <= 0)
            {
                //Rotate to random direction
                transform.Rotate(0f, Random.Range(-360, 360), 0f, Space.Self);
                randomMoveTime = 5f;
            }
        }

        //If something is nearby
        if (target != null)
        {
            /**
             * Movement to target
             * **/
            //Distance to target
            float distance = Vector3.Distance(this.transform.position, target.transform.position);

            //Move our position a step closer to the target.
            float step = speed * Time.deltaTime; 
            // calculate distance to move
            if(distance > 2) 
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
            //Rotate to look at another object
            transform.LookAt(target);

            //***********************************************************************************

            /*
             * Breaking rules for each object
             * **/
            if(distance <= 3)
            {
                //COMBAT
                if ((gameObject.tag.Equals("Blue") && target.gameObject.tag.Equals("Red")) || (gameObject.tag.Equals("Red") && target.gameObject.tag.Equals("Blue")))
                {
                    sW(1f);
                    if (waitTimer <= 0)
                    {
                        if(gameObject.GetComponent<Combat>().getStrength() > target.GetComponent<Combat>().getStrength())
                        {
                            GameObject temp = target.gameObject;
                            target = null;
                            Destroy(temp.gameObject);
                            gameObject.GetComponent<Combat>().addKill();
                            checker = true;
                        }
                        else
                        {
                            Destroy(gameObject);
                            checker = true;
                        }
                    }
                }

                //Destorying Buildings
                else if(target.gameObject.tag.Equals("Farm") || target.gameObject.tag.Equals("Mine"))
                {
                    //If it's of the other TEAM
                    if (target.GetComponent<StructureScript>().isBlue == true && gameObject.tag.Equals("Red") || target.GetComponent<StructureScript>().isBlue == false && gameObject.tag.Equals("Blue"))
                    {
                        target.GetComponent<StructureScript>().health -= gameObject.GetComponent<Combat>().getStrength() * Time.deltaTime;
                    }
                }

                //Trees
                else if (target.gameObject.tag.Equals("Tree"))
                {
                    //Wait for some time then destory the object and add resources
                    sW(2f);
                    if(waitTimer <= 0)
                    {
                        GameObject temp = target.gameObject;
                        target = null;
                        Destroy(temp.gameObject);
                        gameObject.GetComponent<Crafting>().addnormalWood();
                        checker = true;
                    }
                }
                //Rock
                else if (target.gameObject.tag.Equals("Rock"))
                {
                    sW(5f);
                    if (waitTimer <= 0)
                    {
                        //Wait for some time then destory the object and add resources
                        GameObject temp = target.gameObject;
                        target = null;
                        Destroy(temp.gameObject);
                        gameObject.GetComponent<Crafting>().addRock();
                        checker = true;
                    }
                }
            }


        }

        
    }

    /*
     * Deals with when something enters the searching radius
     * **/
    private void OnTriggerEnter(Collider other)
    {
        //If the object isn't of select types
        if (!other.gameObject.tag.Equals("Terrian") && !other.gameObject.tag.Equals("Water") && !other.gameObject.tag.Equals(gameObject.tag))
        {
            //If it isn't a TEAM based object
            if(!other.gameObject.tag.Equals("Farm") && !other.gameObject.tag.Equals("Mine"))
            {
                targets.Add(other.gameObject);
            }
            //If it is a team based object, make sure it's of the other team
            else if ((!other.gameObject.GetComponent<StructureScript>().isBlue == true && gameObject.tag.Equals("Blue")) || (!other.gameObject.GetComponent<StructureScript>().isBlue == false && gameObject.tag.Equals("Red")))
            {
                targets.Add(other.gameObject);
            }
        }
    }

    //When something exits the searching radius
    private void OnTriggerExit(Collider other)
    {
        targets.Remove(other.gameObject);
    }

    /*
     * Deals with searching for targets. If something enters set radius, and it isn't null, and is the closest object, return it 
     * **/
    Transform findTargets()
    {
        Vector3 shortestDistaceTonearby = new Vector3(1000, 1000, 1000);
        
        //Finds the closest object and returns it
        foreach(GameObject g in targets)
        {
            if(g != null)
            {
                Vector3 distanceTonearby = transform.position - g.transform.position;
                if (distanceTonearby.magnitude < shortestDistaceTonearby.magnitude)
                {
                    shortestDistaceTonearby = transform.position - g.transform.position;
                    target = g.transform;
                }
            }
        }
        return target;
    }

    /*
     * Set's waitTimer for desotrying an object to specific time. The dereases time.
     * **/
    void sW(float waitFor)
    {
        if (checker)
        {
            waitTimer = waitFor;
            checker = false;
        }
        waitTimer -= Time.deltaTime;
    }

}
