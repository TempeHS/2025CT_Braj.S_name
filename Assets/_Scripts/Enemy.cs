using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movespeed = 2f;

    private Rigidbody2D rb; 

    [SerializeField] private Transform checkpoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if(Vector2.Distance(checkpoint.transform.position,transform.position) <= 0.1f)
        {
            Debug.Log("Checkpoint Reached");
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (checkpoint.position - transform.position).normalized;
        transform.right = checkpoint.position - transform.position;
        rb.velocity = direction * movespeed;
    }
}
