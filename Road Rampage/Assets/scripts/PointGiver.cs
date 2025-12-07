using UnityEngine;

public class PointGiver : MonoBehaviour
{
    [SerializeField] int health = 5;
    DestroyAny destroyAny;

    void Awake()
    {
        // Initialize destroyAny component
        destroyAny = GetComponent<DestroyAny>();
    }

    public int GetHealth()
    {
        return health;
    }
    public void Hit()
    {
        destroyAny.GetDamageAttached();
    }

    public void OnHit()
    {
        destroyAny.GetDamageAttached();
    }
}

