using System;

namespace MiniGame
{
    public enum MiniGameCommand
    {
        None = 0,
        MoveUp = 1,
        MoveRight = 2,
        MoveDown =3,
        MoveLeft = 4,
        Talk = 27,
        Leave = 28,
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
