using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    #region SERIALIZEDFIELDS
    [SerializeField]
    public float targetRotation;
    [SerializeField]
    public Rigidbody2D rigidBody;
    [SerializeField]
    public float force;
    #endregion

    #region MONO BEHAVIUORS
    public void Update()
    {
        rigidBody.MoveRotation(Mathf.LerpAngle(rigidBody.rotation, targetRotation, force * Time.fixedDeltaTime));
    }
    #endregion

    #region PUBLIC METHODS
    public void SetTargetRotation(float target)
    {
        targetRotation = target;
    }
    #endregion
}
