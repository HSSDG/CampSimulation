using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 120f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
