using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject tank;
    public GameObject robo;
    private float timeOut = 4;
    private float timeElapsed;
    private GameObject Player;
    private int enemycount;
    // Use this for initialization
    void Start()
    {
        //playerのオブジェクトを取得
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
 

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
            timeElapsed = 0f;

            //表示させる範囲
            Vector2 rangex = new Vector2(-11, 25);
            Vector2 rangey = new Vector2(-2,30);
            //敵を表示する位置を生成
            Vector3 pos = new Vector3(
                          Random.Range(rangex.x, rangex.y),
                          Random.Range(rangey.x, rangey.y),
                          0
                      );
        //エネミーを出現させる予定の位置
        Vector3 enemyPos = pos;
        //プレイヤーの位置
        Vector3 playerPos = new Vector3(this.Player.transform.position.x, this.Player.transform.position.y, 0);
            //距離を測る
            float length = Vector3.Distance(enemyPos, playerPos);
            enemycount = GameObject.FindGameObjectsWithTag("enemy").Length;
            //距離が一定以上で敵の数が15以下なら
            if (length > 10 && enemycount < 25)
        {
            int enemy = Random.Range(1, 10);
            if (1 <= enemy && enemy <= 4)
            {
                //enemy1を生成
                GameObject obj = Instantiate(enemy1);
                obj.transform.position = pos;
            }
            else if (5 <= enemy && enemy <= 8)
            {
                //robo作成    
                GameObject obj = Instantiate(robo) as GameObject;
                obj.transform.position = pos;
            }
            else
            {
                //tank作成    
                GameObject obj = Instantiate(tank) as GameObject;
                obj.transform.position = pos;
            }
        }
        }
    }
}