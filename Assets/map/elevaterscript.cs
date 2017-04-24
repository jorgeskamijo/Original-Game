﻿using UnityEngine;
using System.Collections;

public class elevaterscript : MonoBehaviour
{

    Rigidbody2D rigidbody2D;
    private int speed = 7;
    private int degree = 0;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * Mathf.Sin(this.degree * Mathf.Deg2Rad));
        this.degree++;
    }
}