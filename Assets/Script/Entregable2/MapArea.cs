using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapArea : MonoBehaviour
{
    public event EventHandler OnPlayerEnter;
    public event EventHandler OnPlayerExit;

    public enum State
    {
        Neutral,
        Captured,
    }
    private List<MapAreaCollider> m_Colliders;
    private State m_State;
    private float progress;
    private void Awake()
    {
        m_Colliders = new List<MapAreaCollider>();

        foreach(Transform child in transform)
        {
            MapAreaCollider mapAreaCollider = child.GetComponent<MapAreaCollider>();
            if (mapAreaCollider != null)
            {
                m_Colliders.Add(mapAreaCollider);
                mapAreaCollider.OnPlayerEnter += MapAreaCollider_OnPlayerEnter;
                mapAreaCollider.OnPlayerExit += MapAreaCollider_OnPlayerExit;
            }
        }
        m_State = State.Neutral;
    }
    private void MapAreaCollider_OnPlayerEnter(object sender, EventArgs e)
    {
        OnPlayerEnter?.Invoke(this, EventArgs.Empty);
    }
    private void MapAreaCollider_OnPlayerExit(object sender, EventArgs e)
    {
        bool hasPlayerInside = false;
        foreach(MapAreaCollider mapAreaCollider in m_Colliders)
        {
            if(mapAreaCollider.GetPlayerMapAreasList().Count > 0)
            {
                hasPlayerInside = true;
            }
            if (! hasPlayerInside ) {
                OnPlayerExit?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    private void Update()
    {
        switch (m_State)
        {
            case State.Neutral:
                List<PlayerMapArea> playerMapAreaInsideList = new List<PlayerMapArea>();

                foreach (MapAreaCollider mapAreaCollider in m_Colliders)
                {
                    foreach (PlayerMapArea playerMapArea in mapAreaCollider.GetPlayerMapAreasList())
                    {
                        if (!playerMapAreaInsideList.Contains(playerMapArea))
                        {
                            playerMapAreaInsideList.Add(playerMapArea);
                        }
                    }
                }
                float progressSpeed = .5f;
                progress += playerMapAreaInsideList.Count * progressSpeed * Time.deltaTime;
                Debug.Log("playerCountInsideMapArea: " + playerMapAreaInsideList.Count + "; progress: " + progress);
                
                if (progress > 1f)
                {
                    m_State = State.Captured;
                    Debug.Log("Capturado!");
                }
                
                break;
            case State.Captured:
                break;
        }
    }
    public float GetProgress()
    {
        return progress;
    }
}