using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ebulletscript : MonoBehaviour {
    Rigidbody2D rigidbody2D;
    private LifeScript lifeScript;
    private int speed = 10;
    public int ap = 64;
    private Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("eddy").transform;
        //HPタグの付いているオブジェクトのLifeScriptを取得
        lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
        Vector3 playerPos = player.position;    //プレイヤーの位置
        Vector3 direction = playerPos - transform.position; //方向
        direction = direction.normalized;   //単位化（距離要素を取り除く）
        //rigidbody2Dコンポーネントを取得
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        //playerの向きに弾を飛ばす
        rigidbody2D.velocity = new Vector2(speed * direction.x, rigidbody2D.velocity.y);
        if (direction.x >= 0)
        {
            Vector2 temp = transform.localScale;
            temp.x = 2;
            transform.localScale = temp; ;
        }
        else
        {
            Vector2 temp = transform.localScale;
            temp.x = -2;
            transform.localScale = temp; ;
        }
        //5秒後に消滅
        Destroy(gameObject, 2.5f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //playerとぶつかった時
        if (col.gameObject.tag == "eddy")
        {
            //LifeScriptのLifeDownメソッドを実行
            lifeScript.LifeDown(ap);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
