using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damageOBS0 = 2;
    [SerializeField] int damageOBS1 = 4;
    [SerializeField] int damageOBS3 = 6;
    [SerializeField] int damageOBS4 = 8;
    [SerializeField] int damage = 1;
    DestroyAny destroyAny;



    void Awake()
    {
        // Initialize destroyAny component
        destroyAny = GetComponent<DestroyAny>();


    }

    //returns the damage value of this damage dealer
    public int GetDamage(string name)
    {
        if (name.Contains("Obstacle0"))
        {
            Debug.Log("obs0 2 points gone");
            return damageOBS0;
        }
        else if (name.Contains("Obstacle1"))
        {
            Debug.Log("obs1 4 points gone");
            return damageOBS1;
        }
        else if (name.Contains("Obstacle3"))
        {
            Debug.Log("obs2 6 points gone");
            return damageOBS3;
        }
        else if (name.Contains("Obstacle4"))
        {
            Debug.Log("obs3 8 points gone");
            return damageOBS4;
        }

        Debug.Log("Default damage applied");
        return damage;
    }



    //destroy the gameobject this script is attached to
    public void Hit()
    {
        destroyAny.GetDamageAttached();
    }

    public void OnHit()
    {
        destroyAny.GetDamageAttached();
    }
}
