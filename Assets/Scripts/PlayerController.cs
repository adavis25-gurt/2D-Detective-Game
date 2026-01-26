using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask stopsMovement;
    public LayerMask purse;
    public LayerMask sign;
    public PurseSpawner spawner;
    public Sign signScript;
    [SerializeField] private Animator animator;

    public AudioSource grassStep;

    public bool canMove = true;

    private Vector2 movement;
    private Vector2 lastDirection;

    void Start()
    {
        movePoint.parent = null;
        lastDirection = Vector2.down;
    }

    void Update()
    {
        if (!canMove) { return; };

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector2 input = new Vector2(horizontal, vertical);

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                Vector3 targetPos = movePoint.position + new Vector3(input.x, input.y, 0f);

                if (Physics2D.OverlapCircle(targetPos, 0.05f, purse))
                {
                    movePoint.position = targetPos;
                    movement = input;
                    lastDirection = movement;
                    animator.SetBool("isMoving", true);
                    spawner.playerIsClose = true;
                }
                else if (Physics2D.OverlapCircle(targetPos, 0.05f, sign))
                {
                    movement = Vector2.zero;
                    animator.SetBool("isMoving", false);
                    signScript.playerIsClose = true;
                }
                else if (!Physics2D.OverlapCircle(targetPos, 0.05f, stopsMovement))
                {
                    movePoint.position = targetPos;
                    movement = input;
                    lastDirection = movement;
                    animator.SetBool("isMoving", true);
                    spawner.playerIsClose = false;
                    signScript.playerIsClose = false;
                }
                else
                {
                    movement = Vector2.zero;
                    animator.SetBool("isMoving", false);
                    spawner.playerIsClose = false;
                    signScript.playerIsClose = false;
                }
            }
            else
            {
                movement = Vector2.zero;
                animator.SetBool("isMoving", false);
            }
        }

        if (movement == Vector2.zero)
        {
            animator.SetFloat("moveX", lastDirection.x);
            animator.SetFloat("moveY", lastDirection.y);
        }
        else
        {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", movement.y);
        }
    }
}