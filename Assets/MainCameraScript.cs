using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {

    //playerのオブジェクト
    private GameObject Player;


    // Use this for initialization
    void Start()
    {
        //playerのオブジェクトを取得
        this.Player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //playerの位置に合わせてカメラの位置を移動
        this.transform.position = new Vector3(this.Player.transform.position.x, this.Player.transform.position.y, this.transform.position.z);
    }
}