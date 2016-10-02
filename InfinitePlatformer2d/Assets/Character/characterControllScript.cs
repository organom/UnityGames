using UnityEngine;
using System.Collections;

public class characterControllScript : MonoBehaviour
{
    public float maxSpeed = 150f;
    bool facingRight = true;

    Animator anim;
    Rigidbody2D rigidbody2D;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.3f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;

	void Start ()
    {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rigidbody2D.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

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
        if(grounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rigidbody2D.AddForce(new Vector2(0, jumpForce*3));
        }
                
	}
}
