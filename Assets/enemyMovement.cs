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
    public Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        playerDirection = 1;
        jumpMax = jumpCount;
        oldPos = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2(playerSpeed * playerDirection, 0);
        //This is where we add force for character jump.
        
        //Put in max speed
        if (rigidBody.velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            Vector3 norm = rigidBody.velocity.normalized;
            rigidBody.velocity = new Vector3(norm.x * maxSpeed, norm.y * maxSpeed, norm.z * maxSpeed);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
            Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Powered")
        {
            Destroy(this);
            return;
        }
        if (collision.gameObject.tag == "Back")
        {
            playerDirection *= -1;
        }
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
