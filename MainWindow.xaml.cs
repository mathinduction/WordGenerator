using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace WordGenerator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<string> _words = new List<string>();
		private Statistics _statistics = new Statistics();
		private ObservableCollection<Row> _items = new ObservableCollection<Row>();
		private Generator _generator = new Generator();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (textBoxInput.Text == "")
			{
				MessageBox.Show("Не введён текст!", "Ошибка!");
				return;
			}
			_words = SeparateWords(textBoxInput.Text);
			Dictionary<string, int> stat = _statistics.MakeStatistics(_words);
			foreach (KeyValuePair<string, int> keyValuePair in stat)
			{
				_items.Add(new Row(keyValuePair.Key, keyValuePair.Value));
			}
			dataGridStatistics.ItemsSource = _items;

			_generator.Init(stat);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (!_generator.IsInit)
			{
				MessageBox.Show("Не введён текст для сбора статистики!", "Ошибка!");
				return;
			}
			GeneratorWindow gw = new GeneratorWindow(_generator);
			gw.Show();
		}
		#region Private
		/// <summary>
		/// Выделяет из текста отдельные слова и записывает их в массив (слове меньше,чем из 3х букв игнорируются)
		/// </summary>
		private List<string> SeparateWords(string text)
		{
			List<string> words = new List<string>();
			string word = "";
			foreach (char symbol in text)
			{
				if (Char.IsLetter(symbol))
				{
					word += symbol;
				}
				else
				{
					if (word.Count() > 2)
					{
						words.Add(word.ToLower());
					}
					word = "";
				}
			}
			if (word.Count() > 2)
			{
				words.Add(word.ToLower());
			}
			return words;
		}
		#endregion
	}

	public class Row
	{
		private string _syllable = "";
		private int _count = 0;

		public Row(string s, int c)
		{
			_syllable = s;
			_count = c;
		}

		public string Syllable
		{
			set { _syllable = value; }
			get { return _syllable; }
		}

		public int Count
		{
			set { _count = value; }
			get { return _count; }
		}
	}
}
