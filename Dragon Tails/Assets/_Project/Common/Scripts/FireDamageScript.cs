using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamageScript : MonoBehaviour
{

    //private ParticleSystem PSystem;
    //private  List<ParticleCollisionEvent> CollisionEvents;

    public Collider player;

    public void Start()
    {
    }

    public void OnParticleCollision(GameObject other)
    {
        //int collCount = PSystem.GetCollisionEvents(other, CollisionEvents);

        /*if (0 < collCount)
        {
            

            int eventCount = PSystem.GetCollisionEvents(other, CollisionEvents);

            for (int i = 0; i < eventCount; i++)
            {
                //TODO: Do your collision stuff here. 
                // You can access the CollisionEvent[i] to obtaion point of intersection, normals that kind of thing
                // You can simply use "other" GameObject to access it's rigidbody to apply force, or check if it implements a class that takes damage or whatever
            }
        }*/
        if (other.GetComponent<DamageableObjectScript>())
            other.SendMessage("TakeFireDamage", 50);
    }
}
