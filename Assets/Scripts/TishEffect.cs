using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TishEffect : MonoBehaviour
{
    public float HideDistance;
    public Transform _player;
    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) < HideDistance)
        {
            gameObject.SetActive(false);
        }
    }
}
