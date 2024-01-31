using System;
using System.Collections.Generic;
using UnityEngine;

public class MapAreaCollider : MonoBehaviour
{
    public event EventHandler OnPlayerEnter;
    public event EventHandler OnPlayerExit;

    private List<PlayerMapArea> playerMapAreasList = new List<PlayerMapArea>();

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<PlayerMapArea>(out PlayerMapArea playerMapArea))
        {
            playerMapAreasList.Add(playerMapArea);
            OnPlayerEnter?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent<PlayerMapArea>(out PlayerMapArea playerMapArea))
        {
            playerMapAreasList.Remove(playerMapArea);
            OnPlayerExit?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<PlayerMapArea> GetPlayerMapAreasList()
    {
        return playerMapAreasList;
    }
}
