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
    [SerializeField]
    private FixedJoint2D fixedJoint;
    #endregion

    #region PRIVATE FIELDS
    float shootTimer;
    #endregion

    #region MONO BEHAVIOURS
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && fixedJoint.enabled == true)
        {
            transform.parent = null;
            fixedJoint.enabled = false;
            Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ragdollHand")
        {
            if (fixedJoint != null)
            {
                transform.parent = grabPosition;
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                fixedJoint.enabled = true;
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
