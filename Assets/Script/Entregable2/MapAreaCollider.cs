using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapAreaCollider : MonoBehaviour
{
    private List<PlayerMapArea> playerMapAreasList = new List<PlayerMapArea>();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<PlayerMapArea>(out PlayerMapArea playerMapArea))
        {
            playerMapAreasList.Add(playerMapArea);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<PlayerMapArea>(out PlayerMapArea playerMapArea))
        {
            playerMapAreasList.Remove(playerMapArea);
        }
    }

    public List<PlayerMapArea> GetPlayerMapAreasList()
    {
        return playerMapAreasList;
    }
}
