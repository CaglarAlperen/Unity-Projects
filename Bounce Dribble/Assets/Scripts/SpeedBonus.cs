using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBonus : MonoBehaviour
{

    [SerializeField] AudioClip SFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(SFX, Camera.main.transform.position);
        FindObjectOfType<Ball>().ResetSpeed();
        Destroy(gameObject);
    }
}
