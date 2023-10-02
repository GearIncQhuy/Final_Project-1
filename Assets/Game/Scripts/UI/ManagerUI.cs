using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerUI : MonoBehaviour
{
    public void Create_UI(GameObject uiPreFab)
    {
        GameObject NewgameObject = Instantiate(uiPreFab);
        NewgameObject.transform.SetParent(transform, false);
    }
    public void Destroy_UI(GameObject uiPreFab)
    {
        Destroy(uiPreFab.gameObject);
    }
}
