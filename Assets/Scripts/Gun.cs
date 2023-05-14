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

    private void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
        audio.volume = 0.1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        onShoot.Invoke();

        var cam = Camera.main;
        var ray = new Ray(cam.transform.position, cam.transform.forward);

        mFlashPrefab.SetActive(true);
        Invoke("DisableFlash", 0.05f);

        audio.pitch = Random.Range(0.85f, 1.15f);
        audio.PlayOneShot(shotSound);

        if (Physics.Raycast(ray, out var hit, maxDistance))
        {
            var health = hit.transform.GetComponent<Health>();
            if (health)
            {
                health.Damage();
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
