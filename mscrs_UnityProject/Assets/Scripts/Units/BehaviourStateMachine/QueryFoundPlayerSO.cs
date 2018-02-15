// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "QueryFoundPlayer", menuName = "Behaviour State Machine/Query - Found Player")]
public class QueryFoundPlayerSO : QueryBaseSO
{
    public override int DoQuery(UnitController unitController)
    {
        PlayerController playerController = SceneController.Instance.PlayerController;
        if (playerController.IsDead)
            return 0;

        float detectRange = unitController.BehaviourSettings.DetectRange;
        Transform playerTransform = playerController.EnemyRaycastTarget;
        Transform unitTransform = unitController.BehaviourRaycastTarget;
        Vector2 unitToPlayer = playerTransform.position - unitTransform.position;

        //if (Vector2.Distance(unitTransform.position, playerTransform.position) > detectRange)
        if (unitToPlayer.magnitude > detectRange)
        {
            Debug.DrawRay(unitTransform.position, unitToPlayer.normalized*detectRange, Color.gray);
            return 0;
        }

        RaycastHit2D hit = Physics2D.Raycast(unitTransform.position, unitToPlayer, unitToPlayer.magnitude, LayerMask.GetMask("Ground"));
        Debug.DrawRay(unitTransform.position, unitToPlayer, hit.collider == null ? Color.green : Color.blue, 1f);
        return (hit.collider == null)?1:0;
    }
}