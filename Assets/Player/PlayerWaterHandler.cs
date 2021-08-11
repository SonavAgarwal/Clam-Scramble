using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Bounds GetMaxBounds(GameObject parent) {
        var total = new Bounds(parent.transform.position, Vector3.zero);
        foreach (var child in parent.GetComponentsInChildren<Collider>())
        {
            total.Encapsulate(child.bounds);
        }
        return total;
    }
}

// using UnityEngine;
 
//  public class BoundsDebug : MonoBehaviour
//  {
//      // Visual display of the bounding box for testing.
//      private void OnDrawGizmos()
//      {
//          // Draw each child's bounds as a green box.
//          Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
//          foreach (var child in GetComponentsInChildren<Collider>())
//          {
//              Gizmos.DrawCube(child.bounds.center, child.bounds.size);
//          }
 
//          // Draw total bounds of all the children as a white box.
//          Gizmos.color = new Color(1f, 1f, 1f, 0.1f);
//          var maxBounds = GetMaxBounds(gameObject);
//          Gizmos.DrawCube(maxBounds.center, maxBounds.size);
//          Debug.Log("Total height is " + maxBounds.size.y);
//      }
 
//      Bounds GetMaxBounds(GameObject parent)
//      {
//          var total = new Bounds(parent.transform.position, Vector3.zero);
//          foreach (var child in parent.GetComponentsInChildren<Collider>())
//          {
//              total.Encapsulate(child.bounds);
//          }
//          return total;
//      }
//  }