using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public GameObject hitPrefab; //bullet hole
    public ParticleSystem hitSmoke;
    public GameObject mFlashPrefab; //muzzle flash
    public float maxDistance = 100;

    public AudioClip shotSound;
    AudioSource audio;

    public UnityEvent onShoot;

    public int maxAmmo = 30;
    public int ammo;

    public float recoilAngle = 1;
    public int shotsPerAmmo = 1;

    public int damage = 10;

    private void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
        audio.volume = 0.1f;
        ammo = maxAmmo;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (ammo <= 0) return;
        ammo--;
        onShoot.Invoke();

        mFlashPrefab.SetActive(true);
        Invoke("DisableFlash", 0.05f);

        audio.pitch = Random.Range(0.85f, 1.15f);
        audio.PlayOneShot(shotSound);

        for (int i =0; i< shotsPerAmmo; i++)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var cam = Camera.main;
        var dir = cam.transform.forward;

        var offsetX = Random.Range(-recoilAngle, recoilAngle);
        var offsetY = Random.Range(-recoilAngle, recoilAngle);
        dir = Quaternion.Euler(offsetX, offsetY, 0) * dir;

        var ray = new Ray(cam.transform.position, dir);

        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            var health = hit.transform.GetComponent<Health>();
            if (health)
            {
                health.Damage(damage);
            }

            if (!hit.transform.CompareTag("Enemy"))
            {
                var hitObj = Instantiate(hitPrefab, hit.point, Quaternion.Euler(0, 0, 0), hit.transform);
                hitObj.transform.forward = hit.normal;
                hitObj.transform.position += hit.normal * 0.01f;

                var smoke = Instantiate(hitSmoke, hit.point, Quaternion.Euler(0, 0, 0), hit.transform);
                smoke.transform.forward = hitSmoke.transform.forward;
            }
        }
    }

    void DisableFlash()
    {
        mFlashPrefab.SetActive(false);
    }
}
