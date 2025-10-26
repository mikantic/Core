using UnityEngine;

namespace Core.Languages
{
    [CreateAssetMenu(fileName = "Roli", menuName = "Core/Languages/Roli")]
    public class Roli : Language
    {
        public override AudioClip GetAudio(Sound sound)
        {
            int index = 0;
            if (sound.Consonant == 'R') index += 2;
            if (sound.Vowel == 'I') index += 1;
            return _audio[index];
        }
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
                    return 'R';
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
                    return 'L';
            }
        }

        public override char TranslateVowel(char vowel)
        {
            switch (vowel)
            {
                case 'A':
                case 'O':
                case 'U':
                    return 'O';
                default:
                    return 'I';
            }
        }
    }
}