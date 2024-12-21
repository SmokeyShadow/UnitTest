using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    #region CONSTS
    const float LEFT_X_BORDER = -16f;
    const float RIGHT_X_BORDER = 2.17f;
    const float SCREEN_CENTER = -6;
    #endregion

    #region SERIALIZEDFIELDS
    [SerializeField]
    private Balance playerBalance;
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
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float minPlayerRotate;
    [SerializeField]
    private float maxPlayerRotate;
    #endregion

    #region PRIVATE FIELDS
    private bool isOnGround;
    Vector2 randomDir;
    #endregion

    #region MONO BEHAVIOURS
    void Start()
    {
        IgnoreCollisions();
        StartCoroutine(RandomDirMoveRoutine());
        StartCoroutine(RotateBetweenAngels(minPlayerRotate, maxPlayerRotate));
        StartCoroutine(JumpRoutine());
    }

    private void Update()
    {
        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
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
    IEnumerator RotateBetweenAngels(float from, float to)
    {
        while (true)
        {
            if (isOnGround)
            {
                playerBalance.SetTargetRotation(-30);
                yield return new WaitForSeconds(6);
                playerBalance.SetTargetRotation(30);
                yield return new WaitForSeconds(6);
            }
            yield return null;
        }
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            if (isOnGround)
            {
                rigidBody.AddForce(rigidBody.transform.up * jumpForce);
                yield return new WaitForSeconds(Random.Range(3, 8));
            }
            yield return null;
        }
    }

    IEnumerator RandomDirMoveRoutine()
    {
        float timer = 0;
        float moveTime = Random.Range(1, 5);
        while (true)
        {
            if (isOnGround)
            {
                timer += Time.deltaTime / moveTime;
                if (timer >= 1)
                {
                    yield return new WaitForSeconds(Random.Range(1f, 3f));
                    randomDir = new Vector2(Random.Range(-1f, 1f), 0);
                    moveTime = Random.Range(1, 5);
                    timer = 0;
                }
                if (transform.position.x > LEFT_X_BORDER && transform.position.x < RIGHT_X_BORDER)
                    transform.Translate(Time.deltaTime * randomDir * moveSpeed);
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(SCREEN_CENTER, transform.position.y), moveSpeed * Time.deltaTime);
                }
            }
            yield return null;
        }
    }
    #endregion
}
