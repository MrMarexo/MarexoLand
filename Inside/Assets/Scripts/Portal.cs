using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform location;

    public Vector3 GetPortalTargetPosition()
    {
        return location.position;
    }
}
