using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float camRayLength = 100f;

    public void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        Move(horizontal, vertical);
        Turn();
        Animate(horizontal, vertical);
    }

    private void Move(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0f, vertical);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turn()
    {
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;
        if (!Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            return;
        }

        var playerToMouse = floorHit.point - transform.position;
        playerToMouse.y = 0f;

        var newRotation = Quaternion.LookRotation(playerToMouse);
        playerRigidbody.MoveRotation(newRotation);
    }

    private void Animate(float horizontal, float vertical)
    {
        var walking = horizontal != 0f || vertical != 0f;
        anim.SetBool("IsWalking", walking);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            var playerShooting = GetComponentInChildren<PlayerShooting>();
            if (playerShooting.AddAmmo())
            {
                Destroy(other.gameObject);
            }
        }
    }
}
