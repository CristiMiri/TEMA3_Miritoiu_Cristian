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
        private LoginGUI loginGUI;
        private UtilizatorRepository utilizatorRepository;

        public LoginController(LoginGUI loginGUI)
        {
            this.loginGUI = loginGUI;
            utilizatorRepository = new UtilizatorRepository();

            //Buttons
            loginGUI.GetLoginButton().Click += new RoutedEventHandler(Login);
            loginGUI.GetBackButton().Click += new RoutedEventHandler(Back);
        }

        private Utilizator? ValidUtilizatorData()
        {
            string email = loginGUI.GetEmailTextBox().Text;
            string password = loginGUI.GetPasswordTextBox().Text;
            if (String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
            {
                loginGUI.ShowError("Email and password are required!");
                return null;
            }
            return new Utilizator(email, password);
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilizator utilizator = ValidUtilizatorData();
                if (utilizator != null)
                {
                    utilizator = utilizatorRepository.ReadUtilizatorbyEmailandParola(utilizator.Email, utilizator.Parola);
                    switch (utilizator.UserType)
                    {
                        case UserType.ADMINISTRATOR:
                            //Navigate to AdminGUI
                            showPage(new AdminGUI());
                            break;
                        case UserType.PARTICIPANT:
                            //Navigate to UserGUI
                            showPage(new UtilizatorGUI());
                            break;
                        case UserType.ORGANIZATOR:
                            showPage(new OrganizatorGUI());
                            break;
                        default:
                            loginGUI.ShowError("Invalid email or password!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                loginGUI.ShowError("Invalid email or password!");
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            showPage(new HomeGUI());
        }
        private void showPage(Page page)
        {
            Application.Current.MainWindow.Content = page;
        }

    }
}
