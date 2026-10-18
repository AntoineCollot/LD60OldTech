using UnityEngine;

public class CardData
{
    public string description;

    public bool[,] punches;
    public Texture2D texture { get; private set; }

    public const int DIGIT_COUNT = 5;
    public const int COLUMN_COUNT = 12;

    public bool isEmpty;

    public CardData()
    {
        punches = new bool[COLUMN_COUNT, DIGIT_COUNT];
        texture = new Texture2D(12, 5, TextureFormat.ARGB32, false);
        isEmpty = true;
        ClearTexture();
    }

    public void AddPunch(int digit, int column)
    {
        punches[column, digit] = true;
        texture.SetPixel(column, digit, Color.black);
        texture.Apply();

        isEmpty = false;
    }

    public void RemovePunch(int digit, int column)
    {
        punches[column, digit] = false;
        texture.SetPixel(column, digit, Color.white);
        texture.Apply();
    }

    public void Clear()
    {
        for (int c = 0; c < COLUMN_COUNT; c++)
        {
            for (int d = 0; d < DIGIT_COUNT; d++)
            {
                punches[c, d] = false;
            }
        }
        ClearTexture();
        isEmpty = true;
    }

    void ClearTexture()
    {
        Color[] pixels = texture.GetPixels();

        for (var i = 0; i < pixels.Length; ++i)
        {
            pixels[i] = Color.white;
        }

        texture.SetPixels(pixels);
        texture.Apply();
    }

    public void SetDescription(string description)
    {
        this.description = description;
    }

    public void HoverHole(int digit, int column)
    {
        //No hover on punched
        if (punches[column, digit])
            return;

        texture.SetPixel(column, digit, Color.red);
        texture.Apply();
    }

    public void ClearHover(int digit, int column)
    {
        //No hover on punched
        if (punches[column, digit])
            return;

        texture.SetPixel(column, digit, Color.white);
        texture.Apply();
    }

    public int GetColumnValue(int column)
    {
        int value = 0;
        for (int d = 0; d < DIGIT_COUNT; d++)
        {
            if (punches[column, d])
                value += 1 << DIGIT_COUNT - d - 1;
        }
        return value;
    }
}
