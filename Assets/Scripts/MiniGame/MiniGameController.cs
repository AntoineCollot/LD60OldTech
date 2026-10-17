using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniGame
{
    public class MiniGameController : MonoBehaviour
    {

        public static MiniGameController Instance;
        public static event Action<MiniGameInput> onMiniGameUpdate;
        public static int frame;
        bool hasUsedInputThisFrame;

        MiniGameCommand currentKeyboardCommand;

        void Start()
        {
            Instance = this;
            RestartMiniGame();
        }

        void Update()
        {
            //Grab command from keyboard
            MiniGameCommand newKeyboardCommand = GetCommandFromKeyboard();
            if (newKeyboardCommand != MiniGameCommand.None)
                currentKeyboardCommand = newKeyboardCommand;

            if (currentKeyboardCommand!=MiniGameCommand.None)
            {
                MiniGameInput input = new MiniGameInput((int)currentKeyboardCommand);
                ProcessFrame(input);

                //Use the current keyboard command so reset it to none at the end of the frame
                currentKeyboardCommand = MiniGameCommand.None;
            }
        }

        void ProcessFrame(MiniGameInput input)
        {
            frame++;
            hasUsedInputThisFrame = false;

            onMiniGameUpdate?.Invoke(input);
        }

        void RestartMiniGame()
        {
            frame = 0;
        }

        public void ThrowError()
        {
            //Todo
        }

        public void MarkInputAsUsed()
        {
            hasUsedInputThisFrame = true;
        }

        MiniGameCommand GetCommandFromKeyboard()
        {
#if !UNITY_EDITOR
        return MiniGameCommand.None;
#endif

            //Move
            if (Keyboard.current.wKey.wasPressedThisFrame)
                return MiniGameCommand.MoveUp;
            if (Keyboard.current.dKey.wasPressedThisFrame)
                return MiniGameCommand.MoveRight;
            if (Keyboard.current.sKey.wasPressedThisFrame)
                return MiniGameCommand.MoveDown;
            if (Keyboard.current.aKey.wasPressedThisFrame)
                return MiniGameCommand.MoveLeft;

            //Talk
            if (Keyboard.current.tKey.wasPressedThisFrame)
                return MiniGameCommand.Talk;
            if (Keyboard.current.lKey.wasPressedThisFrame)
                return MiniGameCommand.Leave;

            return MiniGameCommand.None;
        }
    }
}
