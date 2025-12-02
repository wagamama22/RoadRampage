using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 2f;
    Vector2 offset;
    Material backgroundMaterial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //access the background material
        backgroundMaterial = GetComponent<Renderer>().material;
        //assign value to offset by initialising it
        offset = Vector2.up * scrollingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //make the background to start scrolling
        Vector2 movement = offset * Time.deltaTime;
        backgroundMaterial.mainTextureOffset += movement;
    }
}
