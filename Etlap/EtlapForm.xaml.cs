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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for EtlapForm.xaml
    /// </summary>
    public partial class EtlapForm : Window
    {
        private EtlapService etlapService;
        private Etlap etlap;
        public EtlapForm()
        {
            InitializeComponent();
        }

		public EtlapForm(EtlapService etlapService)
		{
			InitializeComponent();
			this.etlapService = etlapService;
		}

		public EtlapForm(EtlapService etlapService, Etlap etlap)
		{
			InitializeComponent();
			this.etlapService = etlapService;
			this.etlap = etlap;

			tbName.Text = etlap.Name;
			tbDesc.Text = etlap.Description;
			tbPrice.Text = etlap.Price.ToString();
		}

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Etlap etlap = CreateMenuFromInputData();
				if (etlapService.AddEtlap(etlap))
				{
					MessageBox.Show("Sikeres hozzáadás");
					tbName.Text = "";
					tbDesc.Text = "";
					tbPrice.Text = "";
					
				}
				else
				{
					MessageBox.Show("Hiba történt a hozzáadás során");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		private Etlap CreateMenuFromInputData()
		{
			string name = tbName.Text.Trim();
			string desc = tbDesc.Text.Trim();
			ComboBoxItem category = (ComboBoxItem)cbCategory.SelectedItem;
			string selectedCategory = category.Content.ToString();
			string priceText = tbPrice.Text.Trim();

			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Név megadása kötelező");
			}
			if (string.IsNullOrEmpty(desc))
			{
				throw new Exception("Leírás megadása kötelező");
			}
			if (string.IsNullOrEmpty(selectedCategory))
			{
				throw new Exception("Kategória kiválasztása kötelező");
			}
			if (!int.TryParse(priceText, out int price))
			{
				throw new Exception("Az ár csak szám lehet");
			}

			Etlap etlap = new Etlap();
			etlap.Name = name;
			etlap.Description = desc;
			etlap.Category = selectedCategory;
			etlap.Price = price;
			return etlap;
		}
	}
}
