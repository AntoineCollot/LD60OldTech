using MiniGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardProcessor : MonoBehaviour
{
    [SerializeField] float startTime;
    [SerializeField] float processTimeStep;
    [SerializeField] float processTimeStepTurbo;
    bool isUsingTurbo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MiniGameController.onError += OnMiniGameError;
    }

    private void OnDestroy()
    {
        MiniGameController.onError -= OnMiniGameError;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            ProcessCards();
        }
#endif
    }

    public void ProcessCards()
    {
        CardManager.Instance.SetMode(CardManager.ViewMode.Hide);

        List<CardData> dataToPlay = CardManager.Instance.GetCardsToPlay();

        if (dataToPlay.Count == 0)
        {
            //Wrong SFX
            return;
        }

        StartCoroutine(ProcessCardsC(dataToPlay));
    }

    IEnumerator ProcessCardsC(List<CardData> dataToPlay)
    {
        yield return new WaitForSeconds(startTime);

        for (int i = 0; i < dataToPlay.Count; i++)
        {
            yield return StartCoroutine(ProcessCard(dataToPlay[i], i == dataToPlay.Count - 1));
        }

        Debug.Log("Finished Processing!");
    }

    IEnumerator ProcessCard(CardData data, bool isLastCard)
    {
        for (int c = 0; c < CardData.COLUMN_COUNT; c++)
        {
            int columnValue = data.GetColumnValue(c);
            MiniGameInput input = new MiniGameInput(columnValue);
            Debug.Log("Processing " + columnValue);

            MiniGameController.Instance.ProcessFrame(input);

            if(input.id == 0)
            {
                yield return null;
            }
            else if (isUsingTurbo && !isLastCard)
                yield return new WaitForSeconds(processTimeStepTurbo);
            else
                yield return new WaitForSeconds(processTimeStep);
        }
    }

    private void OnMiniGameError()
    {
        //SFX & FX
        Debug.Log("Mini Game Error");
    }
}
