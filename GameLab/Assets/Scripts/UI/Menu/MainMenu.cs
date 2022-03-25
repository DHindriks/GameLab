using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject game;
    [SerializeField] GameObject options;
    [SerializeField] GameObject exit;
    [SerializeField] GameObject host;
    [SerializeField] GameObject join;
    [SerializeField] GameObject local;
    public void onClickGame()
    {
        main.SetActive(false);
        game.SetActive(true);
    }
    public void onClickOptions()
    {

    }
    public void onClickExit()
    {

    }
    public void onClickHost()
    {
        game.SetActive(false);
        host.SetActive(true);
    }
    public void onClickJoin()
    {
        game.SetActive(false);
        join.SetActive(true);
    }
    public void onClickLocal()
    {
        game.SetActive(false);
        local.SetActive(true);
    }
    public void onClickBackToMain()
    {
        game.SetActive(false);
        main.SetActive(true);
    }
    public void onClickBackToGame()
    {
        host.SetActive(false);
        join.SetActive(false);
        local.SetActive(false);
        game.SetActive(true);
    }
}
