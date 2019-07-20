﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]private float _speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestoryLaser();

    }

    private void DestoryLaser()
    {
        // move up at 10 speed
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // if position on the y of my laser is greater than or equal to 6
        // destory the laser

        if (transform.position.y >= 5.43f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
