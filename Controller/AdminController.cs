using PS_TEMA3.Model;
using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PS_TEMA3.Controller
{
    internal class AdminController :IObserver
    {
        public ObservableCollection<User> Users { get; set; }
        private readonly Subject _subject;
        private UserRepository utilizatorRepository;
        private AdminGUI _adminGUI;

        public AdminController(AdminGUI adminGUI, Subject subject)
        {
            utilizatorRepository = new UserRepository();
            this._adminGUI = adminGUI;
            InitializeEvents();
            InitializeUserTypeComboBox();
            _adminGUI.GetUserTypeComboBox().ItemsSource = Enum.GetValues(typeof(UserType));            
            _subject = subject;
            Users = new ObservableCollection<User>(utilizatorRepository.ReadUsers());
            _subject.Attach(this);
            LoadUsersTable();
        }


        //Auxiliary methods
        public void AddUser(User user)
        {
            Users.Add(user);
            _subject.Notify();
        }
        public void UpdateUser(User user)
        {
            int index = Users.IndexOf(Users.FirstOrDefault(x => x.Id == user.Id));
            Users[index] = user;
            _subject.Notify();
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
            _subject.Notify();
        }
        public void Update()
        {
            LoadUsersTable();
        }
        private void InitializeEvents()
        {
            _adminGUI.GetCreateUserButton().Click += CreateUser;
            _adminGUI.GetUpdateUserButton().Click += UpdateUser;
            _adminGUI.getDeleteUserButton().Click += DeleteUser;
            _adminGUI.getFilterUsersButton().Click += FilterUsers;
            _adminGUI.GetBackButton().Click += Back;
            _adminGUI.GetUsersDataGrid().SelectionChanged += SelectUser;
        }
        private User ValidUserData()
        {
            if (!int.TryParse(_adminGUI.GetIdTextBox().Text, out int id))
            {
                id = 0;
            }

            string name = _adminGUI.GetNameTextBox().Text;
            string email = _adminGUI.GetEmailTextBox().Text;
            string password = _adminGUI.GetPasswordTextBox().Text;
            UserType userType = (UserType)_adminGUI.GetUserTypeComboBox().SelectedIndex;
            string phone = _adminGUI.GetTelephoneTextBox().Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phone))
            {
                _adminGUI.ShowMessage("All fields are mandatory!");
                return null;
            }

            return new User(id, name, email, password, userType, phone);
        }
        private void ClearFields()
        {
            _adminGUI.GetIdTextBox().Clear();
            _adminGUI.GetNameTextBox().Clear();
            _adminGUI.GetEmailTextBox().Clear();
            _adminGUI.GetPasswordTextBox().Clear();
            _adminGUI.GetUserTypeComboBox().SelectedIndex = 0;
            _adminGUI.GetTelephoneTextBox().Clear();
        }
        private void InitializeUserTypeComboBox()
        {
            var items = Enum.GetValues(typeof(UserType)).Cast<UserType>().Select(x => x.ToString()).ToList();
            items.Insert(3, "TOATE");
            _adminGUI.getFilterBox().ItemsSource = items;
            _adminGUI.getFilterBox().SelectedIndex = 0;

        }

        //CRUD methods
        private void CreateUser(object sender, EventArgs e)
        {
            User createUser = ValidUserData();
            if (createUser != null)
            {
                if(utilizatorRepository.CreateUser(createUser))
                    _adminGUI.ShowMessage("Utilizatorul a fost adaugat cu succes!");
                else
                    _adminGUI.ShowMessage("Nu sa putut adauga utilizatorul!");                
            }
            AddUser(createUser);
            this.ClearFields();
        }
        private void LoadUsersTable()
        {
            _adminGUI.GetUsersDataGrid().ItemsSource = Users;
        }
        private void UpdateUser(object sender, EventArgs e)
        {
            User updateUser = ValidUserData();
            if (updateUser != null)
            {
                utilizatorRepository.UpdateUser(updateUser);
                this.UpdateUser(updateUser);
                _adminGUI.ShowMessage("Utilizatorul a fost actualizat cu succes!");
            }            
            this.ClearFields();
        }
        private void DeleteUser(object sender, EventArgs e)
        {
            User deleteUser = ValidUserData();
            if (deleteUser != null)
            {
                utilizatorRepository.DeleteUser(deleteUser.Id);
                _adminGUI.ShowMessage("Utilizatorul a fost sters cu succes!");
            }
            RemoveUser(deleteUser);
            this.ClearFields();
        }
        private void SelectUser(object sender, EventArgs e)
        {
            User user = (User)_adminGUI.GetUsersDataGrid().SelectedItem;
            if (user != null)
            {
                _adminGUI.GetIdTextBox().Text = user.Id.ToString();
                _adminGUI.GetNameTextBox().Text = user.Name;
                _adminGUI.GetEmailTextBox().Text = user.Email;
                _adminGUI.GetPasswordTextBox().Text = user.Password;
                _adminGUI.GetUserTypeComboBox().SelectedIndex = (int)user.UserType;
                _adminGUI.GetTelephoneTextBox().Text = user.Phone;
            }
        }
        private void FilterUsers(object sender, EventArgs e)
        {
            int userType = _adminGUI.getFilterBox().SelectedIndex;

            if (userType == 3)
            {
                LoadUsersTable();
                return;
            }
            List<User> utilizatori = utilizatorRepository.ReadUsersByUserType((UserType)userType);
            _adminGUI.GetUsersDataGrid().ItemsSource = utilizatori;

        }

        //Navigation methods
        private void Back(object sender, EventArgs e)
        {
            //Application.Current.MainWindow.Content = new HomeGUI();
            this._adminGUI.Close();
        }

    }
}
