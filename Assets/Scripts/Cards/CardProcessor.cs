using MiniGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardProcessor : MonoBehaviour
{
    [SerializeField] float startTime;
    [SerializeField] float processTimeStep;
    [SerializeField] float processTimeStepDialogue;
    [SerializeField] float processTimeStepTurbo;
    bool IsUsingTurbo => turboButton.isOn;
    public bool isRunning { get; private set; }
    public bool isPaused { get; private set; }
    public bool isFinishedButRunning { get; private set; }

    [Header("Buttons")]
    [SerializeField] MachineButton runButton;
    [SerializeField] MachineButton pauseButton;
    [SerializeField] MachineButton turboButton;
    [SerializeField] MachineLED errorLed;

    //Audio
    AudioSource source;

    public static CardProcessor Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        MiniGameController.onError += OnMiniGameError;
        pauseButton.onValueChanged += OnPauseButton;
        runButton.onValueChanged += OnRunButton;

        errorLed.SetOn(false);
    }

    private void OnDestroy()
    {
        if (runButton != null)
            runButton.onValueChanged -= OnRunButton;
        if (pauseButton != null)
            pauseButton.onValueChanged -= OnPauseButton;
        MiniGameController.onError -= OnMiniGameError;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Run();
        }
#endif
    }

    public bool Run()
    {
        CardManager.Instance.SetMode(CardManager.ViewMode.Hide, true);

        List<CardData> dataToPlay = CardManager.Instance.GetCardsToPlay();

        if (dataToPlay.Count == 0)
        {
            SFXManager.PlaySound(GlobalSFX.MachineNoCard);
            return false;
        }

        pauseButton.SetOn(false);
        MiniGameController.Instance.ResetGame();
        SFXManager.PlaySound(GlobalSFX.ScreenOn);

        source.Play();
        MusicManager.Instance.PlayMusic();
        StopAllCoroutines();
        StartCoroutine(ProcessCardsC(dataToPlay));

        return true;
    }

    IEnumerator ProcessCardsC(List<CardData> dataToPlay)
    {
        isFinishedButRunning = false;
        isRunning = true;
        yield return new WaitForSeconds(startTime);

        CardManager.Instance.SetProcessingCardPosition();

        for (int i = 0; i < dataToPlay.Count; i++)
        {
            yield return StartCoroutine(ProcessCard(dataToPlay[i], i == dataToPlay.Count - 1));
        }

        isFinishedButRunning = true;
        source.Stop();

        //yield return new WaitForSeconds(1);

        ////CardManager.Instance.SetMode(CardManager.ViewMode.Hide, true);
        //runButton.SetOn(false);
        //isRunning = false;
        //Debug.Log("Finished Processing!");
    }

    IEnumerator ProcessCard(CardData data, bool isLastCard)
    {
        SFXManager.PlaySound(GlobalSFX.MachineCardProcessedStarted);
        bool hasData = false;

        for (int c = 0; c < CardData.COLUMN_COUNT; c++)
        {
            errorLed.SetOn(false);

            int columnValue = data.GetColumnValue(c);
            MiniGameInput input = new MiniGameInput(columnValue);
            Debug.Log("Processing " + columnValue);

            MiniGameController.Instance.ProcessFrame(input);

            hasData |= (input.id != 0);

            if (input.id == 0)
            {
                yield return null; //Skip 0 commands
            }
            else if (IsUsingTurbo)// && !isLastCard) //Turbo only for other cards ?
                yield return new WaitForSeconds(processTimeStepTurbo);
            else if (input.command == MiniGameCommand.Talk)
                yield return new WaitForSeconds(processTimeStepDialogue);
            else
                yield return new WaitForSeconds(processTimeStep);

            //Pause & end of game
            while (isPaused || MiniGameEgg.Instance.hasBeenFound)
            {
                yield return null;
                continue;
            }
        }

        CardManager.Instance.SetProcessedCardPosition(data); //Set card position after processing

        if (hasData)
            SFXManager.PlaySound(GlobalSFX.MachineCardProcessedFinished);
    }

    private void OnMiniGameError()
    {
        //SFX & FX
        errorLed.SetOn(true);
        Debug.Log("Mini Game Error");
    }

    public void StopRun()
    {
        if (isRunning)
            SFXManager.PlaySound(GlobalSFX.ScreenOf);

        CardManager.Instance.SetMode(CardManager.ViewMode.Hide, true);
        MusicManager.Instance.StopMusic();
        isRunning = false;
        source.Stop();
        isFinishedButRunning = false;

        // CardManager.Instance.SetMode(CardManager.ViewMode.Hide, true);
        StopAllCoroutines();
    }

    private void OnRunButton(bool isOn)
    {
        if (isOn)
        {
            bool canRun = Run();
            if (!canRun)
                Invoke("ResetRunButtonDelayed", 0.1f);
        }
        else
            StopRun();
    }

    void ResetRunButtonDelayed()
    {
        runButton.SetOn(false);
    }

    private void OnPauseButton(bool isOn)
    {
        isPaused = isOn;

        if (isRunning && !isFinishedButRunning && !isPaused)
        {
            source.Play();
        }
        if (isPaused)
        {
            source.Stop();
        }
    }
}
