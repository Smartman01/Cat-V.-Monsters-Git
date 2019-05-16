using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Rigidbody2D rb2d;

    Collider2D coll2D;

    public int speed;

    public float lifeTime;

    public int damage;

    // Use this for initialization
    void Start()
    {
        //allParticles = GetComponentsInChildren<ParticleSystem>().ToList();
        rb2d = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<Collider2D>();
        StartCoroutine("SelfDestruct");
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy();
    }

    void Destroy()
    {
        coll2D.enabled = false;
        rb2d.velocity = Vector2.zero;
        rb2d.Sleep();
        foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
        {
            m.enabled = false;
        }
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        var hit = coll.gameObject;
		var hitEnemy = hit.tag == "Enemy";
        if (hitEnemy)
        {
            //var Zomhealth = hit.GetComponent<ZombieHealth>();
            //Debug.Log("hit");
            //Zomhealth.ZomDamage(damage);
            Destroy(gameObject);
        }
        else if (hit.tag == "Ground")
        {
            //Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}
