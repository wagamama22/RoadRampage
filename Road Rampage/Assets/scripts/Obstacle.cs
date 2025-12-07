using System.Runtime.CompilerServices;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int health = 1;
    DestroyAny destroyAny;

    private void Awake()
    {
        destroyAny = GetComponent<DestroyAny>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer == null) return;

        string attackerName = collision.gameObject.name;
        int damage = damageDealer.GetDamage(attackerName);
        Debug.Log("Enemy hit by: " + attackerName + " | Damage: " + damage);

        health -= damage;

        if (health <= 0)
        {
            destroyAny.GetDamageAttached();
            damageDealer.Hit();
        }
        else
        {
            damageDealer.OnHit();
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
