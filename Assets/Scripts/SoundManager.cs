using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    public static SoundManager Instance { get; private set; } 
    void Start()
    {
        Instance = this;
        DeliveryManager.Instance.OnDeliverySuccess += Instance_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFalse += Delivery_OnDeliveryFalse;
        PlayerController.Instance.OnPlayerPickUp += Player_OnPlayerPickUp;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        BaseCounter.OnAnyObjectPlaceHere += BaseCounter_OnAnyObjectPlaceHere;
        TrashCounter.OntrashGrab += TrashCounter_OntrashGrab;
    }

    private void TrashCounter_OntrashGrab(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlaceHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void Player_OnPlayerPickUp(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickUp, PlayerController.Instance.transform.position);
    }

    private void Instance_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliverycounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliverycounter.transform.position);
    }

    private void Delivery_OnDeliveryFalse(object sender, System.EventArgs e)
    {
        DeliveryCounter deliverycounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFalse, deliverycounter.transform.position);
    }


    public void PlaySound(AudioClip audioClip,Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void FootStepPlaySound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipRefsSO.footStep, position, volume);
    }
}
