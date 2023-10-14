using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private enum State { idle, run, jump, fall, attack };
    private State state = State.idle;

    private Rigidbody2D rb;
    public Animator animator;
    public float speed;
    public float jumpForce;
    public Vector2 movementInput;
    public Collider2D Collider2D;
    public float Points = 0;
    public Text cointText;
    public GameObject gameOverPanel;
    public GameObject gameFinishedPanel;
    bool isJumping = false;
    bool isGrounded = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("state", (int)state);
        Movement();
        AnimationState();
        LevelComplete();
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1f, transform.localScale.y);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1f, transform.localScale.y);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Collider2D.IsTouchingLayers())
            {
                if (isGrounded ==  true)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    state = State.jump;

                }


            }
        }
    }


    public void AnimationState()
    {
        if (state == State.jump)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.fall;
            }
        }

        
        else if (state == State.fall)
        {
            if (Collider2D.IsTouchingLayers())
            {
                state = State.idle;
            }

        }

        else if (Mathf.Abs(rb.velocity.y) > .1f)
        {
            state = State.fall;
        }

        else if (Mathf.Abs(rb.velocity.x) > .2f)
        {
            state = State.run;
        }
        else if (Mathf.Abs(rb.velocity.x) == .0f)
        {
            state = State.idle;
        }
    }

    public void getpoint()
    {
        Points = Points + 1;
    }

    public void getpoint2()
    {
        Points = Points + 4;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Point")
        {

            getpoint();
            Debug.Log("You Have " + Points + " Points");
            Destroy(collision.gameObject);
            cointText.text = Points.ToString();


        }

        if (collision.tag == "Point2")
        {

            getpoint2();
            Debug.Log("You Have " + Points + " Points");
            Destroy(collision.gameObject);
            cointText.text = Points.ToString();


        }

        if (collision.tag == "GameOver")
        {

            gameOverPanel.SetActive(true);
            Debug.Log("GameOver");

        }


    }

    public void LevelComplete()
    {
        if (Points == 50)
        {
            gameFinishedPanel.SetActive(true);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("It works");
            isGrounded = true;
        }

    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("It works2");
            isGrounded = false;
        }

    }
}




