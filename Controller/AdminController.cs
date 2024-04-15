using PS_TEMA3.Model;
using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PS_TEMA3.Controller
{
    internal class AdminController
    {
        private UtilizatorRepository utilizatorRepository;
        private AdminGUI adminGUI;

        public AdminController(AdminGUI adminGUI)
        {
            utilizatorRepository = new UtilizatorRepository();
            this.adminGUI = adminGUI;
            adminGUI.GetCreateUserButton().Click += new RoutedEventHandler(CreateUtilizator);
            adminGUI.GetUpdateUserButton().Click += new RoutedEventHandler(UpdateUtilizator); 
            adminGUI.getDeleteUserButton().Click += new RoutedEventHandler(DeleteUtilizator);
            adminGUI.GetBackButton().Click += new RoutedEventHandler(Back);
            adminGUI.GetUsersDataGrid().SelectionChanged += new SelectionChangedEventHandler(SelectUtilizator);
            adminGUI.GetUserTypeComboBox().ItemsSource = Enum.GetValues(typeof(UserType));
            adminGUI.GetUserTypeComboBox().SelectedIndex = 0;
            UtilizatoriTable();            
        }

        private Utilizator ValidUtilizatorData()
        {           
            int id;
            if (!int.TryParse(adminGUI.GetIdTextBox().Text, out id))
            {
                id = 0;               
            }
            String nume = adminGUI.GetNameTextBox().Text;
            String email = adminGUI.GetEmailTextBox().Text;
            String parola = adminGUI.GetPasswordTextBox().Text;
            UserType userType = (UserType)adminGUI.GetUserTypeComboBox().SelectedIndex;
            String telefon = adminGUI.GetTelephoneTextBox().Text;
            if(String.IsNullOrEmpty(nume) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(parola) || String.IsNullOrEmpty(telefon))
            {
                adminGUI.ShowMessage("Toate campurile sunt obligatorii!");
                return null;
            }
            return new Utilizator(id,nume, email, parola, userType, telefon);
        }
        private void ClearFields()
        {
            adminGUI.GetIdTextBox().Text = "";
            adminGUI.GetNameTextBox().Text = "";
            adminGUI.GetEmailTextBox().Text = "";
            adminGUI.GetPasswordTextBox().Text = "";
            adminGUI.GetUserTypeComboBox().SelectedIndex = 0;
            adminGUI.GetTelephoneTextBox().Text = "";
        }

        private void CreateUtilizator(object sender, EventArgs e)
        {
           Utilizator createUser = ValidUtilizatorData();
            if (createUser != null)
            {
                utilizatorRepository.addUtilizator(createUser);
                adminGUI.ShowMessage("Utilizatorul a fost adaugat cu succes!");
            }
            this.UtilizatoriTable();
            this.ClearFields();
        }

        private void UpdateUtilizator(object sender, EventArgs e)
        {
            Utilizator updateUser = ValidUtilizatorData();
            if (updateUser != null)
            {
                utilizatorRepository.updateUtilizator(updateUser);
                adminGUI.ShowMessage("Utilizatorul a fost actualizat cu succes!");
            }
            this.UtilizatoriTable();
            this.ClearFields();
        }

        private void DeleteUtilizator(object sender, EventArgs e)
        {
            Utilizator deleteUser = ValidUtilizatorData();
            if (deleteUser != null)
            {
                utilizatorRepository.deleteUtilizator(deleteUser.Id);
                adminGUI.ShowMessage("Utilizatorul a fost sters cu succes!");
            }
            this.UtilizatoriTable();
            this.ClearFields();
        }

        private void SelectUtilizator(object sender, EventArgs e)
        {
            Utilizator utilizator = (Utilizator)adminGUI.GetUsersDataGrid().SelectedItem;
            if (utilizator != null)
            {
                adminGUI.GetIdTextBox().Text = utilizator.Id.ToString();
                adminGUI.GetNameTextBox().Text = utilizator.Nume;
                adminGUI.GetEmailTextBox().Text = utilizator.Email;
                adminGUI.GetPasswordTextBox().Text = utilizator.Parola;
                adminGUI.GetUserTypeComboBox().SelectedIndex = (int)utilizator.UserType;
                adminGUI.GetTelephoneTextBox().Text = utilizator.Telefon;
            }
        }

        private void UtilizatoriTable()
        {
            List<Utilizator> utilizatori = utilizatorRepository.GetUtilizatori();
            adminGUI.GetUsersDataGrid().ItemsSource = utilizatori;
        }

        private void Back(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Content = new HomeGUI();
        }
    }
}
