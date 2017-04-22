
using UnityEngine;
using System.Collections;

public class CallStageScript : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel("GameScene");
        }
    }
}