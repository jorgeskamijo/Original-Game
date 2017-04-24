using UnityEngine;
using System.Collections;

public class enemyscript1 : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    private float speed = 2f;

    public GameObject itemr;
    private int hp = 2;
    public int ap = 32;
    private LifeScript lifeScript;
    private Transform player;
    public GameObject explosion;

    void Start()
    {
        player = GameObject.FindWithTag("eddy").transform;
        //HPタグの付いているオブジェクトのLifeScriptを取得
        lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bullet")
        {
            this.hp--;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //UnityChanとぶつかった時
        if (col.gameObject.tag == "eddy")
        {
            //LifeScriptのLifeDownメソッドを実行
            lifeScript.LifeDown(ap);
        }
    }

    void Update()
    {
        Vector3 playerPos = player.position;    //プレイヤーの位置
        Vector3 direction = playerPos - transform.position; //方向
        direction = direction.normalized;   //単位化（距離要素を取り除く）
        transform.position = transform.position + (direction * speed * Time.deltaTime);
        if (direction.x >= 0)
        {
            Vector2 temp = transform.localScale;
            temp.x = -2;
            transform.localScale = temp; ;
        }
        else
        {
            Vector2 temp = transform.localScale;
            temp.x = 2;
            transform.localScale = temp; ;
        }

        if (hp == 0)
        {

            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(itemr, transform.position, transform.rotation);
        }
    }

}
