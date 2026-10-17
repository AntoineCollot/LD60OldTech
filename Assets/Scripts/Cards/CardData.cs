using UnityEngine;

public class CardData
{
    public string description;

    public bool[,] punches;
    public Texture2D texture { get; private set; }

    const int DIGIT_COUNT = 5;
    const int COLUMN_COUNT = 12;

    public CardData()
    {
        punches = new bool[COLUMN_COUNT, DIGIT_COUNT];
        texture = new Texture2D(12, 5, TextureFormat.R8, false);
        ClearTexture();
    }

    public void AddPunch(int digit, int column)
    {
        punches[column, digit] = true;
        texture.SetPixel(column, digit, Color.black);
        texture.Apply();
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
}
