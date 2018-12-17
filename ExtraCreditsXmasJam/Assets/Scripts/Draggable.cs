using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Transform))]
public class Draggable : MonoBehaviour
{
    private Vector2 holdOffset = new Vector2(0, 0);

    //private bool held = false;

    public UnityEvent pickupEvent;

    public UnityEvent dropEvent;


    // Use this for initialization
    void Start () {
        // Initialize events
        if(dropEvent == null)
        {
            dropEvent = new UnityEvent();
        }
        if (pickupEvent == null)
        {
            pickupEvent = new UnityEvent();
        }
    }

    private void OnMouseDown()
    {
        // Get the mouse's position in world space
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Define the offset from the center of the object to where it is being held
        holdOffset = mouseWorldPos - new Vector2(transform.position.x, transform.position.y);

        disableCollision();

        pickupEvent.Invoke();
    }

    private void OnMouseDrag()
    {
        // Get the camera's position in world space
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(mouseWorldPos.x - holdOffset.x,
            mouseWorldPos.y - holdOffset.y,
            0.0f);
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse released. Dispatching Event.");

        resetVelocity();

        enableCollision();

        dropEvent.Invoke();
    }

    private void disableCollision()
    {
        Collider2D coll = GetComponent<Collider2D>();
        if(coll != null)
            coll.enabled = false;
    }

    private void enableCollision()
    {
        Collider2D coll = GetComponent<Collider2D>();
        if (coll != null)
            coll.enabled = true;
    }

    private void resetVelocity()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();

        if (body)
        {
            body.velocity = new Vector2();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
