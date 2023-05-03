using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject hitPrefab; //bullet hole
    public GameObject mFlashPrefab; //muzzle flash
    public float mFlashDuration = 0.5f;
    public float mFlashTime;
    public AudioSource shot;
    public float maxDistance = 100;

    private void Start()
    {
        mFlashPrefab.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = new Ray(transform.position, transform.forward);
            mFlashPrefab.SetActive(true);
            shot.Play();
            mFlashTime = Time.time + mFlashDuration;

            if (Physics.Raycast(ray, out var hit, maxDistance))
            {
                //hit
                print(hit.point);
                var hitObj = Instantiate(hitPrefab, hit.point, Quaternion.Euler(0, 0, 0));
                hitObj.transform.forward = hit.normal;
                hitObj.transform.position += hit.normal * 0.01f;
            }
        }

        if(mFlashPrefab.activeSelf && mFlashTime <= Time.time)
        {
            mFlashPrefab.SetActive(false);
        }
    }
}
