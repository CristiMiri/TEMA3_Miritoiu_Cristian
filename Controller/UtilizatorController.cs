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
        private Participant_PrezentareRepository participant_PrezentareRepository;
        private UtilizatorGUI _utilizatorGUI;

        public UtilizatorController(UtilizatorGUI utilizatorGUI)
        {
            this._utilizatorGUI = utilizatorGUI;
            prezentareRepository = new PrezentareRepository();
            conferintaRepository = new ConferintaRepository();
            participantRepository = new ParticipantiRepository();
            participant_PrezentareRepository = new Participant_PrezentareRepository(participantRepository, prezentareRepository);
            utilizatorGUI.GetBackButton().Click += Back;
            LoadConferinte();
        }

        public void LoadConferinte()
        {
            List<Conferinta> conferinte = conferintaRepository.ReadConferinte();
            List<Prezentare> prezentari = prezentareRepository.ReadPrezentari();
            List<Participant> participanti = participantRepository.ReadParticipanti();

            List<Participant> autori = new List<Participant>();
            foreach (Prezentare p in prezentari)
            {
                List<Participant> autorPrezentare = participant_PrezentareRepository.ReadRelationsbyRole<Participant>(p.Id, "AUTOR", "Participant");
                autori.AddRange(autorPrezentare);
            }
            var ListaCompleta = (from conferinta in conferinte
                                             join prezentare in prezentari on conferinta.Id equals prezentare.IdConferinta
                                             
                                             orderby prezentare.Id
                                             select new
                                             {
                                                 ConferintaId = conferinta.Id,
                                                 ConferintaTitlu = conferinta.Titlu,
                                                 ConferintaLocatie = conferinta.Locatie,
                                                 ConferintaData = conferinta.Data,
                                                 PrezentareId = prezentare.Id,
                                                 PrezentareTitlu = prezentare.Titlu,                                                 
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
