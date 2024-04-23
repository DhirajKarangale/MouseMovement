using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] internal float sensitivity;
    private Quaternion playerTargetRot;
    private Quaternion camTargetRot;


    private void Start()
    {
        playerTargetRot = transform.localRotation;
        camTargetRot = player.cam.transform.localRotation;
    }

    private void Update()
    {
        LookRotation();
    }


    private void LookRotation()
    {
        float oldYRotation = transform.eulerAngles.y;

        oldYRotation += (player.moveInput.x < 0) ? -90f : (player.moveInput.x > 0) ? 90f : 0;
        playerTargetRot = Quaternion.Euler(0f, oldYRotation, 0f);

        playerTargetRot *= Quaternion.Euler(0f, player.lookInput.x, 0f);
        camTargetRot *= Quaternion.Euler(-player.lookInput.y, 0f, 0f);
        camTargetRot = ClampRotationAroundXAxis(camTargetRot);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, playerTargetRot, 5 * Time.deltaTime);
        player.cam.transform.localRotation = Quaternion.Slerp(player.cam.transform.localRotation, camTargetRot, 5 * Time.deltaTime);

        player.moveInput.x = 0;

        Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
        player.rigidBody.velocity = velRotation * player.rigidBody.velocity;
    }


    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, -45, 30);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}