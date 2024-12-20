using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region SERIALIZEDFIELDS
    [SerializeField]
    private Rigidbody2D rigidBody;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float positionRadius;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private float rotateSpeed;
    #endregion

    #region PRIVATE FIELDS
    private bool isOnGround;
    #endregion

    #region MONO BEHAVIOURS
    void Start()
    {
        IgnoreCollisions();
        StartCoroutine(RotateByForce());
    }

    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                rigidBody.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
            }
            else
            {
                rigidBody.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
            }
        }

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.AddForce(Vector2.up * jumpForce);
        }
    }
    #endregion

    #region PRIVATE METHODS
    void IgnoreCollisions()
    {
        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();

        for (int i = 0; i < colliders.Length; i++)
        {
            for (int j = i + 1; j < colliders.Length; j++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[j]);
            }
        }
    }
    #endregion

    #region COROUTINES
    IEnumerator RotateByForce()
    {
        while(true)
        {
            float x = Random.Range(-1f, 1);
            float y = Random.Range(-1f, 1);
            rigidBody.AddForce(new Vector3(x,y,0) * rotateSpeed);
            yield return new WaitForSeconds(5);   
        }
    }
    #endregion
}
