// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class CameraZone : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    Transform _targetPositionTransform;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    public Vector3 TargetPos { get { return _targetPositionTransform.position; } }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            CameraController.Instance.CameraZoneEnter(this);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            CameraController.Instance.CameraZoneExit(this);
    }
    #endregion
}