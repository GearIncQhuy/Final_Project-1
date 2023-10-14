using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMap : Singleton<ManagerMap>
{
    [SerializeField] private GameObject PreFab_Map1;
    [SerializeField] private GameObject PreFab_Map2;
    [SerializeField] private GameObject PreFab_Map3;
    [SerializeField] private Transform Map1;
    [SerializeField] private Transform Map2;
    [SerializeField] private Transform Map3;
    [SerializeField] private ScriptTableGame data;

    public List<GameObject> ListMap = new List<GameObject>();
    private int mapCurrent;
    private int mapNext;

    private void Start()
    {
        mapCurrent = data.map;
    }

    public void NextMap()
    {
        mapNext = mapCurrent + 1;
        // map <= 3
        if(mapNext <=3 && mapNext > 0)
        {
            switch (mapNext)
            {
                case 1:
                    CreateMap(PreFab_Map1, Map1);
                    break;
                case 2:
                    CreateMap(PreFab_Map2, Map2);
                    break;
                case 3:
                    CreateMap(PreFab_Map3, Map3);
                    break;
            }
        }

        // map > 3
    }

    private void CreateMap(GameObject PreFab_map, Transform MapPosition)
    {
        if(PreFab_map != null)
        {
            GameObject MapObj = Instantiate(PreFab_map, MapPosition.position, Quaternion.identity);
            ListMap.Add(MapObj);
            Destroy(ListMap[0]);
            ManagerScript.Ins.player.transform.position = MapPosition.position;
            data.map++;
        }
    }
}
