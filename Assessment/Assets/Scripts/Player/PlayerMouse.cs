using UnityEngine;
using UnityEngine.AI;

public class PlayerMouse : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool isHover = true;

    private void Update()
    {
        if (isHover || Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, layerMask))
            {
                if (hit.collider.CompareTag("Item")) Move(hit.point);
            }
        }

        player.animator.SetFloat("speed", player.agent.velocity.magnitude);
    }

    private void Move(Vector3 pos)
    {
        player.agent.SetDestination(pos);
    }
}