using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    private float timeMax;
    [SerializeField]
    private GameObject loadingImg;

    private float scaleX;
    private bool check;
    private Vector3 TranLoadingScale;

    // Start is called before the first frame update
    void Start()
    {
        scaleX = 0;
        TranLoadingScale = loadingImg.transform.localScale;
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            scaleX += (Time.deltaTime / timeMax);
            scaleX = Mathf.Clamp01(scaleX);
        }

        TranLoadingScale.x = scaleX;
        loadingImg.transform.localScale = TranLoadingScale;

        if (scaleX == 1)
        {
            scaleX = 0;
            check = false;
            //SceneManager.LoadScene("");
        }
    }
}
