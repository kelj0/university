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

namespace WPFWorldCup
{
    /// <summary>
    /// Interaction logic for ChooseLanguage.xaml
    /// </summary>
    public partial class ChooseLanguage : Window
    {
        private MainWindow f;
        public ChooseLanguage(MainWindow f)
        {
            InitializeComponent();
            this.f = f;
        }

        private async void Btn_languageENG_Click(object sender, RoutedEventArgs e)
        {
            f.lng ="ENG";
            Hide();
            await f.changeLanguageToENG();
            f.initialSettingsForm.Show();
        }

        private async void Btn_languageCRO_Click(object sender, RoutedEventArgs e)
        {
            f.lng = "CRO";
            Hide();
            await f.changeLanguageToCRO();
            f.initialSettingsForm.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
