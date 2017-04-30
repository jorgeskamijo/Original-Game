using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{

    public　RectTransform rt;
    public GameObject eddy; //Player
    public Text gameOverText; //ゲームオーバーの文字
    private bool gameOver = false; //ゲームオーバー判定
    private Player player;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("eddy").GetComponent<Player>();
    }
    void Update()
    {
        //ライフが0以下になった時、
        if (rt.sizeDelta.x <= 0)
        {
            //ゲームオーバー判定をtrue
            GameOver();
        }
        //ゲームオーバー判定がtrueの時、
        if (gameOver)
        {
            //ゲームオーバーの文字を表示
            gameOverText.enabled = true;
            //画面をクリックすると
            if (Input.GetMouseButtonDown(0) && player.score >= 10000)
            {
                //end2へ進む
                Application.LoadLevel("end2");
            }
            if (Input.GetMouseButtonDown(0) && player.score < 10000)
            {
                //endへ進む
                Application.LoadLevel("end");
            }
        }
    }


    public void LifeDown(int ap)
    {
        //RectTransformのサイズを取得し、マイナスする
        rt.sizeDelta -= new Vector2(ap, 0);
    }

    public void GameOver()
    {
        gameOver = true;
        
    }
}