using PS_TEMA3.Model;
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
    internal class UtilizatorController
    {
        private PrezentareRepository prezentareRepository;
        private ConferintaRepository conferintaRepository;
        private ParticipantiRepository participantRepository;
        private UtilizatorGUI _utilizatorGUI;

        public UtilizatorController(UtilizatorGUI utilizatorGUI)
        {
            this._utilizatorGUI = utilizatorGUI;
            prezentareRepository = new PrezentareRepository();
            conferintaRepository = new ConferintaRepository();
            participantRepository = new ParticipantiRepository();
            utilizatorGUI.GetBackButton().Click += Back;
            LoadConferinte();
        }

        public void LoadConferinte()
        {
            List<Conferinta> conferinte = conferintaRepository.GetConferinte();
            List<Prezentare> prezentari = prezentareRepository.GetPrezentari();
            List<Participant> participanti = participantRepository.GetParticipanti();
            var ListaCompleta = (from conferinta in conferinte
                                             join prezentare in prezentari on conferinta.Id equals prezentare.IdConferinta
                                             join participant in participanti on prezentare.IdAutor equals participant.Id
                                             orderby prezentare.Id
                                             select new
                                             {
                                                 ConferintaId = conferinta.Id,
                                                 ConferintaTitlu = conferinta.Titlu,
                                                 ConferintaLocatie = conferinta.Locatie,
                                                 ConferintaData = conferinta.Data,
                                                 PrezentareId = prezentare.Id,
                                                 PrezentareTitlu = prezentare.Titlu,
                                                 PrezentareAutor = participant.Nume,
                                                 PrezentareDescriere = prezentare.Descriere,
                                                 PrezentareData = prezentare.Data,
                                                 PrezentareOra = prezentare.Ora,
                                                 PrezentareSectiune = prezentare.Sectiune,
                                                 PrezentareConferintaId = prezentare.IdConferinta
                                             }).ToList();
            _utilizatorGUI.GetTabelConferinte().ItemsSource = ListaCompleta;
        }
        
        private void Back(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Content = new HomeGUI();
        }
    }
}
