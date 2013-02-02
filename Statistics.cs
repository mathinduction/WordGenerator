using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordGenerator
{
	public class Statistics
	{
		private List<char> _vowels = new List<char>();//Гласные
		private List<char> _consonants = new List<char>();//Согласные

		public Statistics()
		{
			#region Заполнение массивов гласных и согласных

			_vowels.Add('а');
			_vowels.Add('е');
			_vowels.Add('ё');
			_vowels.Add('и');
			_vowels.Add('о');
			_vowels.Add('у');
			_vowels.Add('ы');
			_vowels.Add('э');
			_vowels.Add('ю');
			_vowels.Add('я');

			_consonants.Add('б');
			_consonants.Add('в');
			_consonants.Add('г');
			_consonants.Add('д');
			_consonants.Add('ж');
			_consonants.Add('з');
			_consonants.Add('й');
			_consonants.Add('к');
			_consonants.Add('л');
			_consonants.Add('м');
			_consonants.Add('н');
			_consonants.Add('п');
			_consonants.Add('р');
			_consonants.Add('с');
			_consonants.Add('т');
			_consonants.Add('ф');
			_consonants.Add('х');
			_consonants.Add('ц');
			_consonants.Add('ч');
			_consonants.Add('ш');
			_consonants.Add('щ');
			_consonants.Add('ь');
			_consonants.Add('ъ');
			#endregion
		}

		/// <summary>
		/// Подсчитывает частоту появления слогов
		/// </summary>
		public Dictionary<string, int> MakeStatistics(List<string> words)
		{
			Dictionary<string, int> stat = new Dictionary<string, int>();
			foreach (string word in words)
			{
				List<string> syllables = SeparateSyllable(word);
				foreach (string syllable in syllables)
				{
					if (stat.ContainsKey(syllable))
					{
						stat[syllable]++;
					}
					else
					{
						stat.Add(syllable, 1);
					}
				}
			}
			return stat;
		}

		/// <summary>
		/// Разделяет слово на слога
		/// </summary>
		private List<string> SeparateSyllable (string word)
		{
			
			List<string> syllables = new List<string>();
			string s = "";
			bool hasVowel = false;
			for (int i = 0; i < word.Count(); i++ )
			{
				char c = word[i];
				if (s == "")//Начинаем записывать новый слог
				{
					s += c;
					if (_vowels.Contains(c))
						hasVowel = true;
				}
				else
				{
					if (_consonants.Contains(c) && (i + 1)<word.Count() && _consonants.Contains(word[i+1]))//Если текушая буква - согласная и следующая тоже
					{
						s += c;
					}
					else if (_vowels.Contains(c))//Если текушая буква - гласная
					{
						if (hasVowel)
						{
							syllables.Add(s);
							s = "";
						}
						s += c;
						hasVowel = true;

					}
					else if (_consonants.Contains(c) && (i + 1) < word.Count() && _vowels.Contains(word[i + 1]))//Если текушая буква - согласная,а и следующая - гласная
					{
						if (hasVowel)
						{
							syllables.Add(s);
							s = "";
						}
						s += c;
						hasVowel = false;
					}
					else if ((i + 1) == word.Count())//Если текущая буква - последняя в слове
					{
						s += c;
						syllables.Add(s);
						s = "";
					}
				}
			}
			if (s != "")
				syllables.Add(s);
			return syllables;
		}
	}
}
