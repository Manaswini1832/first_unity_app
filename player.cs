using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private new Rigidbody rigidbody;
    //private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        //if (!isGrounded)
        //{
        //    return;
        //}
        rigidbody.velocity = new Vector3(horizontalInput, rigidbody.velocity.y, 0);

        //Player capsule has its own collider so if it collides with itself, i.e. length of collisions array = 1 => don't jump further
        //Another way to do this exact thing is by the Layer method inside Unity's UI
        //if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1)

        //layer method where we expose playerMask(which is a layerMask) to the inspector from where we give it everything except that player layer that we gave to the player and to its children
        //PREFERRED
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyWasPressed)
        {
            rigidbody.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
 
    }

    //private void OnCollisionEnter(Collision collision)
    //{
     //   isGrounded = true;
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //}

    private void onTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Destroy(gameObject);
            Debug.Log("Coin touched");
        }
    }
}
