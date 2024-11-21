using UnityEngine;
using UnityEngine.AI;

public class HealthComponent : MonoBehaviour
{
    public int maxHealth;
    private int health;



    void Start()
    {
        health = maxHealth;
    }

    public int Health
    {
        get { return health; }
    }

    public void Subtract(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
