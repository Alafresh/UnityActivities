using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAreaCapturing : MonoBehaviour
{
    [SerializeField] private List<MapArea> mapAreaList;

    private Slider sliderImage;

    private void Awake()
    {
        sliderImage = transform.Find("Power_bubble").GetComponent<Slider>();
    }
}
