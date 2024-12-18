using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinnMove : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] int jumpCount;
    [SerializeField] float health;
    [SerializeField] bool powered;
    int jumpMax;
    private int playerDirection;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerDirection = 0;
        jumpMax = jumpCount;
        powered = false;
    }

    // Update is called once per frame
    void Update()
    {
        //This is where we add force for character movement.
        playerDirection = 0;
        if (Input.GetKey(KeyCode.D))
        {
            playerDirection += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerDirection += -1;
        }
        rigidBody.AddForce(Vector3.right * playerSpeed * playerDirection);
        //Put in max speed
        if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
        {
            rigidBody.velocity = new Vector2((rigidBody.velocity.x / Mathf.Abs(rigidBody.velocity.x)) * maxSpeed, rigidBody.velocity.y);
        }
        //This is where we add force for character jump.
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount>0)
        {
            jumpCount--;
            rigidBody.AddForce(Vector3.up * jumpForce);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = jumpMax;
        }
        if (collision.gameObject.tag == "Power_Up")
        {
            bool powered = true;
            gameObject.tag = "Powered";
            Destroy(collision.gameObject);
        }
    }

}