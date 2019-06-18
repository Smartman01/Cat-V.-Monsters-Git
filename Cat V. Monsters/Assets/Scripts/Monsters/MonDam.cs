using UnityEngine;

public class MonDam : MonoBehaviour
{
    public int damage;

    void OnCollisionEnter2D(Collision2D coll)
    {
        var hit = coll.gameObject;
        var hitplayer = hit.GetComponent<PlayerMovement>();
        if (hitplayer != null)
        {
            var health = hit.GetComponent<Health>();
            Debug.Log("ZombieHit");
            health.Damage(damage);
        }
    }
}
