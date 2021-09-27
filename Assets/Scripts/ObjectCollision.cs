using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for handleing single object collision
 * 
 * @Author Brayden Brackett, September 9-26-2021
 * 
 * **/
public class ObjectCollision : MonoBehaviour
{
    public string CollisionTag; //Tag of object your checking for collision
    private void OnCollisionStay(Collision collision)
    {
        //Checks if the two objects are colliding, if they are, destory them
        if (collision.gameObject.tag.Equals(CollisionTag))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}

