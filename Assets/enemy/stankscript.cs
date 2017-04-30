using UnityEngine;
using System.Collections;

public class stankscript : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    private float speed = 1.9f;

    float intervalTime;
    public GameObject itemblue;
    public GameObject itemd;
    private int hp = 14;
    public int ap = 64;
    private LifeScript lifeScript;
    private Transform player;
    public GameObject explosion;
    public GameObject enemybullet;

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
        if (col.tag == "spbullet")
        {
            this.hp -= 5;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //playerとぶつかった時
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
            temp.x = -4;
            transform.localScale = temp; ;
        }
        else
        {
            Vector2 temp = transform.localScale;
            temp.x = 4;
            transform.localScale = temp; ;
        }

        intervalTime += Time.deltaTime;


        if (intervalTime >= 3.0f)
        {
            intervalTime = 0.0f;

            Instantiate(enemybullet, transform.position + new Vector3(0f, 0.3f, 0f), transform.rotation);
        }


        if (hp <= 0)
        {

            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(itemd, transform.position, transform.rotation);
            //四分の一の確率でアイテムを落とす
            if (Random.Range(0, 10) == 0)
            {
                Instantiate(itemblue, transform.position + new Vector3(0.5f, 0f, 0f), transform.rotation);
            }
        }

    }
}
