using UnityEngine;

public class PlayerKeyboard : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float speed;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] AnimationCurve slopeCurveModifier;

    private float playerYMax;
    private bool isGrounded;
    private RaycastHit hit;
    private Vector3 groundDir;
    private AnimatorStateInfo animCurrentState;
    

    private void Update()
    {
        GroundCheck();
        AddGroundForce();
        Move();
    }


    #region Move

    private void Move()
    {
        if (Mathf.Abs(player.moveInput.magnitude) > 0.1f && isGrounded)
        {
            Vector3 desiredMove = transform.forward * player.moveInput.y + player.cam.transform.right * player.moveInput.x;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, groundDir).normalized;

            desiredMove.x = desiredMove.x * speed;
            desiredMove.z = desiredMove.z * speed;
            desiredMove.y = desiredMove.y * speed;

            if (player.rigidBody.velocity.sqrMagnitude < (speed * speed))
            {
                player.rigidBody.AddForce(desiredMove * Time.deltaTime * slopeCurveModifier.Evaluate(Vector3.Angle(groundDir, Vector3.up)), ForceMode.Impulse);
            }
        }
    }

    #endregion

    #region Ground

    private void AddGroundForce()
    {
        if (isGrounded)
        {
            player.rigidBody.drag = 5f;
            if (Mathf.Abs(player.moveInput.x) < float.Epsilon && Mathf.Abs(player.moveInput.y) < float.Epsilon && player.rigidBody.velocity.magnitude < 1f) player.rigidBody.Sleep();
        }
        else
        {
            player.rigidBody.drag = 0f;
        }
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, groundCheckDistance, groundLayerMask))
        {
            isGrounded = true;
            float distance = playerYMax - transform.position.y;

            playerYMax = transform.position.y;
            groundDir = hit.normal;

            Anim();
        }
        else
        {
            isGrounded = false;
            groundDir = Vector3.up;
            if (playerYMax < transform.position.y) playerYMax = transform.position.y;
        }
    }

    #endregion

    #region Anim

    private void Anim()
    {
        player.animator.SetFloat("speed", player.moveInput.y);
    }

    #endregion
}