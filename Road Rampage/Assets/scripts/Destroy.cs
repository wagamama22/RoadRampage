using UnityEngine;

public class Destroy : MonoBehaviour
{
    //destroy player laser after they left the viewport
    private void OnBecameInvisible()
    {
        //this script will destroy any gameobject attached to it
        Destroy(gameObject);
        Debug.LogWarning($"{gameObject} has been deleted");
    }
}