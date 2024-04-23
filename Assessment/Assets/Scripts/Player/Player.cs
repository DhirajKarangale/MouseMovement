using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] internal Camera cam;
    [SerializeField] internal Animator animator;
    [SerializeField] internal NavMeshAgent agent;
    [SerializeField] internal Rigidbody rigidBody;

    [Header("Scripts")]
    [SerializeField] PlayerLook playerLook;
    [SerializeField] PlayerMouse playerMouse;
    [SerializeField] PlayerKeyboard playerKeyboard;

    [SerializeField] bool isKeyboard;

    internal Vector2 lookInput, moveInput;


    private void Start()
    {
        agent.enabled = !isKeyboard;
        playerMouse.enabled = !isKeyboard;
        playerKeyboard.enabled = isKeyboard;
    }

    private void Update()
    {
        GetInput();
    }


    private void GetInput()
    {
        lookInput.x = Input.GetAxis("Mouse X") * playerLook.sensitivity * 6;
        lookInput.y = Input.GetAxis("Mouse Y") * playerLook.sensitivity * 6;

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
    }


    internal void GameOver()
    {
        animator.SetFloat("speed", 0);
        playerLook.enabled = false;
        playerMouse.enabled = false;
        playerKeyboard.enabled = false;
        enabled = false;
    }
}