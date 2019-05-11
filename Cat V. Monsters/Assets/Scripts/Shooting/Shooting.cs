using UnityEngine.UI;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Rigidbody2D bulletPrefab;

    //public GameObject muzzleFlashPrefab;

    Transform bulletSpawn;

    public float speed;

    PlayerMovement control;

    public float fireRate = 0.5f;

    float nextFire = 0.0f;

    public int clip = 30;

    public int reserve = int.MaxValue;

    public Text ammo;

    /*public AudioSource gunAudioSource;
    public AudioClip gunAudioClip;
    public AudioClip reloadClip;*/

    private Animator anim;

    public static bool shot;

    void Start()
    {
        anim = GetComponent<Animator>();
        control = GetComponent<PlayerMovement>();
        bulletSpawn = gameObject.transform.Find("GunPoint");
    }

    void Update()
    {
        //ammo.text = clip.ToString() + " / " + reserve.ToString();

        if (Input.GetButton("Fire1") && Time.time > nextFire && clip > 0)
        {
            nextFire = Time.time + fireRate;
            clip -= 1;
            //gunAudioSource.clip = gunAudioClip;
            //gunAudioSource.Play();
            shoot();
        }
        else
        {
            anim.SetBool("Shoot", false);
        }

        if (Input.GetKeyUp(KeyCode.R) && clip != 30 && reserve != 0)
        {
            var totalAmmo = clip + reserve;
            if (totalAmmo <= 30)
            {
                clip = totalAmmo;
                reserve = 0;
            } else
            {
                var shotsFired = 30 - clip;
                clip = 30;
                reserve -= shotsFired;
            }

            //gunAudioSource.clip = reloadClip;
            //gunAudioSource.Play();
        }
    }

    void shoot()
    {
        if (control.facingRight)
        {
            Rigidbody2D bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(new Vector3(0, 0, -90f))) as Rigidbody2D;
            //Instantiate(muzzleFlashPrefab, bulletSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bulletInstance.velocity = new Vector2(speed, 0);
        }
        else
        {
            Rigidbody2D bulletInstance = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(new Vector3(0, 0, 90f))) as Rigidbody2D;
            //Instantiate(muzzleFlashPrefab, bulletSpawn.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            bulletInstance.velocity = new Vector2(-speed, 0);
        }

        anim.SetBool("Shoot", true);
    }
}
