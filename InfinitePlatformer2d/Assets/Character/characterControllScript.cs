using UnityEngine;
using System.Collections;

public class characterControllScript : MonoBehaviour
{
    public float maxWalkSpeed = 120f;
    public float maxRunSpeed = 230f;
    
    float maxSpeed;
    bool facingRight = true;

    Animator anim;
    Rigidbody2D rbody2d;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    public float jumpForce = 7000f;

    void Start ()
    {
        anim = GetComponent<Animator>();
        rbody2d = GetComponent<Rigidbody2D>();
        maxSpeed = maxWalkSpeed;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rbody2d.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move * maxSpeed));
        rbody2d.velocity = new Vector2(move * maxSpeed, rbody2d.velocity.y);

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Update()
    {
        if(grounded)
            maxSpeed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? maxRunSpeed : maxWalkSpeed;

        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rbody2d.AddForce(new Vector2(0, jumpForce));
        }
                
	}
}
