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
using System.Windows.Shapes;

namespace WordGenerator
{
	/// <summary>
	/// Interaction logic for GeneratorWindow.xaml
	/// </summary>
	public partial class GeneratorWindow : Window
	{
		private Generator _generator;

		public GeneratorWindow(Generator generator)
		{
			InitializeComponent();
			_generator = generator;
		}

		private void buttonNext_Click(object sender, RoutedEventArgs e)
		{
			if (textBoxN.Text == "")
			{
				MessageBox.Show("Не введено число слогов в генерируемом слове!", "Ошибка!");
				return;
			}
			int n;
			bool res = int.TryParse(textBoxN.Text, out n);
			if (!res || n > 10)
			{
				MessageBox.Show("Введёное число слогов не корректно!", "Ошибка!");
				return;
			}

			string newWord = _generator.Generate(n);
			listBoxWords.Items.Insert(0, newWord);
		}
	}
}
