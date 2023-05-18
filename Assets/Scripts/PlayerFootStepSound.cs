using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFootStepSound : MonoBehaviour
{
    private PlayerController player;
    private float footStepTime;
    private float footStepTimeMax = .1f;
    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        footStepTime -= Time.deltaTime;
        if(footStepTime < 0f)
        {
            footStepTime = footStepTimeMax;
            if(player.IsWalking())
            {
               SoundManager.Instance.FootStepPlaySound(player.transform.position);
            }
        }
    }
}
