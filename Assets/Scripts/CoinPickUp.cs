using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip _coinAudioFx;

    [SerializeField] int _pointsForCoinPick = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().ChangeScoreBoard(_pointsForCoinPick);

        AudioSource.PlayClipAtPoint(_coinAudioFx, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
