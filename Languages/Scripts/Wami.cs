using UnityEngine;

namespace Core.Languages
{
    [CreateAssetMenu(fileName = "Wami", menuName = "Core/Languages/Wami")]
    public class Wami : Language
    {
        public override char TranslateConsonant(char consonant)
        {
            switch (consonant)
            {
                case 'B':
                case 'D':
                case 'G':
                case 'K':
                case 'M':
                case 'N':
                case 'P':
                case 'S':
                case 'T':
                case 'W':
                default:
                    return 'W';
                case 'C':
                case 'F':
                case 'H':
                case 'J':
                case 'L':
                case 'Q':
                case 'R':
                case 'V':
                case 'X':
                case 'Z':
                    return 'M';
            }
        }
    }
}