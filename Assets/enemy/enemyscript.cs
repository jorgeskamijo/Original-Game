using UnityEngine;
using System.Collections;

public class enemyscript : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
        private float speed = 1.5f;

    float intervalTime;
    public GameObject itemg;
    private int hp = 10;
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
            this.hp　--;
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
        if (Input.GetKeyDown("left ctrl") )
        {

            if (intervalTime >= 5.5f)
            {
                intervalTime = 0.0f;

                Instantiate(enemybullet, transform.position + new Vector3(0f, 0.2f, 0f), transform.rotation);
            }
        }

        if (hp == 0)
        {
                        
                Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(itemg, transform.position, transform.rotation);
        }
    }

}
