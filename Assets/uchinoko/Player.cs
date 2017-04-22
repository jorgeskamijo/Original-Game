using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    public float speed = 4f; //歩くスピード
    public float jumpPower = 700; //ジャンプ力
    public GameObject bullet;
    private Rigidbody2D rigidbody2D;
    private Animator anim;
    private bool isGrounded; //着地判定

    void Start()
    {
        //各コンポーネントをキャッシュしておく
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    //足元に地面があるか判定
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            isGrounded = true;
    }

    void Update()
    {
        //スペースキーを押し、
        if (Input.GetKeyDown("space"))
        {
            //着地していた時、
            if (isGrounded)
            {
                //Dashアニメーションを止めて、Jumpアニメーションを実行
                anim.SetBool("Dash", false);
                anim.SetTrigger("Jump");
                //着地判定をfalse
                isGrounded = false;
                //AddForceにて上方向へ力を加える
                rigidbody2D.AddForce(Vector2.up * jumpPower);
            }
        }
        //上下への移動速度を取得
        float velY = rigidbody2D.velocity.y;
        //移動速度が0.1より大きければ上昇
        bool isJumping = velY > 0.1f ? true : false;
        //移動速度が-0.1より小さければ下降
        bool isFalling = velY < -0.2f ? true : false;
        //結果をアニメータービューの変数へ反映する
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);
        if (Input.GetKeyDown("left ctrl"))
        {
            anim.SetTrigger("shot");
            Instantiate(bullet, transform.position + new Vector3(0f, 0.2f, 0f), transform.rotation);
        }

    }

    void FixedUpdate()
    {
        //左キー: -1、右キー: 1
        float scale = 2.0f; //オブジェクトのサイズ
        float x = Input.GetAxisRaw("Horizontal")　*　scale;
        //左か右を入力したら
        if (x != 0)
        {
            //入力方向へ移動
            rigidbody2D.velocity = new Vector2(x　/ scale * speed, rigidbody2D.velocity.y);
            //localScale.xを-1にすると画像が反転する
            Vector2 temp = transform.localScale;
            temp.x = x;
            transform.localScale = temp;
            //Wait→Dash
            anim.SetBool("Dash", true);//左も右も入力していなかったら

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