using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public bool onWall;
    public Vector2 leftOnwall1, leftOnwall2, leftOnwall3;
    public Vector2 rightOnwall1, rightOnwall2, rightOnwall3;
    public float collisionRadius;   //碰撞判定半径
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {//OverlapCircle()函数
        onWall = Physics2D.OverlapCircle((Vector2)transform.position+leftOnwall1, collisionRadius,LayerMask.GetMask("Ground"))|| //只跟Ground层发生判定
            Physics2D.OverlapCircle((Vector2)transform.position+ leftOnwall2, collisionRadius, LayerMask.GetMask("Ground"))|| 
            Physics2D.OverlapCircle((Vector2)transform.position+ leftOnwall3, collisionRadius, LayerMask.GetMask("Ground"))|| 
            Physics2D.OverlapCircle((Vector2)transform.position + rightOnwall1, collisionRadius, LayerMask.GetMask("Ground")) || 
            Physics2D.OverlapCircle( (Vector2)transform.position + rightOnwall2, collisionRadius, LayerMask.GetMask("Ground"))||
            Physics2D.OverlapCircle((Vector2)transform.position+rightOnwall3, collisionRadius, LayerMask.GetMask("Ground"));//transform.position，世界空间坐标
  
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOnwall1, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOnwall2, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOnwall3, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOnwall1, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOnwall2, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOnwall3, collisionRadius);
    }
}
