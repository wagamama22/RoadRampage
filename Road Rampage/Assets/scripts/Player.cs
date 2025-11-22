using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 5f;
    Vector2 moveX;
    Camera gameCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameCamera = Camera.main;
    }

    void MovePlayer() 
    {
        //getting user input
        float inputX = Input.GetAxis("Horizontal");
        //player movement iin x-axis without clamping
        moveX = new Vector2(inputX, 0f) * playerSpeed * Time.deltaTime;
        transform.Translate(moveX);

        //creating boundary for player movement along x axis
        Vector3 playerMoveBoundary = gameCamera.WorldToViewportPoint(transform.position);
        playerMoveBoundary.x = Mathf.Clamp(playerMoveBoundary.x, 0.04f, 0.97f);
        transform.position = gameCamera.ViewportToWorldPoint(playerMoveBoundary);

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
}
