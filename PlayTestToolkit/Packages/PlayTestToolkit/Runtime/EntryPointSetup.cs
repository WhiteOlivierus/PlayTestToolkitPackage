using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryPointSetup : MonoBehaviour
{
    [SerializeField]
    private PlayTest test = default;

    [SerializeField]
    private Toggle accept = default;

#if UNITY_EDITOR
    [SerializeField]
    private Button startButton = default;
    [SerializeField]
    private TMP_Text description = default;
    [SerializeField]
    private TMP_Text tutorialDescription = default;
    [SerializeField]
    private GameObject tutorialInput = default;
    [SerializeField]
    private GameObject dataCollectors = default;
#endif

    [ContextMenu("test")]
    public void Test()
    {
        Init(test);
    }

    public void LoadPlayTest()
    {
        if (accept.isOn)
            SceneManager.LoadScene(1);
        else
            accept.GetComponentInChildren<TMP_Text>().color = Color.red;
    }

#if UNITY_EDITOR

    public void Init(PlayTest playtest)
    {
        description.text = Empty(playtest.Description);
        tutorialDescription.text = Empty(playtest.TutorialDescription);

        PopulateListObject(playtest.GameInput, tutorialInput);

        var recorders = playtest.Recorders.Where((s) => s.Active).ToList();
        PopulateListObject(recorders, dataCollectors);

        startButton.RemoveAllPresistentListener();
        startButton.onClick.AddPersistentListener(LoadPlayTest);
    }

    private static string Empty(string value) => string.IsNullOrEmpty(value) ? "" : value;

    private void PopulateListObject<T>(IList<T> list, GameObject gameObjectList)
    {
        while (gameObjectList.transform.childCount > 1)
            DestroyImmediate(gameObjectList.transform.GetChild(1).gameObject);

        GameObject element = gameObjectList.transform.GetChild(0).gameObject;

        if (list.IsNullOrEmpty())
        {
            SetText("", element);
            return;
        }

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

        text.text = Empty(element.ToString());
    }
#endif
}
