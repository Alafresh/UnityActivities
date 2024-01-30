using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAreaCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<PlayerMapArea>() != null)
        {
            Debug.Log("Jugador entro");
        }
    }
}
