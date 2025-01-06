using UnityEngine;

public class Target : MonoBehaviour
{
    public GameManager gameManager; 

    void OnMouseDown() 
    {
        gameManager.AddScore(2); 
        Destroy(gameObject); 
    }
}


public class GunController : MonoBehaviour
{
    public Camera playerCamera; 
    public float range = 100f; 
    public GameObject hitEffectPrefab; 

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

       
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);

           
            if (hit.collider.CompareTag("Shootable"))
            {
                Destroy(hit.collider.gameObject);
            }

           
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
