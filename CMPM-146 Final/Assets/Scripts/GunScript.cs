using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    public AudioSource shotSound;
    public float cooldown = 1f;
    private float onCD = 0f;

    public Camera fpsCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onCD > 0f)
            onCD -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && onCD <= 0f)
        {
            Shoot();
            onCD = cooldown;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        shotSound.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "AI")
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                }
            }
        }
    }
}
