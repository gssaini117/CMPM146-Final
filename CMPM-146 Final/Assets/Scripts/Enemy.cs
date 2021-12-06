using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    public float getHealth () {
        return health;
    }

    private void Die()
    {
        if (gameObject.GetComponent<BehaviorTree>()) {
            Destroy(gameObject.GetComponent<BehaviorTree>());
        }
    }
}
