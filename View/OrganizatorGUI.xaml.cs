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
    /// Interaction logic for OrganizatorGUI.xaml
    /// </summary>
    public partial class OrganizatorGUI : Page
    {
        public OrganizatorGUI()
        {
            InitializeComponent();
            OrganizatorController organizatorController = new OrganizatorController(this);
        }


        //Presentation Management Section
        public TextBlock GetIdPresentationTextBox()
        {
            return this.IdPrezentare;
        }
        public TextBox GetTitleTextBox()
        {
            return this.TitleTextBox;
        }
        public TextBox GetAuthorTextBox()
        {
            return this.AuthorTextBox;
        }
        public TextBox GetDescritionTextBox()
        {
            return this.DescriptionTextBox;
        }
        public DatePicker GetDataDatePicker()
        {
            return this.DataDatePicker;
        }
        public TextBox GetTimeTextBox()
        {
            return this.TimeTextBox;
        }        
        public ComboBox getSectionComboBox()
        {
            return this.SectionComboBox;
        }

        public Button getCreatePresentationButton()
        {
            return this.CreatePresentationButton;
        }
        public Button getDeletePresentationButton()
        {
            return this.DeletePresentationButton;
        }
        public Button getUpdatePresentationButton()
        {
            return this.UpdatePresentationButton;
        }
        public Button GetFilterPresenationButton()
        {
            return this.FilterPrezentariButton;
        }

        //Participant Management Section
        public TextBox GetIdParticipantTextBox()
        {
            return this.IdParticipantTextBox;
        }
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
        public TextBox GetPhotoPathTextBox()
        {
            return this.PhotoPathTextBox;
        }
        public TextBox GetDocumentPathTextBox()
        {
            return this.DocumentPathTextBox;
        }

        public Button getCreateParticipantButton()
        {
            return this.CreateParticipantButton;
        }
        public Button getDeleteParticipantButton()
        {
            return this.DeleteParticipantButton;
        }
        public Button getUpdateParticipantButton()
        {
            return this.UpdateParticipantButton;
        }
        public Button getAcceptParticipantButton()
        {
            return this.AcceptParticipantButton;
        }

        public Button getRejectParticipantButton()
        {
            return this.RejectParticipantButton;
        }



        // Similarly, add getters for other controls, including buttons and DataGrids

        public DataGrid GetTabelPrezentari()
        {
            return this.TabelPrezentari;
        }

        public DataGrid GetTabelParticipanti()
        {
            return this.TabelParticipanti;
        }

        public ComboBox GetFilterParticipantsComboBox()
        {
            return this.FilterParticipantsComboBox;
        }
        // Getters for buttons if needed

        public ComboBox getComboBoxFiltrarePrezentare()
        {
            return this.ComboBoxFiltrarePrezentare;
        }

        public ComboBox getComboBoxFormat()
        {
            return this.ComboBoxFormat;
        }

        public Button getBackButton()
        {
            return this.BackButton;
        }

        public Button getFilterParticipantsButton()
        {
            return this.FilterParticipantButton;
        }

        public Button getFilterPrezentariButton()
        {
            return this.FilterPrezentariButton;
        }

        public Button getDownloadButton()
        {
            return this.DownloadButton;
        }


        //Statistics Section

        public Button getShowRingChartButton()
        {
            return this.ShowRingChartButton;
        }
        public Button getShowLineChartDialogButton()
        {
            return this.ShowLineChartDialogButton;
        }
        public Button getShowPresentationsByAuthorChartButton()
        {
            return this.ShowPresentationsByAuthorChartButton;
        }
        public Button getShowPresentationsPerDayChartButton()
        {
            return this.ShowPresentationsPerDayChartButton;
        }
        public Button getShowParticipantsBySectionChartButton()
        {
            return this.ShowParticipantsBySectionChartButton;
        }
        public Button getShowParticipantsByConferenceChartButton()
        {
            return this.ShowParticipantsByConferenceChartButton;
        }

    }

}
