using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    private List<MapAreaCollider> m_Colliders;
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
    }
    private void Update()
    {
        int playerCountInsideMapArea = 0;
        foreach(MapAreaCollider mapAreaCollider in m_Colliders)
        {
            playerCountInsideMapArea += mapAreaCollider.GetPlayerMapAreasList().Count;
        }
    }
}
