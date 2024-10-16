using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] int jumpCount;
    [SerializeField] float health;
    int jumpMax;
    private int playerDirection;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerDirection = 0;
        jumpMax = jumpCount;
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
        //This is where we add force for character jump.
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumpCount--;
            rigidBody.AddForce(Vector3.up * jumpForce);
        }
        //Put in max speed
        if (rigidBody.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            Vector3 norm = rigidBody.velocity.normalized;
            rigidBody.velocity = new Vector3(norm.x * maxSpeed, norm.y * maxSpeed, norm.z * maxSpeed);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            if (health >= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = jumpMax;
        }
    }
}
