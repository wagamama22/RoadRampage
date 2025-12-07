using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    Vector2 movement;
    Camera gameCamera;
    float moveX;
    [SerializeField] int health = 100;
    DestroyAny destroyAny;

    private void Awake()
    {
        destroyAny = GetComponent<DestroyAny>();
    }

    //Method that reads damage from damageDealer carrier gameObject
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            string obstacleName = collision.gameObject.name;
            Debug.Log("Collided with: " + obstacleName);

            int damage = damageDealer.GetDamage(obstacleName); // new method
            health -= damage;
            Debug.Log("Damage received: " + damage);

            if (health <= 0)
            {
                destroyAny.GetDamageAttached();//destroy the gameObject
                damageDealer.Hit();//destroy the damageDealer gameObject when player is dead
            }
            else
            {
                damageDealer.OnHit();//destroys the damagedealer component when it collides with the player and the player is still alife
            }
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameCamera = Camera.main;
    }

    void MovePlayer() 
    {
        //getting user input
        moveX = Input.GetAxis("Horizontal");
        //player movement iin x-axis without clamping
        movement = Vector2.zero;//nitialize to zero:
        movement.x = moveX;
        movement *= playerSpeed * Time.deltaTime;
        transform.Translate(movement);

        //creating boundary for player movement along x axis
        Vector3 playerMoveBoundary = gameCamera.WorldToViewportPoint(transform.position);
        playerMoveBoundary.x = Mathf.Clamp(playerMoveBoundary.x, 0.04f, 0.97f);
        transform.position = gameCamera.ViewportToWorldPoint(playerMoveBoundary);

    }

    //method to add extra health points by pointGivers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PointGiver pointGiver = collision.gameObject.GetComponent<PointGiver>();

        if (pointGiver != null)
        {
            int life = pointGiver.GetHealth(); // new method
            health += life;
            Debug.Log("health received: " + life);

            if (health <= 0)
            {
                GetComponent<DestroyAny>().GetDamageAttached();
                pointGiver.Hit();
            }
            else
            {
                pointGiver.Hit();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
}
