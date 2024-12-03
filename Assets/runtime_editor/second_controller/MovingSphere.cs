using UnityEngine;
using UnityEngine.InputSystem;

public class MovingSphere : MonoBehaviour
{
    bool desiredJump;
    bool onGround;

    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;

    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f, maxAirAcceleration = 1f;


    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;

    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);

    Vector3 velocity, desiredVelocity;

    Rigidbody body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerinput;

        playerinput.x = Input.GetAxis("Horizontal");
        playerinput.y = Input.GetAxis("Vertical");
        playerinput = Vector2.ClampMagnitude(playerinput, 1f);

        //velocity = new Vector3(playerinput.x, 0, playerinput.y) * maxSpeed;
        desiredVelocity = new Vector3(playerinput.x, 0, playerinput.y) * maxSpeed;

        desiredJump |= Input.GetButtonDown("Jump");



        //Vector3 displacement = velocity * Time.deltaTime;

        //Vector3 newPosition = transform.localPosition + displacement;

        /*
        if (!allowedArea.Contains(new Vector2(newPosition.x, newPosition.z)))
        {
            newPosition.x =
                Mathf.Clamp(newPosition.x, allowedArea.xMin, allowedArea.xMax);
            newPosition.z =
                Mathf.Clamp(newPosition.z, allowedArea.yMin, allowedArea.yMax);
        }
        */
        //transform.localPosition = newPosition;


    }
    void FixedUpdate()
    {
        velocity = body.linearVelocity;

        float acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        float maxSpeedChange = acceleration * Time.deltaTime;
        
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        if (desiredJump)
        {
            desiredJump = false;
            Jump();
        }

        body.linearVelocity = velocity;
        onGround = false;
    }

    void Jump()
    {
        if(onGround)
            velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight); 
    }

    void OnCollisionEnter(Collision collision)
    {
        EvaluateCollision(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        EvaluateCollision(collision);
    }

    void EvaluateCollision(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            onGround |= normal.y >= 0.9f;
        }
    }
}
