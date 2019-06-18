using UnityEngine;

public class MonHeal : MonoBehaviour
{
    static int maxHealth = 100;
    public int currentHealth = maxHealth;

    public int scoreValue = 10;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        maxHealth = 100;

        currentHealth = maxHealth;
    }

    public void monDam(int monDam)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= monDam;

        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            currentHealth = 0;

            //anim.SetTrigger("Dead");

            Destroy(this.gameObject);
        }
    }
}
