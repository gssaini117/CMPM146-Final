using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public AudioSource groan;
    public AudioSource scream;
    public NavMeshAgent agent;
    public Animator anim;
    public float health = 10f;
    private float groaning = 0f;
    private bool hasNotScreamed = true;

    // Start is called before the first frame update
    void Start()
    {
        groaning = Random.Range(6f, 12f);
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        groaning -= Time.deltaTime;
        if (groaning <= 0)
        {
            groaning = Random.Range(6f, 12f);
            groan.Play();
        }

        if (agent.speed > 3f)
        {
            anim.SetTrigger("Kill");
            if (hasNotScreamed)
            {
                scream.Play();
                hasNotScreamed = false;
            }
        }
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
