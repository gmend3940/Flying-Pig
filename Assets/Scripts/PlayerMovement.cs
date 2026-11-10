using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   Rigidbody rb;
    public int force = 1;
    public LayerMask PublicLayerMask;
    public GameObject flyingObject;

    private bool flying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10f, PublicLayerMask))
        {
            if (Input.GetKey(KeyCode.W))
            {
                var direction = Vector3.ProjectOnPlane(Vector3.forward, hit.normal).normalized;
                rb.AddForce(direction * force);
            }
            if (Input.GetKey(KeyCode.S))
            {
                var direction = Vector3.ProjectOnPlane(-Vector3.forward, hit.normal).normalized;
                rb.AddForce(direction * force);
            }
            if (Input.GetKey(KeyCode.D))
            {
                var direction = Vector3.ProjectOnPlane(Vector3.right, hit.normal).normalized;
                rb.AddForce(direction * force);
            }
            if (Input.GetKey(KeyCode.A))
            {
                var direction = Vector3.ProjectOnPlane(Vector3.left, hit.normal).normalized;
                rb.AddForce(direction * force);
            }
        }
        else
        {   // Flying
            if (!flying)
            {
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.rotation = Quaternion.identity;
                Physics.gravity = new Vector3(0, -10f, 0f);
            
                flyingObject.SetActive(true);
                GetComponent<MeshRenderer>().enabled = false;

                flying = true;
            }

            float turnSpeed = 60f;
            float currentYaw = rb.rotation.eulerAngles.y;
            float targetYaw = currentYaw;
            float targetRoll = 0f;

            if (Input.GetKey(KeyCode.D))
            {
                targetYaw += turnSpeed * Time.deltaTime;
                targetRoll = 30f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                targetYaw -= turnSpeed * Time.deltaTime;
                targetRoll = -30f;
            }

            Quaternion targetRotation = Quaternion.Euler(0f, targetYaw, targetRoll);
            rb.MoveRotation(targetRotation);
            
            float currentSpeed = rb.linearVelocity.magnitude;
            
            Vector3 targetVelocity = transform.forward * currentSpeed;
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, Time.deltaTime * 5f);
            
            // Add extra gravity
            rb.AddForce(Vector3.down * 15f, ForceMode.Acceleration);
        }
    }
}