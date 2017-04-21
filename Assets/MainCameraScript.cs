using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour {

    //Unityちゃんのオブジェクト
    private GameObject Player;
    //Unityちゃんとカメラの距離
    private float difference;

    // Use this for initialization
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        this.Player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //Unityちゃんの位置に合わせてカメラの位置を移動
        this.transform.position = new Vector3(this.Player.transform.position.x, this.Player.transform.position.y, this.transform.position.z);
    }
}