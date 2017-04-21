using UnityEngine;
using System.Collections;

public class enemyscript : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    public int speed = -3;

    public GameObject explosion;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bullet")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    void Update()
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
    }
}
