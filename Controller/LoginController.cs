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
    internal class LoginController
    {
        private readonly LoginGUI _loginGUI;
        private readonly UserRepository _userRepository;


        public LoginController(LoginGUI loginGUI)
        {
            _loginGUI = loginGUI;
            _userRepository = new UserRepository();

            InitializeEvents();
        }

        //Auxiliary methods
        private void InitializeEvents()
        {
            _loginGUI.GetLoginButton().Click += Login;
            _loginGUI.GetBackButton().Click += Back;
        }
        private User? ValidateUserData()
        {
            string email = _loginGUI.GetEmailTextBox().Text;
            string password = _loginGUI.GetPasswordTextBox().Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                _loginGUI.ShowError("Email and password are required!");
                return null;
            }

            return new User(email, password);
        }
        private void NavigateToUserPage(User user)
        {
            switch (user.UserType)
            {
                case UserType.ADMINISTRATOR:
                    ShowPage(new AdminGUI());
                    break;
                case UserType.PARTICIPANT:
                    ShowPage(new UtilizatorGUI());
                    break;
                case UserType.ORGANIZER:
                    ShowPage(new OrganizatorGUI());
                    break;
                default:
                    _loginGUI.ShowError("Invalid user type!");
                    break;
            }
        }

        //Navigation methods
        private void Login(object sender, RoutedEventArgs e)
        {
            try
            {
                User? user = ValidateUserData();
                if (user != null)
                {
                    User? authenticatedUser = _userRepository.ReadUserByEmailAndPassword(user.Email, user.Password);
                    if (authenticatedUser != null)
                    {
                        NavigateToUserPage(authenticatedUser);
                    }
                    else
                    {
                        _loginGUI.ShowError("Invalid email or password!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception if logging is set up, e.g., using a logging framework.
                _loginGUI.ShowError("An error occurred during login. Please try again.");
            }
        }        
        private void Back(object sender, RoutedEventArgs e)
        {
            ShowPage(new HomeGUI());
        }
        private void ShowPage(Page page)
        {
            Application.Current.MainWindow.Content = page;
        }

    }
}
