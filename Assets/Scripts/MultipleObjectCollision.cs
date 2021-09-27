using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Gives objets the ability to check for collsions with multiple objets of a specific type. 
 * 
 * @Author Brayden Brackett, September 9-26-2021
 * 
 * **/
public class MultipleObjectCollision : MonoBehaviour
{
    public string[] CollisionTags; //Array of tags that are checked when a collision is detected

    /*
     * Built in unity method for checking collisions. Checks to see if the object colliding is in the list and if it's
     * not a river. If true, it destorys that colliding obejct.
     * **/
    private void OnCollisionStay(Collision collision)
    {
        for(int i = 0; i < CollisionTags.Length; i++)
        {
            if (collision.gameObject.tag.Equals(CollisionTags[i]) && !collision.gameObject.tag.Equals("River"))
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
