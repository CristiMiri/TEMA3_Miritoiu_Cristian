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
    /// Interaction logic for OrganizatorGUI.xaml
    /// </summary>
    public partial class OrganizatorGUI : Page
    {
        public OrganizatorGUI()
        {
            InitializeComponent();
            OrganizatorController organizatorController = new OrganizatorController(this);
        }

        public TextBox GetTitluTextBox()
        {
            return this.TitluTextBox;
        }

        public TextBox GetAutorTextBox()
        {
            return this.AutorTextBox;
        }

        public TextBox GetDescriereTextBox()
        {
            return this.DescriereTextBox;
        }

        public DatePicker GetDataDatePicker()
        {
            return this.DataDatePicker;
        }

        public TextBox GetOraTextBox()
        {
            return this.OraTextBox;
        }

        public ComboBox GetComboBoxSectiuneAdministrare()
        {
            return this.ComboBoxSectiuneAdministrare;
        }

        public TextBox GetIdConferintaTextBox()
        {
            return this.IdConferintaTextBox;
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

        public ComboBox getComboBoxFilterParticipanti()
        {
            return this.ComboBoxFilterParticipanti;
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

        public Button getFilterButtonParticipanti()
        {
            return this.FilterButtonParticipanti;
        }

        public Button getFilterPrezentariButton()
        {
            return this.FilterPrezentariButton;
        }

        public Button getDownloadButton()
        {
            return this.DownloadButton;
        }
    }

}
