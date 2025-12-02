using UnityEngine;

public class DestroyAny : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    //destroy any gameObject the script is attached to when conditions are met
    public GameObject GetDamageAttached()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
        return null;
    }
}
