using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace BindingTest
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string DATA_FILENAME = "zamestnanci.json";

        private List<Zamestnanec> zamestnanci = new List<Zamestnanec>();

        public MainWindow()
        {
            InitializeComponent();
            if (!File.Exists(DATA_FILENAME))
                File.Create(DATA_FILENAME);
            else
                zamestnanci = JsonSerializer.Deserialize<List<Zamestnanec>>(File.ReadAllText(DATA_FILENAME));

            this.DataContext = new Zamestnanec();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            zamestnanci.Add(DataContext as Zamestnanec);
            DataContext = new Zamestnanec();
            SaveToFile();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            SaveToFile();
        }

        private void SaveToFile()
        {
            File.WriteAllText(DATA_FILENAME, JsonSerializer.Serialize(zamestnanci));
        }
    }

    public class Zamestnanec : Osoba
    {
        private string vzdelani;
        private string pozice;
        private decimal plat;

        public decimal Plat
        {
            get { return plat; }
            set { plat = value; RaisePropertyChanged("Plat"); }
        }


        public string Pozice
        {
            get { return pozice; }
            set { pozice = value; RaisePropertyChanged("Pozice"); }
        }


        public string Vzdelani
        {
            get { return vzdelani; }
            set { vzdelani = value; RaisePropertyChanged("Vzdelani"); }
        }

    }

    public class Osoba : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string jmeno;
        private string prijmeni;
        private int rokNarozeni;

        public int RokNarozeni
        {
            get { return rokNarozeni; }
            set { rokNarozeni = value; RaisePropertyChanged("RokNarozeni"); }
        }


        public string Prijmeni
        {
            get { return prijmeni; }
            set { prijmeni = value; RaisePropertyChanged("Prijmeni"); }
        }


        public string Jmeno
        {
            get { return jmeno; }
            set { jmeno = value; RaisePropertyChanged("Jmeno"); }
        }


        protected void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
