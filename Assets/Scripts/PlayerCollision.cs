using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public Vector2 leftOnwall1, leftOnwall2, leftOnwall3;
    public Vector2 rightOnwall1, rightOnwall2, rightOnwall3;
    public float collisionRadius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOnwall1, collisionRadius,LayerMask.GetMask("Ground"))|| 
            Physics2D.OverlapCircle((Vector2)transform.position + leftOnwall2, collisionRadius, LayerMask.GetMask("Ground"))|| 
            Physics2D.OverlapCircle((Vector2)transform.position + leftOnwall3, collisionRadius, LayerMask.GetMask("Ground"))|| 
            Physics2D.OverlapCircle((Vector2)transform.position + rightOnwall1, collisionRadius, LayerMask.GetMask("Ground")) || 
            Physics2D.OverlapCircle((Vector2)transform.position + rightOnwall2, collisionRadius, LayerMask.GetMask("Ground"))||
            Physics2D.OverlapCircle((Vector2)transform.position + rightOnwall3, collisionRadius, LayerMask.GetMask("Ground"));

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOnwall1, collisionRadius, LayerMask.GetMask("Ground")) ||
            Physics2D.OverlapCircle((Vector2)transform.position + rightOnwall2, collisionRadius, LayerMask.GetMask("Ground")) ||
            Physics2D.OverlapCircle((Vector2)transform.position + rightOnwall3, collisionRadius, LayerMask.GetMask("Ground"));

        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOnwall1, collisionRadius, LayerMask.GetMask("Ground")) ||
            Physics2D.OverlapCircle((Vector2)transform.position + leftOnwall2, collisionRadius, LayerMask.GetMask("Ground")) ||
            Physics2D.OverlapCircle((Vector2)transform.position + leftOnwall3, collisionRadius, LayerMask.GetMask("Ground"));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        var positions = new Vector2[] {leftOnwall2, rightOnwall2 };
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOnwall1, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOnwall2, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOnwall3, collisionRadius);

        Gizmos.DrawWireSphere((Vector2)transform.position + leftOnwall1, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOnwall2, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOnwall3, collisionRadius);
    }
}
