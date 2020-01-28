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
            Disable(buttonSave);
        }

        private void Enable(params Control[] controls)
        {
            if(controls.Count() > 0)
            {
                foreach(Control control in controls)
                {
                    control.IsEnabled = true;
                }
            }
        }

        private void Disable(params Control[] controls)
        {
            if(controls.Count() > 0)
            {
                foreach(Control control in controls)
                {
                    control.IsEnabled = false;
                }
            }
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

        private void Clear(params Control[] controls)
        {
            foreach(Control control in controls)
            {
                if(control is TextBox textBox)
                {
                    textBox.Text = String.Empty;
                }
                else if(control is DatePicker datePicker)
                {
                    datePicker.SelectedDate = DateTime.Now.Date;
                }
            }
        }

        private void ListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveBorderAround(controlsWithToggleableBorders);
            Enable(buttonEdit);
            Disable(buttonSave);
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            EnableBorderAround(controlsWithToggleableBorders);
            Enable(buttonSave);
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            listBox.UnselectAll();
            Clear(textBoxFirstname, textBoxLastname, textBoxYearlySalary, datePickerHireDate);
            EnableBorderAround(controlsWithToggleableBorders);
            Enable(buttonSave);
            Disable(buttonEdit);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string firstnameInput = textBoxFirstname.Text;
            string lastnameInput = textBoxFirstname.Text;
            DateTime? hireDateInput = datePickerHireDate.SelectedDate;
            string yearlySalaryInput = textBoxYearlySalary.Text;

            

            (bool isValid, string message) validationResult = viewModel.TryAddEmployee(firstnameInput, lastnameInput, hireDateInput, yearlySalaryInput);
            if(!validationResult.isValid)
            {
                MessageBox.Show(validationResult.message, "Input fejl", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {

            }
        }
    }
}
