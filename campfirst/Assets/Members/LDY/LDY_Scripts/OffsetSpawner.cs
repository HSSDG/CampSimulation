using UnityEngine;

public class OffsetSpawner : MonoBehaviour
{
    void OnEnable()
    {
        MoveToPlayerOffset();
    }

    void MoveToPlayerOffset()
    {
        Transform player = GameObject.FindWithTag("MainCamera").transform;

        Vector3 offset = new Vector3(0, 0, 0.5f);
        transform.position = player.position + player.forward * offset.z +
                            player.right * offset.x + player.up * offset.y;
    }
}
