using UnityEngine;

public class DestroyAny : MonoBehaviour
{
    //destroy any gameObject the script is attached to when conditions are met
    public void GetDamageAttached()
    {
        Destroy(gameObject);
    }
}
