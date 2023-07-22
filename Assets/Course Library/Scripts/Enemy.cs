using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Get player direction
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        //Apply force in player direction
        enemyRb.AddForce(lookDirection * speed);


        //Destroy OoB
        if (transform.position.y < -12)
        {
            Destroy(gameObject);
            player.transform.localScale = player.transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
            player.GetComponent<PlayerController>().ChangeMaterial();
        }
    }
}
