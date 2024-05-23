using PS_TEMA3.Controller;
using PS_TEMA3.Model;
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
        private HomeController homeController;
        public HomeGUI()
        {
            InitializeComponent();
            homeController = new HomeController(this);

        }

        //Settable click event to return to the controller
        

        //Inscriere fields
        //TextBox
        public TextBox GetNameTextBox()
        {
            return this.NameTextBox;
        }        
        public TextBox GetEmailTextBox()
        {
            return this.EmailTextBox;
        }
        public TextBox GetPhoneTextBox()
        {
            return this.PhoneTextBox;
        }
        
        public TextBox GetPinTextBox()
        {
            return this.PinTextBox;
        }

        public TextBox GetPhotoPathTexBox()
        {
            return this.PhotoPathTextBox;
        }
        public TextBox GetDocumentPathTexBox()
        {
            return this.DocumentPathTextBox;
        }
        

        //ComboBox
        public ComboBox GetTypeComboBox()
        {
            return this.AuthorComboBox;
        }

        //Participant fields
        public Label GetPresentationLabel()
        {
            return this.PresentationLabel;
        }
        public ComboBox GetAttendPresentationComboBox()
        {
            return this.AttendPresentationComboBox;
        }


        //Autor fields        
        //TextBox
        public TextBox GetTitleTextBox()
        {
            return this.TitleTextBox;
        }
        public TextBox GetDescriptionTextBox()
        {
            return this.DescriptionTextBox;
        }
        public TextBox GetTimeTextBox()
        {
            return this.TimeTextBox;
        }

        //DatePicker
        public DatePicker GetDataDatePicker()
        {
            return this.PresentationDatePicker;
        }

        //ComboBox
        public ComboBox GetSectionComboBox()
        {
            return this.SectionComboBox;
        }
        
        //Labels
        public Label GetTitleLabel()
        {
            return this.TitleLabel;
        }
        public Label GetDescriptionLabel()
        {
            return this.DescriptionLabel;
        }
        public Label GetTimeLabel()
        {
            return this.TimeLabel;
        }
        public Label GetDateLabel()
        {
            return this.DateLabel;
        }
        public Label GetSectionLabel()
        {
            return this.SectionLabel;
        }


        //Buttons
        public Button getBrowseDocumentButton()
        {
            return this.BrowseDocumentButton;
        }
        public Button getBrowsePhotoButton()
        {
            return this.BrowsePhotoButton;
        }
        public Button getSingUpButton()
        {
            return this.SingUpButton;
        }

        public Button getRomanianButton()
        {
            return this.RomanianButton;
        }

        public Button getEnglishButton()
        {
            return this.EnglishButton;
        }

        //Filter fields
        //ComboBox
        public ComboBox GetFilterPresentationComboBox()
        {
            return this.FilterPresentationComboBox;
        }
        //Buttons
        public Button GetFilterPresentationButton()
        {
            return this.FilterPresentationButton;
        }


        //Tabel fields
        public DataGrid GetTabelConferinte()
        {
            return this.TabelConferinte;
        }


        //Auxiliary fields
        public Button getLoginButton()
        {
            return this.loginButton;
        }        


        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void DownloadLink_Click(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            string pdfFilePath = e.Uri.ToString();
            homeController.DownloadLink_Click(pdfFilePath);
            e.Handled = true;            
                      
        }
    }

}
