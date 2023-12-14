using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Etlap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		EtlapService etlapService;
		
		public MainWindow(EtlapService etlapService)
		{
			InitializeComponent();
			this.etlapService = etlapService;
			Read();
		}

		private void Create_Click(object sender, RoutedEventArgs e)
		{
			EtlapForm form = new EtlapForm(etlapService);
			form.Closed += (_, _) =>
			{
				Read();
			};
			form.ShowDialog();
		}

		private void pricePercentClick(object sender, RoutedEventArgs e)
		{
			Etlap selected = menuTable.SelectedItem as Etlap;
			if (selected == null)
			{
				double multiplier = int.Parse(pricePercent.Text);
				multiplier = (multiplier / 100) + 1;
				etlapService.UpdateAllMultiply(multiplier);
				Read();
			}
			else
			{
				double multiplier = int.Parse(pricePercent.Text);
				multiplier = (multiplier / 100) + 1;
				etlapService.UpdateOneMultiply(multiplier, selected.Id);
				Read();
			}
		}

		private void priceHUFClick(object sender, RoutedEventArgs e)
		{
			Etlap selected = menuTable.SelectedItem as Etlap;
			if (selected == null)
			{
				int value = int.Parse(pricePercent.Text);
				etlapService.UpdateAllAdd(value);
				Read();
			}
			else
			{
				int value = int.Parse(pricePercent.Text);
				etlapService.UpdateOneMultiply(value, selected.Id);
				Read();
			}
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			Etlap selected = menuTable.SelectedItem as Etlap;
			if (selected == null)
			{
				MessageBox.Show("Törléshez előbb válasszon ki menüt!");
				return;
			}
			MessageBoxResult selectedButton =
				MessageBox.Show($"Biztos, hogy törölni szeretné az alábbi menüt: {selected.Name}?",
					"Biztos?", MessageBoxButton.YesNo);
			if (selectedButton == MessageBoxResult.Yes)
			{
				if (etlapService.Delete(selected.Id))
				{
					MessageBox.Show("Sikeres törlés!");
				}
				else
				{
					MessageBox.Show("Hiba történt a törlés során, a megadott elem nem található");
				}
				Read();
			}
		}


		private void Read()
		{
			menuTable.ItemsSource = etlapService.GetEtlap();
		}

		
	}
}
