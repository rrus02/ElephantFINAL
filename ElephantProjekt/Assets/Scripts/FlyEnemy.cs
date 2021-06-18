using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public float speed, circleRadius;

    private Rigidbody2D EnemyRB;
    public GameObject rightCheck, roofCheck, groundCheck, enemyCheck;
    public LayerMask groundLayer;
    private bool facingRight = true, groundTouch, roofTouch, rightTouch, enemyTouch;
    public float dirX = 1f, dirY = 0.25f;


    void Start()
    {
        EnemyRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //transform.Translate(new Vector2(dirX, dirY).normalized * speed * Time.fixedDeltaTime, Space.World);
        EnemyRB.velocity = new Vector2(dirX, dirY) * speed * Time.deltaTime;
        //EnemyRB.velocity = new Vector2(dirX, dirY).normalized * speed * Time.deltaTime;
        HitDetection();
        Debug.Log(EnemyRB.velocity.magnitude/Time.deltaTime);
    }

    void HitDetection()
    {
        rightTouch = Physics2D.OverlapCircle(rightCheck.transform.position, circleRadius, groundLayer);
        roofTouch = Physics2D.OverlapCircle(roofCheck.transform.position, circleRadius, groundLayer);
        groundTouch = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
        enemyTouch = Physics2D.OverlapCircle(enemyCheck.transform.position, circleRadius, groundLayer);
        HitLogic();
    }

    void HitLogic()
    {
        if (rightTouch && facingRight)
        {
            Flip();
        }
        else if (rightTouch && !facingRight)
        {
            Flip();
        }
        if (roofTouch)
        {
            dirY = -0.25f;
        }
        else if (groundTouch)
        {
            dirY = 0.25f;
        }
        if (enemyTouch)
        { Flip(); }

    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        dirX = -dirX;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rightCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(roofCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
        Gizmos.DrawWireSphere(enemyCheck.transform.position, circleRadius);
    }
}
