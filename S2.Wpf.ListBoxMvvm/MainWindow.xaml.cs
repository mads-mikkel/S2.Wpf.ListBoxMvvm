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

namespace S2.Wpf.ListBoxMvvm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        private ViewModel viewModel;
        private List<Control> controlsWithToggleableBorders;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;

            controlsWithToggleableBorders = new List<Control>() { textBoxFirstname, textBoxLastname, textBoxYearlySalary, datePickerHireDate };
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void RemoveBorderAround(List<Control> controls)
        {
            if(controls.Count() > 0)
            {
                foreach(Control control in controls)
                {
                    control.BorderThickness = new Thickness(0);
                }
            }
        }

        private void EnableBorderAround(List<Control> controls)
        {
            if(controls.Count() > 0)
            {
                foreach(Control control in controls)
                {
                    control.BorderThickness = new Thickness(1);
                }
            }
        }

        private void ListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveBorderAround(controlsWithToggleableBorders);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            EnableBorderAround(controlsWithToggleableBorders);
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            EnableBorderAround(controlsWithToggleableBorders);
        }
    }
}
