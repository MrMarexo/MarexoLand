using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShadow : MonoBehaviour
{
    [SerializeField] float offset = 0.1f;
    
    void Start()
    {
        var srs = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            if (sr.gameObject.layer == LayerMask.NameToLayer("Walls") || (sr.gameObject.layer == LayerMask.NameToLayer("Enemy") && sr.gameObject.name != "Death"))
            {
                var parent = sr.transform.parent;
                var oPos = sr.transform.position;
                var newPos = new Vector3(oPos.x - offset, oPos.y - offset, oPos.z);
                var clone = Instantiate(sr, newPos, sr.transform.rotation, parent);
                clone.color = Colors.semiTransparentColor;
                clone.sortingOrder = -1;
                clone.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

}
