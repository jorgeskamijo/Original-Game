using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour
{

    public　RectTransform rt;
    public GameObject eddy; //ユニティちゃん
    public Text gameOverText; //ゲームオーバーの文字
    private bool gameOver = false; //ゲームオーバー判定

    void Start()
    {
        rt = GetComponent<RectTransform>();
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
            if (Input.GetMouseButtonDown(0))
            {
                //タイトルへ戻る
                Application.LoadLevel("Title");
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