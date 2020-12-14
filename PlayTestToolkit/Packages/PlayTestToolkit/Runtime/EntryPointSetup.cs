using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryPointSetup : MonoBehaviour
{
#if UNITY_EDITOR
    public TMP_Text description;
    public TMP_Text tutorialDescription;
    public GameObject tutorialInput;
    public GameObject dataCollectors;
#endif

    public Toggle accept;
    public Button startButton;

#if UNITY_EDITOR
    public PlayTest test;

    [ContextMenu("test")]
    public void fasdfasdft()
    {
        Init(test);
    }

    public void Init(PlayTest playtest)
    {
        description.text = playtest.description;
        tutorialDescription.text = playtest.tutorialDescription;

        PopulateListObject(playtest.input, tutorialInput);

        PopulateListObject(playtest.dataCollectors, dataCollectors);

        startButton.RemoveAllPresistentListener();
        startButton.AddPersistentListener(LoadPlayTest);
    }
#endif

    public void LoadPlayTest()
    {
        if (accept.isOn)
            SceneManager.LoadScene(1);
        else
            accept.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

#if UNITY_EDITOR
    private void PopulateListObject<T>(List<T> list, GameObject gameObjectList)
    {
        while (gameObjectList.transform.childCount > 1)
            DestroyImmediate(gameObjectList.transform.GetChild(1).gameObject);

        GameObject element = gameObjectList.transform.GetChild(0).gameObject;

        SetText(list[0], element);

        for (int i = 1; i < list.Count; i++)
        {
            GameObject instance = Instantiate(element);
            instance.transform.SetParent(gameObjectList.transform);
            instance.transform.localScale = Vector3.one;
            SetText(list[i], instance);
        }
    }

    private static void SetText<T>(T element, GameObject instance)
    {
        TMP_Text text = instance.GetComponentInChildren<TMP_Text>();

        text.text = element.ToString();
    }
#endif
}
