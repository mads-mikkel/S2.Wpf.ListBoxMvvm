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
        private bool isEditing;
        private bool isAdding;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;
            isEditing = isAdding = false;

            controlsWithToggleableBorders = new List<Control>() { textBoxFirstname, textBoxLastname, textBoxYearlySalary, datePickerHireDate };
            RemoveBorderAround(controlsWithToggleableBorders);
            Disable(buttonSave, buttonEdit);
            ToggleReadonlyFor(true, textBoxFirstname, textBoxLastname, textBoxYearlySalary);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Disable(buttonSave);
            ToggleReadonlyFor(true, textBoxFirstname, textBoxLastname, textBoxYearlySalary);
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
            ToggleReadonlyFor(false, textBoxFirstname, textBoxLastname, textBoxYearlySalary);
            Enable(buttonSave);
            isEditing = true;
            isAdding = false;
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            listBox.UnselectAll();
            Clear(textBoxFirstname, textBoxLastname, textBoxYearlySalary, datePickerHireDate);
            EnableBorderAround(controlsWithToggleableBorders);
            ToggleReadonlyFor(false, textBoxFirstname, textBoxLastname, textBoxYearlySalary);
            Enable(buttonSave);
            Disable(buttonEdit);
            isAdding = true;
            isEditing = false;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string firstnameInput = textBoxFirstname.Text;
            string lastnameInput = textBoxLastname.Text;
            DateTime? hireDateInput = datePickerHireDate.SelectedDate;
            string yearlySalaryInput = textBoxYearlySalary.Text;

            if(isAdding && !isEditing)
            {
                (bool isValid, string message) validationResult = viewModel.TryAddEmployee(firstnameInput, lastnameInput, hireDateInput, yearlySalaryInput);
                if(!validationResult.isValid)
                {
                    MessageBox.Show(validationResult.message, "Input fejl", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else
                {
                    Clear(textBoxFirstname, textBoxLastname, textBoxYearlySalary, datePickerHireDate);
                }
            }
            else if(isEditing && !isAdding)
            {
                (bool isValid, string message) validationResult = viewModel.TryEditEmployee(firstnameInput, lastnameInput, hireDateInput, yearlySalaryInput);
                if(!validationResult.isValid)
                {
                    MessageBox.Show(validationResult.message, "Input fejl", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                Disable(buttonEdit);
            }
        }

        private void ToggleReadonlyFor(bool isReadonly, params Control[] controls)
        {
            if(controls.Count() > 0)
            {
                foreach(Control control in controls)
                {
                    if(control is TextBox textBox)
                    {
                        textBox.IsReadOnly = isReadonly;
                    }
                    else if(control is DatePicker datePicker)
                    {
                        // TODO: figure how to make a date picker "readonly".
                    }
                }
            }
        }
    }
}
