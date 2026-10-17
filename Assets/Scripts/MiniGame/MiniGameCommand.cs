using System;

namespace MiniGame
{
    public enum MiniGameCommand
    {
        None,
        MoveUp,
        MoveRight,
        MoveDown,
        MoveLeft,
        Talk,
        Leave,
    }

    [Serializable]
    public class MiniGameInput
    {
        public MiniGameInput(int id)
        {
            this.id = id;
            if (Enum.IsDefined(typeof(MiniGameCommand), id))
                command = (MiniGameCommand)id;
            else
                command = MiniGameCommand.None;
            letter = IdToLetter(id);
        }

        public int id;
        public MiniGameCommand command;
        public char letter;

        public static char IdToLetter(int id)
        {
            int asciiStart = (int)'A';
            return (char)(asciiStart + id);
        }
    }
}
