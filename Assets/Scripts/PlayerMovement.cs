using UnityEngine;
// using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {

    // private float horizontalInput;
    // private float wallJumpCooldown;

    // private Rigidbody2D body;
    // private Animator animator;
    // private TilemapCollider2D tilemapCollider;

    // [SerializeField] private float speed;
    // [SerializeField] private float jumpPower;
    // [SerializeField] private LayerMask groundLayer;
    // [SerializeField] private LayerMask wallLayer;

    // // Called when instance of script is loaded
    // private void Awake() {
    //     // this method checks the player component for type RigidBody2D - then stores inside body variable
    //     body = GetComponent<Rigidbody2D>();
    //     animator = GetComponent<Animator>();
    //     tilemapCollider = GetComponent<TilemapCollider2D>();
    // }

    // // Runs on every frame of the game - to detect when player presses left/right
    // private void Update() {
    //     horizontalInput = Input.GetAxis("Horizontal");

    //     // this if conditions validates and updates the x value after detecting user left-right input
    //     if (horizontalInput > 0.01f)
    //         transform.localScale = Vector3.one;
    //     else if (horizontalInput < -0.01f)
    //         transform.localScale = new Vector3(-1, 1, 1); 

    //     // configuring animator properties to notify animations
    //     // != 0 because horizontalInput reflects user input of left or right keys 
    //     animator.SetBool("run", horizontalInput != 0);
    //     animator.SetBool("grounded", isGrounded());
        
    //     // this if condition is facilitates the jump logic 
    //     // if (wallJumpCooldown > 0.2f){

    //         // to assign speed on all directions (left/right, up/down, forward/backwards)
    //         // getAxis and Horizontal is defined by unity and helps avoid if/else statements
    //         body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

    //         // if (onWall() && !isGrounded()){
    //         //     body.gravityScale = 0;
    //         //     body.velocity = Vector2.zero;
    //         // }
    //         // else
    //         //     body.gravityScale = 3;

    //         // if statement to check if space bar is pressed
    //         // keyCode. => can be any key you would like to listen for 
    //         if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && isGrounded())
    //             Jump();
    //     // }

    //     // else 
    //     //     wallJumpCooldown += Time.deltaTime;
    // }

    // private void Jump(){
    //     if (isGrounded()){
    //         body.velocity = new Vector2(body.velocity.x, jumpPower);
    //         animator.SetTrigger("jump");
    //     }
    //     else if (onWall() && !isGrounded()){
    //         if (horizontalInput == 0){
    //             body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
    //             transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    //         }
    //         else
    //             // flips the player to face left when negative and right when positive 
    //             // only for when the player is stuck to the wall
    //             // the negative symbol infront of Mathf function is to push the player away from the wall
    //             // value multiplied by 3 is the power that pushes the player away from the wall
    //             // 6 is the force in which the player will be pushed upwards
    //             body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

    //         wallJumpCooldown = 0;
    //     }
    // }

    // private bool isGrounded(){
    //     // uses the box (of character) to detect any collision
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(tilemapCollider.bounds.center, tilemapCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
    //     print(tilemapCollider.offset);
    //     // return raycastHit.collider != null;
    //     return true;
    // }

    // private bool onWall(){
    //     // uses the box (of wall) to detect any collision
    //     // only the box projection will change horizontally
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(tilemapCollider.bounds.center, tilemapCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    //     // return raycastHit.collider != null;
    //     return false;
    // }

    // // Specifying certain rules before player can attack
    // public bool canAttack(){
    //     return horizontalInput == 0 && isGrounded() && !onWall();
    // }



    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float jump;
    private float horizontalInput;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // this if conditions validates and updates the x value after detecting user left-right input
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // configuring animator properties to notify animations
        // != 0 because horizontalInput reflects user input of left or right keys 
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", !isJumping);

        rb.velocity = new Vector2(horizontalInput*speed,rb.velocity.y);
        if(Input.GetKeyDown("up")&& !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x,jump);
            animator.SetTrigger("jump");
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping=false;
       
        }
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && !isJumping;
    }

}

