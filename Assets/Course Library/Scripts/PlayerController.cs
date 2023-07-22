using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float rotationSpeed = 10;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup = false;
    public float powerUpStrength;
    public GameObject powerUpIndicator;
    //public GameObject direction;

    //for changing materials
    public Texture2D[] materials;
    new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        //Initialize material updates
        renderer = GetComponent<Renderer>();
        renderer.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        //Move player
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(focalPoint.transform.right * speed * horizontalInput);
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);

        //Rotate player
        //float horizontalInput = Input.GetAxis("Horizontal");
        //focalPoint.transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        //Keep powerup indicator
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.56f, 0);
        powerUpIndicator.transform.localScale = transform.localScale * 2;

        //On fall, reset player to center and remove linear and angular velocity
        if (transform.position.y < -12)
        {
            transform.position = new Vector3(0, 0, 0);
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
        }

        //Show player direction
        //direction.transform.position = transform.position + new Vector3(0, -0.56f, 0.8f);
        //direction.transform.eulerAngles = new Vector3(90, focalPoint.transform.eulerAngles.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Collect power up, show ring, start timer
        if (other.gameObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //If powered up, push away enemies on collision
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerUpIndicator.gameObject.SetActive(false);
    }

    public void ChangeMaterial()
    {
        int x = Random.Range(0, materials.Length);

        renderer.material.mainTexture = materials[x];
    }
}
