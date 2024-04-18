using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject decalPrefab;
    public AudioSource fireSound;
    public float fireRate = 2.0f;
    public int counter = 0;
    public int damage = 10;

    private GameObject[] bulletHoles = new GameObject[10];

    // Timer to track time since the last shot
    private float timeSinceLastShot = 0.0f;

    // Other variables...
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        // Update the timer
        timeSinceLastShot += Time.deltaTime;


        // Check for mouse input and whether enough time has passed since the last shot
        if (Input.GetMouseButton(0) && timeSinceLastShot >= 1.0f / fireRate)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)), out hit))
            {
                // Check if the hit collider is not a trigger



                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponentInParent<EnemyAI>().Hit(damage);
                }
                else
                {
                    if (counter >= 10)
                    {
                        Destroy(bulletHoles[counter % 10]);
                    }
                    bulletHoles[counter % 10] = GameObject.Instantiate(decalPrefab, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, -hit.normal));
                    counter++;
                }
            }

            fireSound.Play();

            // Reset the timer
            timeSinceLastShot = 0.0f;
        }
    }

}
