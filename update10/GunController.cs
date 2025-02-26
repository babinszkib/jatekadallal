using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    public Camera playerCamera;
    public float range = 100f;
    public int maxAmmo = 6; 
    private int currentAmmo;

    [Header("Effects")]
    public GameObject hitEffectPrefab;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;

    [Header("UI Elements")]
    public Text ammoText;

    private void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Nincs lőszer! Nyomj 'R'-t az újratöltéshez.");
            return;
        }

        currentAmmo--;
        UpdateAmmoUI();

        if (shootSound)
            audioSource.PlayOneShot(shootSound);

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Shootable"))
            {
                Target target = hit.collider.GetComponent<Target>();
                if (target != null)
                {
                    target.HitTarget();
                }
            }

            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
            }
        }
    }

    void Reload()
    {
        if (currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            if (reloadSound)
                audioSource.PlayOneShot(reloadSound);
            UpdateAmmoUI();
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText)
            ammoText.text = $"Ammo: {currentAmmo}/{maxAmmo}";
    }
}
