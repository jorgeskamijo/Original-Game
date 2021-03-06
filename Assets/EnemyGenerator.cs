﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject tank;
    public GameObject robo;
    public GameObject enemy2;
    public GameObject stank;
    public GameObject robo2;
    private float timeOut = 3.5f;
    private float timeElapsed;
    private GameObject Player;
    private int enemycount;
    private Player player;
    // Use this for initialization
    void Start()
    {
        //playerのオブジェクトを取得
        this.Player = GameObject.Find("Player");
        player = GameObject.FindGameObjectWithTag("eddy").GetComponent<Player>();
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
            Vector3 pos = new Vector3(Random.Range(rangex.x, rangex.y),Random.Range(rangey.x, rangey.y),0);
            //enemyを出現させる予定の位置
        Vector3 enemyPos = pos;
            //playerの位置
        Vector3 playerPos = new Vector3(this.Player.transform.position.x, this.Player.transform.position.y, 0);
            //距離を測る
            float length = Vector3.Distance(enemyPos, playerPos);
            enemycount = GameObject.FindGameObjectsWithTag("enemy").Length;
            //距離が一定以上で敵の数が15以下なら
            if (length > 10 && enemycount <= 30)

            {
            int enemy = Random.Range(1, 15);
            if (1 <= enemy && enemy <= 6)

                {
                //enemy1を生成
                GameObject obj = Instantiate(enemy1);
                obj.transform.position = pos;
                }
            else if (7 <= enemy && enemy <= 12)

                {
                //robo作成    
                GameObject obj = Instantiate(robo) as GameObject;
                obj.transform.position = pos;
                }
            else if (12 <= enemy && enemy <= 15)
                {
                //tank作成    
                GameObject obj = Instantiate(tank) as GameObject;
                obj.transform.position = pos;
                }
                if (enemy == 12 && player.score >= 5000)
                {
                    GameObject obj = Instantiate(stank) as GameObject;
                    obj.transform.position = pos;
                }
                if (enemy == 7 && player.score >= 5000)
                {
                    GameObject obj = Instantiate(robo2) as GameObject;
                    obj.transform.position = pos;
                }
                if (enemy == 1 && player.score >= 5000)
                {
                    GameObject obj = Instantiate(enemy2) as GameObject;
                    obj.transform.position = pos;
                }
            }
        }
    }
}