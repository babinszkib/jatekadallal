using UnityEngine;

public class Target : MonoBehaviour
{
    public bool isTargetPractice;
    public float health = 10f;
    public float defaultHealth;

    void Start()
    {
        defaultHealth = health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isTargetPractice)
        {
            health = defaultHealth;
            gameObject.transform.position = new Vector3(Random.Range(2, -30), 5, Random.Range(13, 65));
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("Target Broken");
    }
}