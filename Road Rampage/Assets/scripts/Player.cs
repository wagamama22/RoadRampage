using TMPro;
using UnityEngine;
using UnityEngine.UI; // For Text
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    Vector2 movement;
    Camera gameCamera;
    float moveX;
    [SerializeField] int health = 100;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float explosionTime = 1f;
    [SerializeField] AudioClip destroyObstacleSound;
    [SerializeField][Range(0, 1)] float destroyObstacleSoundVolume = 0.8f;
    [SerializeField] AudioClip destroyPlayerSound;
    [SerializeField][Range(0, 1)] float destroyPlayerSoundVolume = 0.8f;
    [SerializeField] AudioClip destroyPointGiverSound;
    [SerializeField][Range(0, 1)] float destroyPointGiverSoundVolume = 0.8f;
    [SerializeField] AudioClip playerMoveSound;
    [SerializeField][Range(0, 1)] float playerMoveSoundVolume = 0.8f;
    [SerializeField] AudioClip playerDeathSound;
    [SerializeField][Range(0, 1)] float playerDeathSoundVolume = 0.8f;
    // UI for health display
    [SerializeField] TextMeshProUGUI healthText;
    private LevelManager levelManager;




    void Awake()
    {
        // Load persistent health
        health = GameState.PlayerHealth > 0 ? GameState.PlayerHealth : 100; // Default 100 if first time
        levelManager = FindFirstObjectByType<LevelManager>();

    }

    void Start()
    {
        gameCamera = Camera.main;
        UpdateHealthDisplay(); // Initial display
    }

    void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health;
        }
    }

    //Method that reads damage from damageDealer carrier gameObject by type obstacle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            string obstacleName = collision.gameObject.name;
            Debug.Log("Collided with: " + obstacleName);

            int damage = damageDealer.GetDamage(obstacleName); // new method
            health -= damage;
            GameState.PlayerHealth = health; // Persist
            GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(explosion, explosionTime);
            //play sound obstacle destroy sound on collision with player
            AudioSource.PlayClipAtPoint(destroyPlayerSound, Camera.main.transform.position, destroyPlayerSoundVolume);
            Debug.Log("Damage received: " + damage);

            if (health <= 0)
            {
                GetComponent<DestroyAny>().GetDamageAttached();//destroy the gameObject
                damageDealer.Hit();//destroy the damageDealer gameObject when player is dead
                levelManager.LoadSceneByName("GameOver");
            }
            else
            {
                damageDealer.OnHit();//destroys the damagedealer component when it collides with the player and the player is still alife
            }
            UpdateHealthDisplay();
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    void MovePlayer() 
    {
        //getting user input
        moveX = Input.GetAxis("Horizontal");
        //player movement iin x-axis without clamping
        movement = Vector2.zero;//nitialize to zero:
        movement.x = moveX;
        movement *= playerSpeed * Time.deltaTime;
        transform.Translate(movement);
        //Play move sound only if moving
        if (moveX != 0  && playerMoveSound != null)
        {
            AudioSource.PlayClipAtPoint(playerMoveSound, Camera.main.transform.position, playerMoveSoundVolume);
        }

        //creating boundary for player movement along x axis
        Vector3 playerMoveBoundary = gameCamera.WorldToViewportPoint(transform.position);
        playerMoveBoundary.x = Mathf.Clamp(playerMoveBoundary.x, 0.04f, 0.97f);
        transform.position = gameCamera.ViewportToWorldPoint(playerMoveBoundary);

    }

    //method to add extra health points by pointGivers and read damage from the obstacle bullets
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for PointGiver
        PointGiver pointGiver = collision.gameObject.GetComponent<PointGiver>();
        if (pointGiver != null)
        {
            int life = pointGiver.GetHealth();
            health += life;
            //play sound
            AudioSource.PlayClipAtPoint(destroyPointGiverSound, Camera.main.transform.position, destroyPointGiverSoundVolume);
            Debug.Log("Health received: " + life);

            pointGiver.Hit(); // pickup consumed
            UpdateHealthDisplay();
            return; // exit early, no need to check damage
        }

        // Check for DamageDealer from obstacle bullet
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            string attackerName = collision.gameObject.name;
            int damage = damageDealer.GetDamage(attackerName);
            Debug.Log("Hit by: " + attackerName + " | Damage: " + damage);

            health -= damage;
            GameState.PlayerHealth = health; // Persist
            GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(explosion, explosionTime);
            //play sound
            AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);

            if (health <= 0)
            {
                
                GetComponent<DestroyAny>().GetDamageAttached();
                damageDealer.Hit();
                levelManager.LoadSceneByName("GameOver");
            }
            else
            {
                damageDealer.OnHit();
            }
            UpdateHealthDisplay();
        }
    }


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        UpdateHealthDisplay(); // Keep updated every frame
    }
    void OnDestroy()
    {
        // Save health when leaving scene (safety)
        GameState.PlayerHealth = health;
    }
}
