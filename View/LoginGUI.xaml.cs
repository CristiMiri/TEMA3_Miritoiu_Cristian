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
    /// Interaction logic for LoginGUI.xaml
    /// </summary>
    public partial class LoginGUI : Page
    {
        public LoginGUI()
        {
            InitializeComponent();
        }

        public TextBox GetEmailTextBox()
        {
            return this.txtEmail;
        }

        public TextBox GetPasswordTextBox()
        {
            return this.txtPassword;
        }

        // Optionally, you might want to add getters for the buttons if needed for UI testing or other operations
        public Button GetLoginButton()
        {
            return this.LoginButton;
        }

        public Button GetBackButton()
        {
            return this.BackButton;
        }
    }

}
