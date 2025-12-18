using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    [SerializeField] int health = 1;
    DestroyAny destroyAny;
    GameObject obstacleBullet;
    [SerializeField] GameObject enemyBulletPrefab;
    IEnumerator obstacleShots;
    [SerializeField] float minimumTimeBeforeShots = 0.01f;
    [SerializeField] float maximumTimeBeforeShots = 2f;
    [SerializeField] float countDownCounter;
    bool canShoot = false;
    [SerializeField] AudioClip ObstacleShootSound;
    [SerializeField][Range(0, 1)] float ObstacleShootSoundVolume = 0.8f;


    private void Awake()
    {
        destroyAny = GetComponent<DestroyAny>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        obstacleShots = SteadyEnemyShooting();
        countDownCounter = Random.Range(minimumTimeBeforeShots, maximumTimeBeforeShots);
    }
   

    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }

    IEnumerator SteadyEnemyShooting()
    {
        while (true)
        {
            float laserDisplacement = 0.899f; //distance of the spawn laser from the tip of the player
            var obstaclePosition = transform.position;
            obstaclePosition.y -= laserDisplacement;
            obstacleBullet = Instantiate(enemyBulletPrefab, obstaclePosition, Quaternion.identity);
            obstacleBullet.GetComponent<Rigidbody2D>().linearVelocityY = -3f;
            //Destroy(laser, 5f);//destroys laser after 5 seconds


            //applying delay that coroutine is known for
            yield return new WaitForSeconds(countDownCounter);
        }
    }

    void EnemyShootNow()
    {
        countDownCounter -= Time.deltaTime;
        if (countDownCounter <= 0)
        {
            StartCoroutine(obstacleShots);
            //reset the counter
            countDownCounter = Random.Range(minimumTimeBeforeShots, maximumTimeBeforeShots);
        }
        else
        {
            StopCoroutine(obstacleShots);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            // shooting logic here
            EnemyShootNow();
        }
       

    }
}
