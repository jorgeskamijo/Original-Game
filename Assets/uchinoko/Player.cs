using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float intervalTime;
    public float speed = 4f; //歩くスピード
    public float jumpPower = 750; //ジャンプ力
    public GameObject bullet;
    private Rigidbody2D rigidbody2D;
    private Animator anim;
    private bool isGrounded; //着地判定
    private Renderer renderer;
    public LifeScript lifeScript;
    private bool gameOver = false; //ゲームオーバーしたら操作を無効にする
    private GameObject scoreText;
    private int score = 0;
    public LayerMask groundLayer; //Linecastで判定するLayer
    //左ボタン押下の判定
    private bool isLButtonDown = false;
    //右ボタン押下の判定
    private bool isRButtonDown = false;
    //Jボタン押下の判定
    private bool isJumpButtonDown = false;
    //Jボタン押下の判定
    private bool isShotButtonDown = false;

    void Start()
    {
        //各コンポーネントをキャッシュしておく
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        this.scoreText = GameObject.Find("ScoreText");
    }
   
    
    //足元に地面があるか判定
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Ground" && Physics2D.Linecast(
        transform.position + transform.up * 0,
        transform.position - transform.up * 1f,
        groundLayer)
)
            isGrounded = true;
        if (!gameOver)
        {
            //敵がplayerとぶつかった時
            if (col.gameObject.tag == "enemy")
        {
            anim.SetTrigger("damage"); 
            
			StartCoroutine ("Damage");
		}
        }
    }
	
	IEnumerator Damage ()
	{
		//レイヤーをPlayerDamageに変更
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		//while文を10回ループ
		int count = 20;
		while (count > 0){
			//透明にする
			renderer.material.color = new Color (1,1,1,0);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			//元に戻す
			renderer.material.color = new Color (1,1,1,1);
			//0.05秒待つ
			yield return new WaitForSeconds(0.05f);
			count--;
		}
		//レイヤーをPlayerに戻す
		gameObject.layer = LayerMask.NameToLayer("Player");
	}

        

    void Update()
    {
        if (lifeScript.rt.sizeDelta.x <= 0)
        {
            //ゲームオーバー判定をtrue
            gameOver　=　true;
        }
        if (!gameOver)
        {
            //スペースキーを押し、
            if (Input.GetKeyDown("space") || this.isJumpButtonDown)
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

        if (!gameOver)
        {
            intervalTime += Time.deltaTime;
            if (Input.GetKeyDown("left ctrl") || this.isShotButtonDown)
        　　{

                if (intervalTime >= 0.25f)
                {
                    intervalTime = 0.0f;

                    anim.SetTrigger("shot");
                    Instantiate(bullet, transform.position + new Vector3(0f, 0.2f, 0f), transform.rotation);
                }
       　　 }
        }


    }
    void OnTriggerEnter2D(Collider2D other)
    {

        //itemrを取得した場合
        if (other.gameObject.tag == "itemr")
        {
            // スコアを加算
            this.score += 100;

            //ScoreText獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

            //接触したオブジェクトを破棄
            Destroy(other.gameObject);
        }

        //itemgを取得した場合
        if (other.gameObject.tag == "itemg")
        {
            // スコアを加算
            this.score += 300;

            //ScoreText獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

            //接触したオブジェクトを破棄
            Destroy(other.gameObject);
        }

        //itemsを取得した場合
        if (other.gameObject.tag == "items")
        {

            // スコアを加算(追加)
            this.score += 200;

            //ScoreText獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

            //接触したオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }
  void FixedUpdate()
    {
        if (!gameOver)
        {
            //左キー: -1、右キー: 1
            int scale = 2; //オブジェクトのサイズ
            int x = 0;
            if (Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown)
                {
				x = -1 * scale;
			}
            else if (Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) {
				x = 1 * scale;
			}
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
            //横移動の速度を0にして止まるようにする
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            //Dash→Wait
            anim.SetBool("Dash", false);
        }
        }
        if (gameOver) {
            anim.SetBool("GameOver", true);
            gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
        }
    }
 
       //左ボタンを押し続けた場合の処理
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    //左ボタンを離した場合の処理
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理
    public void GetMyRightButtonDown()
    {
           this.isRButtonDown = true;
    }
    //右ボタンを離した場合の処理
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
    //右ボタンを押し続けた場合の処理
    public void GetMyJumpButtonDown()
    {
       this.isJumpButtonDown = true;
    }
    //右ボタンを離した場合の処理
    public void GetMyJumpButtonUp()
    {
        this.isJumpButtonDown = false;
    }
    //右ボタンを押し続けた場合の処理
    public void GetMyShotButtonDown()
    {
        this.isShotButtonDown = true;
    }
    //右ボタンを離した場合の処理
    public void GetMyShotButtonUp()
    {
        this.isShotButtonDown = false;
    }
}