using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRb;
    public Transform cam;


    [SerializeField] private float jumpForce;

    [SerializeField] private float speed;

    public bool isPoweredUp;
    public float powerBounceStrength;
    public float powerupTime = 7f;

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.65f);
    }



    private void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {



        //moveX and moveZ also works

        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if(movement.magnitude > 0.1F) 
        {
            float targetAngle = Mathf.Atan2(movement.x,movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerRb.AddForce(movement * speed * Time.deltaTime);
        }


    
    }

    private void Jump()
    {
        if (isGrounded() == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup")) 
        {
            isPoweredUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDownRoutine());
        }
    }

    IEnumerator PowerupCountDownRoutine()
    {
        yield return new WaitForSeconds(powerupTime);
        isPoweredUp = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && isPoweredUp == true)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 bounceDir = (collision.gameObject.transform.position - transform.position);
            enemyRB.AddForce(bounceDir * powerBounceStrength, ForceMode.Impulse);
        }
    }



}
