using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabGroup : MonoBehaviour
{
    [Header("Tab Buttons")]
    public List<TabButton> tabButtons;

    [Header("Selected Colors")]
    public Color tabIdle;
    public Color tabHover;
    public Color tabSelected;

    [Header("Objects")]
    private TabButton selectedTab;
    public List<GameObject> objectsToSwap = new List<GameObject>();


    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();

        tabButtons.Add(tabButton);
    }


    public void OnTabEnter(TabButton tabButton)
    {
        ResetTabs();

        if (selectedTab != null && tabButton == selectedTab)
            return;

        tabButton.backgroundImage.color = tabHover;

    }
    public void OnTabExit(TabButton tabButton)
    {
        ResetTabs();

    }
    public void OnTabSelected(TabButton tabButton)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }


        selectedTab = tabButton;

        selectedTab.Select();

        ResetTabs();

        tabButton.backgroundImage.color = tabSelected;
        int index = tabButton.transform.GetSiblingIndex();

        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (var button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab)
                continue;

            button.backgroundImage.color = tabIdle;
        }
    }
}
