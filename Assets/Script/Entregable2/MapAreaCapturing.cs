using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAreaCapturing : MonoBehaviour
{
    [SerializeField] private List<MapArea> mapAreaList;

    [SerializeField] private MapArea mapArea;
    private Slider sliderImage;

    private void Awake()
    {
        sliderImage = transform.Find("Power_bubble").GetComponent<Slider>();
    }

    private void Start()
    {
        foreach (MapArea mapArea in mapAreaList)
        {
            mapArea.OnPlayerEnter += MapArea_OnPlayerEnter;
            mapArea.OnPlayerExit += MapArea_OnPlayerExit;
        }
        Hide();
    }
    private void Update()
    {
        sliderImage.value = mapArea.GetProgress();
    }

    private void MapArea_OnPlayerEnter(object sender, System.EventArgs e)
    {
        mapArea = sender as MapArea;
        
        Show();
    }
    private void MapArea_OnPlayerExit(object sender, System.EventArgs e)
    {
        Hide();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
