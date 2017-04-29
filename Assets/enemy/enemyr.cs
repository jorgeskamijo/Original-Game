using UnityEngine;
using System.Collections;

public class enemyr : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    private float speed = 3f;

    public GameObject items;
    private int hp = 5;
    public int attackPoint = 16;
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
        //playerとぶつかった時
        if (col.gameObject.tag == "eddy")
        {
            //LifeScriptのLifeDownメソッドを実行
            lifeScript.LifeDown(attackPoint);
        }
    }

    void Update()
    {

        //敵を表示する位置を生成
        Vector3 target = new Vector3(
                      Random.Range(-100, 100),
                      transform.position.y,
                      0
                  );
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step); ;


        if (hp == 0)
        {

            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(items, transform.position, transform.rotation);
        }
    }

}