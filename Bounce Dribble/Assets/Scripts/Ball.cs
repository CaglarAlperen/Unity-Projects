using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // config params
    [SerializeField] float horizontalSpeed = 4f;
    [SerializeField] float jumpProjSpeed = 8f;
    [Header("Audio")]
    [SerializeField] AudioClip[] bounceSFX;
    [SerializeField] AudioClip popSFX;

    // caches
    Rigidbody2D myRigidbody;
    Collider2D myCollider;

    // states
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
       myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Stay();
        Jump();
        LimitVel();
    }

    private void Stay()
    {
        if (!started)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }

    private void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!started)
            {
                FindObjectOfType<GameSession>().StartGame();
                started = true;
                myRigidbody.velocity = new Vector2(horizontalSpeed, 0f);
            }
            else
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpProjSpeed);
            }           
        }
            
    }

    private void LimitVel()
    {
        if ( Mathf.Abs(myRigidbody.velocity.y) > jumpProjSpeed )
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Mathf.Sign(myRigidbody.velocity.y) * jumpProjSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myRigidbody.IsTouchingLayers(LayerMask.GetMask("Side Wall")))
        {
            PlayBounceClip();
            FindObjectOfType<Score>().AddScore();
        }
    }

    public void AddHorizontalVel(float vel)
    {
        float vel_x = Mathf.Sign(myRigidbody.velocity.x) * vel + myRigidbody.velocity.x;
        if (vel_x > 5f) vel_x = 5f;
        myRigidbody.velocity = new Vector2(vel_x, myRigidbody.velocity.y);
    }

    public void ResetSpeed()
    {
        myRigidbody.velocity = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * horizontalSpeed, myRigidbody.velocity.y);
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(popSFX, Camera.main.transform.position);
        Destroy(gameObject);
        FindObjectOfType<GameSession>().FinishGame();
    }

    private void PlayBounceClip()
    {
        AudioSource.PlayClipAtPoint(bounceSFX[UnityEngine.Random.Range(0,bounceSFX.Length)], Camera.main.transform.position);
    }
}
