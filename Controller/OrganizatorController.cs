using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PS_TEMA3.Controller
{
    internal class OrganizatorController
    {
        private OrganizatorGUI organizatorGUI;
        private ParticipantiRepository participantiRepository;
        private PrezentareRepository prezentareRepository;

        public OrganizatorController(OrganizatorGUI organizatorGUI)
        {
            this.organizatorGUI = organizatorGUI;
            participantiRepository = new ParticipantiRepository();
            prezentareRepository = new PrezentareRepository();

            //Buttons
            organizatorGUI.getBackButton().Click += new RoutedEventHandler(Back);
            
        }

        private void Back(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Content = new HomeGUI();
        }
    }
}
