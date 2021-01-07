using Dutchskull.Utilities.Extensions;
using PlayTestToolkit.Runtime.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryPointSetup : MonoBehaviour
{
    [SerializeField]
    private Toggle accept = default;
    [SerializeField]
    private Button startButton = default;

#if UNITY_EDITOR
    [SerializeField]
    private TMP_Text description = default;
    [SerializeField]
    private TMP_Text tutorialDescription = default;
    [SerializeField]
    private GameObject tutorialInput = default;
    [SerializeField]
    private GameObject dataCollectors = default;
#endif

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
        description.text = playtest.Description;
        tutorialDescription.text = playtest.TutorialDescription;

        PopulateListObject(playtest.GameInput, tutorialInput);

        PopulateListObject(playtest.DataCollectors.Collectors, dataCollectors);

        startButton.RemoveAllPresistentListener();
        startButton.onClick.AddPersistentListener(LoadPlayTest);
    }

    private void PopulateListObject<T>(IList<T> list, GameObject gameObjectList)
    {
        while (gameObjectList.transform.childCount > 1)
            DestroyImmediate(gameObjectList.transform.GetChild(1).gameObject);

        GameObject element = gameObjectList.transform.GetChild(0).gameObject;

        if (list.IsNullOrEmpty())
            return;

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
