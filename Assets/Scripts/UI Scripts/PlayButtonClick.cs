using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Image img;
    [SerializeField] private Sprite defaultbut, pressedbut;
    [SerializeField] private AudioClip compressedClip, uncompressedClip;
    [SerializeField] private AudioSource source;
    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = pressedbut;
        source.PlayOneShot(compressedClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = defaultbut;
        source.PlayOneShot(uncompressedClip);
    }
}
