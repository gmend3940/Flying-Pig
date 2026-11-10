using UnityEngine;

public class PlayerRigidbody : MonoBehaviour
{
    Rigidbody rb;
    public int force = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.forward * force);
    }
}
