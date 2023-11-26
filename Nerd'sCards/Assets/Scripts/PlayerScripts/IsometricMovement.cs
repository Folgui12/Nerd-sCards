using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float controllerDeadZone = 0.3f;
    [SerializeField] private float rotateSmoothing = 1000f;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashTime;
    [SerializeField] private LayerMask invisibleWall;
    [SerializeField] private Transform pjFront;
    public Transform currentRoom;
    public bool isGamepad;
    public Vector3 playerLookAt;

    private Vector2 aim;
    private Vector3 input;
    private PlayerAttackSystem attackRef;
    private float movementWhileAttacking;
    private PlayerInputActions playerInputActions;
    private PlayerInput playerInput;

    private Vector3 relativePosition;
    private bool onMapLimit = false;

    private float currentSpeed;
    private float aimingSpeed;
    private float normalSpeed;

    [SerializeField] private bool canDash;
    public bool canRange;
    public bool hasAttack;
    private Vector3 currentDir;

    private void Awake()
    {
        attackRef = GetComponentInChildren<PlayerAttackSystem>();
        movementWhileAttacking = .015f;
        playerInputActions = new PlayerInputActions();
        playerInput = GetComponent<PlayerInput>();
        normalSpeed = 6;
        aimingSpeed = normalSpeed / 2;
        currentSpeed = normalSpeed;
    }

    void OnEnable()
    {
        playerInputActions.Enable();
    }

    void OnDisable()
    {
        playerInputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        GatherInput();
        Look();
        HandleMouseInput();
        HandlePlayerRotation();

        currentDir = Vector3.right * aim.x + Vector3.forward * aim.y;
        playerLookAt = transform.position + (pjFront.position - transform.position);

        if (!attackRef.canAttack)
            transform.Translate(Vector3.forward*movementWhileAttacking);
    }

    private void FixedUpdate()
    {
        if(attackRef.canAttack)
            Move();
    }

    void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    void Look()
    {
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

        var skewedInput = matrix.MultiplyPoint3x4(input);

        relativePosition = transform.position + skewedInput - transform.position;
    }

    void Move()
    {
        rb.MovePosition(transform.position + relativePosition.normalized * relativePosition.magnitude * currentSpeed * Time.deltaTime);
    }

    private void HandleMouseInput() => aim = playerInputActions.Player.Aim.ReadValue<Vector2>();

    private void HandlePlayerRotation()
    {

        if(isGamepad)
        {
            if(Mathf.Abs(aim.x) > controllerDeadZone || Mathf.Abs(aim.y) > controllerDeadZone)
            {

                if (playerLookAt.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(currentDir, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSmoothing * Time.deltaTime);
                }
            } 
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayDistance;

            if(groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }
    }

    private void LookAt(Vector3 lookPoint)
    {
        playerLookAt = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(playerLookAt);
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            currentRoom = other.gameObject.transform;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 10)
            ScreenManager.Instance.PlayerWin();
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Boundary")
        {
            onMapLimit = true;
            StopCoroutine(Dash());
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Boundary")
        {
            onMapLimit = false;
        }
    }

    public void DetectDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
            StartCoroutine(Dash());
    }

    public void walkWhileAiming()
    {
        currentSpeed = aimingSpeed;
    }

    public void walkWhioutAiming()
    {
        currentSpeed = normalSpeed;
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < dashTime + startTime)
        {
            if(!onMapLimit)
                transform.position += relativePosition * Time.deltaTime * dashDistance;
            yield return null;
        }
        
    }

}
