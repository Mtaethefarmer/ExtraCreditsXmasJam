using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPoint : MonoBehaviour
{

    public Vector2 AttachmentDirection = new Vector2(1.0f, 0.0f);

    public float gizmoLength = 0.1f;

    public float attachmentMaxRange = 0.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.05f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position +
            new Vector3(AttachmentDirection.normalized.x * gizmoLength, AttachmentDirection.normalized.y * gizmoLength, 0.0f));
    }
    
}
