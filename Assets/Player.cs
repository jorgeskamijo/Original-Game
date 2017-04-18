using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float speed = 4f; //歩くスピード
    public GameObject mainCamera;
    private Rigidbody2D rigidbody2D;
    private Animator anim;

    void Start()
    {
        //各コンポーネントをキャッシュしておく
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //左キー: -1、右キー: 1
        float x = Input.GetAxisRaw("Horizontal");
        //左か右を入力したら
        if (x != 0)
        {
            //入力方向へ移動
            rigidbody2D.velocity = new Vector2(x * speed, rigidbody2D.velocity.y);
            //localScale.xを-1にすると画像が反転する
            Vector2 temp = transform.localScale;
            temp.x = x;
            transform.localScale = temp;
            //Wait→Dash
            anim.SetBool("Dash", true);//左も右も入力していなかったら

            //画面中央から左に4移動した位置をユニティちゃんが超えたら
            if (transform.position.x > mainCamera.transform.position.x - 4)
            {
                //カメラの位置を取得
                Vector3 cameraPos = mainCamera.transform.position;
                //ユニティちゃんの位置から右に4移動した位置を画面中央にする
                cameraPos.x = transform.position.x + 4;
                mainCamera.transform.position = cameraPos;
            }
            //カメラ表示領域の左下をワールド座標に変換
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            //カメラ表示領域の右上をワールド座標に変換
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            //ユニティちゃんのポジションを取得
            Vector2 pos = transform.position;
            //ユニティちゃんのx座標の移動範囲をClampメソッドで制限
            pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
            transform.position = pos;

        }
        else
        {
            //横移動の速度を0にしてピタッと止まるようにする
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            //Dash→Wait
            anim.SetBool("Dash", false);
        }
    }
}