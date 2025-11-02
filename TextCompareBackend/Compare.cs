using System;

namespace TextCompareBackend;

public static class CompareService
{
    public static bool GetTextDifference(string text1, string text2, out int[][] differenceIndexes)
    {
        differenceIndexes =  new int[2][]; // Posiciones que pueden estar diferentes en cada texto (un array)
        differenceIndexes[0] = new int [text1.Length];
        differenceIndexes[1] = new int [text2.Length];
        
        var equal = AreEqual(text1, text2 );
        if (equal) return false; // No hay diferencias
        
        var sameLength = AreEqualLength(text1, text2);

        if (sameLength)
        {
            GetIndexesFromEqualLengthText(text1, text2, out var text1Indexes, out var text2Indexes );
            differenceIndexes[0] = text1Indexes;
            differenceIndexes[1] = text2Indexes;
        }
        else
        {
            GetShortAndLongText(text1, text2, out var shortText, out var longText);
        
            GetIndexesFromDifferentLengthText(
                ref shortText, 
                ref longText,
                out var shortTextIndexes, 
                out var longTextIndexes);
            
            differenceIndexes[0] = shortTextIndexes;
            differenceIndexes[1] = longTextIndexes;
        }

        return true;
    }

    private static void GetIndexesFromEqualLengthText(
        string text1, 
        string text2,
        out int[] text1Indexes,
        out int[] text2Indexes)
    {
        text1Indexes =  new int[text1.Length]; // Array con index de caracteres con diferencia para text 1
        text2Indexes =  new int[text2.Length]; // Array con index de caracteres con diferencia para text 2
        
        for (var i = 0; i < text1.Length; i++)
        {
            if (AreEqual(text1[i], text2[i])) continue;

            text1Indexes[i] = 1; // Marcamos la diferencia
            text2Indexes[i] = 1; // Marcamos la diferencia
        }
    }
    private static void GetIndexesFromDifferentLengthText(
        ref string shortText, 
        ref string longText, 
        out int[] shortTextIndexes,
        out int[] longTextIndexes)
    {
        longTextIndexes =  new int[longText.Length];
        shortTextIndexes =  new int[shortText.Length];
        
        for (var i = 0; i < longText.Length; i++)
        {
            if (i > shortText.Length - 1)
            {
                FillEmptyIndexes(longTextIndexes, i);
                break; // Cerramos el bucle porque no hay más que comparar
            }

            if (longText[i].Equals(shortText[i])) continue;

            longTextIndexes[i] = 1; // Marcamos la diferencia
            shortTextIndexes[i] = 1; // Marcamos la diferencia
        }
    }

    private static bool AreEqualLength(string text1, string text2)
    {
        return text1.Length == text2.Length;
    }
    
    private static void GetShortAndLongText(string text1, string text2, out string shortText, out string longText)
    {
        if (text1.Length >= text2.Length)
        {
            shortText = text2;
            longText = text1;
        }
        else
        {
            shortText = text1;
            longText = text2;
        }
    }
    

    private static int[] FillEmptyIndexes(int[] indexes, int indexStart)
    {
        for (var i = indexStart; i < indexes.Length; i++)
        {
            indexes[i] = 1;
        }

        return indexes;
    }
    
    private static bool AreEqual(string text1, string text2)
    {
        return text1.Equals(text2);
    }
    private static bool AreEqual(char char1, char char2)
    {
        return char1.Equals(char2);
    }
}