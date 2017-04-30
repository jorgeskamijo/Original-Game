using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletatscript : MonoBehaviour
{

    private GameObject player;
    private int speed = 10;

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
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
