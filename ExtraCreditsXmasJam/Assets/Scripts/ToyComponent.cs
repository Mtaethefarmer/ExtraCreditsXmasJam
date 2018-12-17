using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ComponentType
{
    Base,
    Attachment
}

[RequireComponent(typeof(Transform))]
public class ToyComponent : MonoBehaviour {

    private ComponentType compType = ComponentType.Base;

    private List<GameObject> myPoints = new List<GameObject>();
    

	// Use this for initialization
	void Start () {

        // Set component type based on tag
        if(tag == "Base")
        {
            compType = ComponentType.Base;
        }
        else if (tag == "Attachment")
        {
            compType = ComponentType.Attachment;
        }

        // Aggregate references to all owned attachment points
		foreach(Transform childT in transform)
        {
            if(childT.CompareTag("AttachPoint"))
            {
                myPoints.Add(childT.gameObject);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(transform.position.x) > 30.0f || Mathf.Abs(transform.position.y) > 30.0f)
            Destroy(gameObject);

    }

    private void Attach(AttachmentPoint myPoint, AttachmentPoint otherPoint)
    {
        transform.Rotate(new Vector3(0.0f, 0.0f,
            Vector2.SignedAngle(myPoint.AttachmentDirection, -otherPoint.AttachmentDirection)));

        // Snap the attachment points
        transform.position = otherPoint.gameObject.transform.position - myPoint.gameObject.transform.position + transform.position;

        // Parenting
        if(compType == ComponentType.Attachment)
        {
            transform.SetParent(otherPoint.transform.parent);
        }

        Rigidbody2D body = GetComponent<Rigidbody2D>();
        if (body != null)
            body.isKinematic = true;

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), otherPoint.transform.parent.GetComponent<Collider2D>());
    }

    private void Detach(GameObject other)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.GetComponent<Collider2D>(), false);

        Rigidbody2D body = GetComponent<Rigidbody2D>();
        if (body != null)
            body.isKinematic = false;

        transform.SetParent(null);
        // Reset rotation
        transform.Rotate(-transform.rotation.eulerAngles);
    }

    public void ComponentPickedUp()
    {
        if(compType == ComponentType.Attachment)
        {
            Detach(transform.parent.gameObject);
        }
    }

    public void ComponentDropped()
    {
        GameObject[] potentialAttachments;

        // Look for attachments if we are a base, and vice versa
        if(compType == ComponentType.Base)
        {
            //potentialAttachments = GameObject.FindGameObjectsWithTag("Attachment");
            return;
        }
        else
        {
            potentialAttachments = GameObject.FindGameObjectsWithTag("Base");
        }

        // Tracking the closest valid attachment point found
        GameObject closestPoint = null;
        GameObject myClosestPoint = null;
        float closestDist = float.PositiveInfinity;

        foreach (GameObject obj in potentialAttachments)
        {
            Transform objT = obj.transform;

            // Check each child under object for attachment points
            foreach(Transform childT in objT)
            {
                // Only execute for attachment points
                if (childT.tag != "AttachPoint")
                    continue;

                // Check each of our attachment points against the found one
                foreach(GameObject point in myPoints)
                {
                    // Find the distance of our point to their point
                    float dist = (childT.position - point.transform.position).magnitude;

                    // If distance is an improvement, and within max ranges, update references
                    if(dist < closestDist &&
                        dist < point.GetComponent<AttachmentPoint>().attachmentMaxRange &&
                        dist < childT.gameObject.GetComponent<AttachmentPoint>().attachmentMaxRange)
                    {
                        myClosestPoint = point;
                        closestPoint = childT.gameObject;
                        closestDist = dist;
                    }
                }
                
            }
        }

        // If neither references were found, exit here.
        if(closestPoint == null || myClosestPoint == null)
        {
            return;
        }

        Attach(myClosestPoint.GetComponent<AttachmentPoint>(), closestPoint.GetComponent<AttachmentPoint>());

        Debug.Log("Attachment successful");
    }
}
