using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float scrollingSpeed = 2f;
    Vector2 offset;
    Material backgroundMaterial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
        

        offset = new Vector2(0f, scrollingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        backgroundMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
