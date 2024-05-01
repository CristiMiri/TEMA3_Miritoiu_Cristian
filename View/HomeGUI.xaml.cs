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
    /// Interaction logic for HomeGUI.xaml
    /// </summary>
    public partial class HomeGUI : Page
    {
        public HomeGUI()
        {
            InitializeComponent();
            HomeController homeController = new HomeController(this);

        }

        public TextBox GetTxtNume()
        {
            return this.txtNume;
        }

        public TextBox GetTxtEmail()
        {
            return this.txtEmail;
        }

        public TextBox GetTxtTelefon()
        {
            return this.txtTelefon;
        }

        public ComboBox GetCmbPrezentare()
        {
            return this.cmbPrezentare;
        }

        public ComboBox GetCmbSectiune()
        {
            return this.cmbSectiune;
        }

        public DataGrid GetTabelConferinte()
        {
            return this.TabelConferinte;
        }

        public Button getFilterPrezenariButton()
        {
            return this.FilterPrezenariButton;
        }

        public TextBox getPhotoPathTexBox()
        {
            return this.txtPhotoPath;
        }

        public TextBox getDocumentPathTexBox()
        {
            return this.txtDocumentPath;
        }


        //Buttons
        public Button getLoginButton()
        {
            return this.loginButton;
        }        

        public Button getBrowseDocumentButton()
        {
            return this.BrowseDocumentButton;
        }

        public Button getBrowsePhotoButton()
        {
            return this.BrowsePhotoButton;
        }

        public Button getInscriereButton()
        {
            return this.InscriereButton;
        }
    }

}
