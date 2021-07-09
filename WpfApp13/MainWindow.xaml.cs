using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfApp13.Models;

namespace WpfApp13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Food food;
        public Food Info
        {
            get { return food; }
            set { food = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Food> Foods2 { get; set; } = new ObservableCollection<Food>();
        public ObservableCollection<Food> Foods { get; set; } = new ObservableCollection<Food>
        {
            new Food
            {
                Name = "waffle",
                ImagePath = "Images/wf.jpeg",
                Price = 1.03
            },
            new Food
            {
                Name = "chocolate",
                ImagePath = "Images/Ch.jpeg",
                Price = 1
            },
            new Food
            {
                Name = "puding",
                ImagePath = "Images/puding.jpg",
                Price = 3.91
            },
            new Food
            {
                Name = "random",
                ImagePath = "Images/default.jpg",
                Price = 0.2
            },
        };
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private void StackPanel_DragEnter(object sender, DragEventArgs e)
        {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            labl1.Text = file[0];
            BitmapImage sr = new BitmapImage();
            sr.BeginInit();
            sr.UriSource = new Uri(file[0], UriKind.Absolute);
            sr.EndInit();
            nm.Source = sr;
        }

        private void StackPanel_PreviewDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            nem.Visibility = Visibility.Visible;
            pr.Visibility = Visibility.Visible;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Food fd;
            if (labl1.Text == string.Empty || labl1.Text == null)
            {
                fd = new Food()
                {
                    ImagePath = "Images/default.jpg",
                    Name = nem.Text,
                    Price = Convert.ToDouble(pr.Text)
                };
            }
            else
            {

                fd = new Food()
                {
                    ImagePath = labl1.Text,
                    Name = nem.Text,
                    Price = Convert.ToDouble(pr.Text)
                };
            }
            Foods.Add(fd);
            nem.Text = "Name";
            pr.Text = "Price";
            nem.Visibility = Visibility.Collapsed;
            pr.Visibility = Visibility.Collapsed;
            nm.Source = null;
            labl1.Text = string.Empty;
        }

        private void listb1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Food f = (sender as ListBox).SelectedItem as Food;
            nem.Visibility = Visibility.Visible;
            pr.Visibility = Visibility.Visible;
            pr.Foreground = Brushes.Black;
            nem.Foreground = Brushes.Black;
            nem.Text = f.Name;
            pr.Text = f.Price.ToString();
            Info = f;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (listb1.SelectedItem as Food).Name = nem.Text;
            (listb1.SelectedItem as Food).Price = Convert.ToDouble(pr.Text);
            if (nm != null)
            {
                (listb1.SelectedItem as Food).ImagePath = labl1.Text;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            listb1.Visibility = Visibility.Collapsed;
            listb2.Visibility = Visibility.Visible;
            foreach (var item in Foods)
            {
                if (item.Name.Contains(searchh.Text))
                {
                    Foods2.Add(item);
                }

            }
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            listb2.Visibility = Visibility.Collapsed;
            listb1.Visibility = Visibility.Visible;
            ;
        }
    }
}
