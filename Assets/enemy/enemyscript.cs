using UnityEngine;
using System.Collections;

public class enemyscript : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    public int speed = -3;
    private int degree = 0;
    public int attackPoint = 0;
    public LifeScript lifeScript;

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

    void OnCollisionEnter2D(Collision2D col)
    {
        //UnityChanとぶつかった時
        if (col.gameObject.tag == "eddy")
        {
            //LifeScriptのLifeDownメソッドを実行
            lifeScript.LifeDown(attackPoint);
        }
    }

    void Update()
    {
        rigidbody2D.velocity = new Vector2(speed　* Mathf.Sin(this.degree * Mathf.Deg2Rad), rigidbody2D.velocity.y);
        //現在の角度を小さくする
        this.degree ++;
    }

}
