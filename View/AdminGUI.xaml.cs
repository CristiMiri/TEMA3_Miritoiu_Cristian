using PS_TEMA3.Controller;
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

namespace PS_TEMA3.View
{
    /// <summary>
    /// Interaction logic for AdminGUI.xaml
    /// </summary>
    public partial class AdminGUI : Page
    {
        public AdminGUI()
        {
            InitializeComponent();
            AdminController adminController = new AdminController(this);
        }

        // Getter for the ID TextBox
        public TextBox GetIdTextBox()
        {
            return this.IdTextBox;
        }

        // Getter for the Name TextBox
        public TextBox GetNameTextBox()
        {
            return this.NumeTextBox;
        }

        // Getter for the Email TextBox
        public TextBox GetEmailTextBox()
        {
            return this.EmailTextBox;
        }

        // Getter for the Password TextBox
        public TextBox GetPasswordTextBox()
        {
            return this.ParolaTextBox;
        }

        // Getter for the User Type ComboBox
        public ComboBox GetUserTypeComboBox()
        {
            return this.UserTypeComboBox;
        }

        // Getter for the Telephone TextBox
        public TextBox GetTelephoneTextBox()
        {
            return this.TelefonTextBox;
        }

        // Getter for the DataGrid
        public DataGrid GetUsersDataGrid()
        {
            return this.TabelUtilizatori;
        }

        //Getter for the Create User Button
        public Button GetCreateUserButton()
        {
            return this.CreateUserButton;
        }

        //Getter for the Update User Button
        public Button GetUpdateUserButton()
        {
            return this.UpdateUserButton;
        }

        //Getter for the Route Back Button
        public Button GetBackButton()
        {
            return this.BackButton;
            //TODO: Implement the BackButton
        }
        public Button getDeleteUserButton()
        {
            return this.DeleteUserButton;
        }

        public void ShowMessage(String message)
        {
            MessageBox.Show(message);
        }        
    }

}
