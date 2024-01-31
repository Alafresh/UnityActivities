using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapArea : MonoBehaviour
{
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
            }
        }
        m_State = State.Neutral;
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
}
