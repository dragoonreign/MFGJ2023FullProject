using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpCount = 1f;
    [SerializeField] private float maxJumpCount = 2f;
    [SerializeField] private bool bIsJumping;
    [SerializeField] private bool bIsStartupJumping = true;
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float maxVertSpeed = 2f;
    [SerializeField] private float retainSpeedPercentage = .98f;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction m_jump;
    [SerializeField] private InputAction m_shoot;
    [SerializeField] private InputAction m_reset;
    [SerializeField] private MyDefaultInputAction myDefaultInputAction;
    public Vector3 _input;
    public GameObject groundDetector;

    //cooldown
    public Cooldown jumpCD;
    public float jumpCD_Length = .5f;



    private void Awake() {
        jumpCD = new Cooldown();
        playerInput = GetComponent<PlayerInput>();
        myDefaultInputAction = new MyDefaultInputAction();
    }

    private void OnEnable() {
        m_jump = myDefaultInputAction.Player.Jump;
        m_shoot = myDefaultInputAction.Player.Shoot;
        m_reset = myDefaultInputAction.Player.Reset;
        m_jump.Enable();
        m_shoot.Enable();
        m_reset.Enable();

        myDefaultInputAction.Player.Jump.performed += DoJump;
        myDefaultInputAction.Player.Jump.Enable();
    }

    private void Update() {
        Time.timeScale = 0.5f;

        GatherInput();
        JumpCooldown();
        OnGrounded();

        if (m_shoot.WasPerformedThisFrame())
        {
            DoShoot();
        }

        if (m_reset.WasPerformedThisFrame())
        {
            GameManager.instance.DoResetLevel();
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void GatherInput() {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        Jump(context);
    }

    private void DoShoot()
    {
        Debug.Log("pew pew");
    }

    void JumpCooldown()
    {
        StartJumpCooldown();
    }

    private void Jump(InputAction.CallbackContext context) {
        if (jumpCount <= 0) return;
        if (Input.GetKeyDown("space") || m_jump.WasPerformedThisFrame())
        {
            bIsStartupJumping = true;
            bIsJumping = true;
            DoDecrementJumpCount();
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
            _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            OnJump();
        }
    }

    public void StartJumpCooldown()
    {
        //start cooldown
        if (jumpCD.CDStarted)
        {
            //SpeedBoost is on cooldown
            jumpCD.UpdateCooldown(jumpCD);

            // groundDetector.SetActive(false);
        } 

        if (jumpCD.CDBool())
        {
            //Off-Cooldown
            jumpCD.CDReset = false;
            bIsStartupJumping = false;
        }
    }

    public void OnJump()
    {
        StartAbilityCooldown(jumpCD, jumpCD_Length);
    }

    public void OnGrounded()
    {
        //jump button was pressed & is detecting ground & is launching
        if (bIsJumping && groundDetector.GetComponent<GroundDetector>().bIsGrounded && bIsStartupJumping) {
            // Debug.Log("Startup");
        }

        //jump button was pressed & is detecting ground & is landing
        if (bIsJumping && groundDetector.GetComponent<GroundDetector>().bIsGrounded && !bIsStartupJumping)
        {
            // Debug.Log("Landing");
            // DoIncrementJumpCount();
            DoResetJumpCount();
            bIsJumping = false;
            bIsStartupJumping = true;
        } 
        
    }

    public void DoResetJumpCount()
    {
        jumpCount = 1;
    }

    public void DoIncrementJumpCount()
    {
        jumpCount++;
    }

    public void DoDecrementJumpCount()
    {
        jumpCount--;
    }

    private void Move() {
        if (groundDetector)
        {
            if (_input == Vector3.zero && !bIsJumping && groundDetector.GetComponent<GroundDetector>().bIsGrounded) 
            {
                _rb.velocity = _rb.velocity * (retainSpeedPercentage / 100);
            }
        }

        SetMaxVelocity();

        _rb.AddForce(( transform.right * -_input.z + transform.forward * _input.x).normalized * _speed - _rb.velocity, ForceMode.Force);
        // _rb.MovePosition(transform.position + (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")) * _speed * Time.deltaTime);
    }

    public void StartAbilityCooldown(Cooldown cd, float cdLength)
    {
        cd.CDStart(cdLength);
    }

    void SetMaxVelocity()
    {
        Vector3 xzVel = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        Vector3 yVel = new Vector3(0, _rb.velocity.y, 0);

        xzVel = Vector3.ClampMagnitude(xzVel, maxSpeed);
        yVel = Vector3.ClampMagnitude(yVel, maxVertSpeed);

        _rb.velocity = xzVel + yVel;
    }

}

public static class Helpers 
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
