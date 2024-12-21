using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region CONST FIELDS
    const float pi = 3.14f;
    #endregion

    #region SERIALIZED FIELDS
    [SerializeField]
    private Transform sideNet;
    [SerializeField]
    private Transform grabPosition;
    [SerializeField]
    private GameObject ballLineRenderer;
    #endregion

    #region PRIVATE FIELDS
    bool onShoot = false;
    #endregion

    #region PRIVATE FIELDS
    float shootTimer;
    #endregion

    #region MONO BEHAVIOURS
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q)
        )
        {
            onShoot = true;
            transform.parent = null;
            gameObject.AddComponent<Rigidbody2D>();
            Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.tag == "ragdollHand")
        {
            if (!onShoot)
            {
                Destroy(GetComponent<Rigidbody2D>());
                transform.parent = grabPosition;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "net")
        {
            onShoot = false;
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "ragdollHand")
        {
            if (!onShoot)
            {
                Destroy(GetComponent<Rigidbody2D>());
                transform.parent = grabPosition;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
        }

    }
    #endregion

    #region PRIVATE METHODS
    void Shoot()
    {
        StartCoroutine(ShootRoutine());
    }
    #endregion

    #region COROUTINE
    IEnumerator ShootRoutine()
    {
        while (true)
        {
            ballLineRenderer.SetActive(true);
            shootTimer += Time.deltaTime;
            Vector3 position = Vector3.Lerp(transform.position, sideNet.position, shootTimer);
            Vector3 arc = Vector3.up * Mathf.Sin(shootTimer * pi);
            transform.position = position + arc;
            if (shootTimer >= 1)
            {
                shootTimer = 0;
                ballLineRenderer.SetActive(false);
                yield break;
            }
            yield return null;
        }
    }
    #endregion
}
