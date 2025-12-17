using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;

    public LayerMask stopsMovement;
    [SerializeField] private Animator animator;

    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("moveX", horizontal);
            animator.SetFloat("moveY", vertical);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= .05f)
        {
            if (Mathf.Abs(horizontal) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(horizontal, 0f, 0f), .05f, stopsMovement))
                {
                    vertical = 0f;
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }

            if (Mathf.Abs(vertical) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, vertical, 0f), .05f, stopsMovement))
                {
                    horizontal = 0f;
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }
            }
        }
    }
}
