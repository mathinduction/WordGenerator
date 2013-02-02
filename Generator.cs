using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordGenerator
{
	public class Generator
	{
		private List<Tuple<string, int>> _syllables = new List<Tuple<string, int>>();
		private List<double> _probability = new List<double>();
		private bool _init = false;

		public Generator()
		{
			
		}

		public void Init(Dictionary<string, int> syllables)
		{
			if (syllables.Count == 0) return;
			foreach (KeyValuePair<string, int> keyValuePair in syllables)
			{
				_syllables.Add(new Tuple<string, int>(keyValuePair.Key, keyValuePair.Value));
			}
			_init = true;


			int countAll = 0;
			foreach (var keyValuePair in _syllables)
			{
				countAll += keyValuePair.Item2;
			}
			List<double> pr = new List<double>();
			foreach (var keyValuePair in _syllables)
			{
				pr.Add(keyValuePair.Item2 / (double)countAll);
			}
			_probability.Add(pr[0]);
			for(int i = 1; i < pr.Count; i++)
			{
				_probability.Add(pr[i] + _probability[i-1]);
			}
			_probability[_probability.Count - 1] = 1;
		}

		public string Generate(int n)
		{
			if (!_init) return "";

			string word = "";
			Random rand = new Random();

			for (int k = 0; k < n; k++)
			{
				double p = rand.NextDouble();

				for (int i = 1; i < _probability.Count; i++)
				{
					if (_probability[i] >= p)
					{
						word += _syllables[i].Item1;
						break;
					}
				}
			}
			return word;
		}

		public bool IsInit
		{
			get { return _init; }
		}
	}
}
