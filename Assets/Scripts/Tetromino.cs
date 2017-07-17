using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private BoxCollider2D[] colliders;
    private Rigidbody2D rb;
    private float tickRate = 1f;
    private bool isFalling;
    private bool playerRecentlyMoved = false;

    // Use this for initialization
    void Start()
    {
        // GameManager.instance.pieceFalling = true;
        colliders = GetComponentsInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        isFalling = true;

        StartCoroutine(Gravity());
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling && !playerRecentlyMoved)
        {
            int horizontal = (int)Input.GetAxisRaw("Horizontal");
            int vertical = (int)Input.GetAxisRaw("Vertical");

            bool rotate = Input.GetKeyDown(KeyCode.Space);

            if (rotate)
            {
                Rotate();
            }

            if (horizontal != 0 || vertical > 0)
            {
                vertical = 0;
            }

            if (horizontal != 0 || vertical != 0)
            {
                playerRecentlyMoved = AttemptMove(horizontal, vertical);
                if (playerRecentlyMoved)
                    StartCoroutine(PlayerMove());
            }
        }
    }

    private void Rotate()
    {
        rb.MoveRotation(90f);
    }

    private bool AttemptMove(int xDir, int yDir)
    {
        RaycastHit2D hit;
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        // compCollider.enabled = false;
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        hit = Physics2D.Linecast(start, end);
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
        // compCollider.enabled = true;

        if (hit.transform == null)
            rb.MovePosition(end);

        return hit.transform == null;
    }

    private IEnumerator PlayerMove()
    {
        yield return new WaitForSeconds(0.2f);

        playerRecentlyMoved = false;
    }

    private IEnumerator Gravity()
    {
        while (AttemptMove(0, -1))
        {
            yield return new WaitForSeconds(tickRate);
        }

        Debug.Log("Finished falling");
        isFalling = false;
        // GameManager.instance.pieceFalling = false;
    }
}