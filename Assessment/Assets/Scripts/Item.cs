using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] internal bool isRed;
    private bool isCollisionAllow = true;

    private void Start()
    {
        GameManager.instance.AddItem(this);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isCollisionAllow && collider.CompareTag("Player"))
        {
            GameManager.instance.UpdateScore(this);
            isCollisionAllow = false;
        }
    }
}