using UnityEngine;
using System.Collections;

public class Bulletscript : MonoBehaviour
{

    private GameObject player;
    private int speed = 5;

    void Start()
    {
        //playerオブジェクトを取得
        player = GameObject.FindWithTag("eddy");
        //rigidbody2Dコンポーネントを取得
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        //playerの向いている向きに弾を飛ばす
        rigidbody2D.velocity = new Vector2(speed * player.transform.localScale.x, rigidbody2D.velocity.y);
        //画像の向きをplayerに合わせる
        Vector2 temp = transform.localScale;
        temp.x = player.transform.localScale.x;
        transform.localScale = temp;
        //5秒後に消滅
        Destroy(gameObject, 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "enemy"||col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}