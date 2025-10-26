using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Languages
{
    public struct Sound
    {
        public char Consonant { get; set; }
        public char Vowel { get; set; }
        public Sound(char consonant, char vowel)
        {
            Consonant = consonant;
            Vowel = vowel;
        }
        public override string ToString()
        {
            return $"{Consonant}{Vowel}";
        }
    }
    
    public abstract class Language : ScriptableObject
    {
        [SerializeField] protected AudioClip[] _audio;
        public abstract AudioClip GetAudio(Sound sound);

        public string Translate(string word)
        {
            return string.Join(" ", word.GetSounds().Select(section => string.Join("", section.Select(sound => Translate(sound)))));
        }
        public virtual Sound Translate(Sound sound)
        {
            sound.Consonant = TranslateConsonant(sound.Consonant);
            sound.Vowel = TranslateVowel(sound.Vowel);
            return sound;
        }
        public abstract char TranslateConsonant(char consonant);
        public virtual char TranslateVowel(char vowel)
        {
            switch (vowel)
            {
                case 'A':
                case 'O':
                case 'U':
                    return 'A';
                default:
                    return 'I';
            }
        }
    }

    public static class LanguageHelpers
    {
        public static bool IsVowel(this char letter)
        {
            switch (letter)
            {
                case 'A':
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                case 'Y':
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsConsonant(this char letter)
        {
            switch (letter)
            {
                case 'B':
                case 'C':
                case 'D':
                case 'F':
                case 'G':
                case 'H':
                case 'J':
                case 'K':
                case 'L':
                case 'M':
                case 'N':
                case 'P':
                case 'Q':
                case 'R':
                case 'S':
                case 'T':
                case 'V':
                case 'W':
                case 'X':
                case 'Z':
                    return true;
                default:
                    return false;
            }
        }

        public static List<List<Sound>> GetSounds(this string word)
        {
            List<List<Sound>> sounds = new();
            bool inSound = false;
            word = word.ToUpper();
            List<Sound> section = new();
            for (int i = 0; i < word.Length; i++)
            {
                char letter = word[i];

                if (letter.IsVowel())
                {
                    if (!inSound) continue;
                    Sound sound = new Sound(word[i - 1], letter);
                    section.Add(sound);
                    inSound = false;
                }
                else if (letter.IsConsonant())
                {
                    if (inSound) continue;
                    inSound = true;
                }
                else
                {
                    if (section.Count > 0) sounds.Add(section);
                    section = new();
                    inSound = false;
                }
            }
            if (section.Count > 0) sounds.Add(section);
            return sounds;
        }
    }
}