using PS_TEMA3.Model;
using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PS_TEMA3.Controller
{
    internal class UtilizatorController
    {
        private readonly PresentationRepository _presentationRepository;
        private readonly ConferenceRepository _conferenceRepository;
        private readonly ParticipantRepository _participantRepository;
        private readonly Participant_PrezentareRepository _participantPresentationRepository;
        private readonly UtilizatorGUI _utilizatorGUI;

        public UtilizatorController(UtilizatorGUI utilizatorGUI)
        {
            _utilizatorGUI = utilizatorGUI;
            _presentationRepository = new PresentationRepository();
            _conferenceRepository = new ConferenceRepository();
            _participantRepository = new ParticipantRepository();
            _participantPresentationRepository = new Participant_PrezentareRepository(_participantRepository, _presentationRepository);

            InitializeEvents();
            LoadConferencesTable();
        }

        private void InitializeEvents()
        {
            _utilizatorGUI.GetBackButton().Click += Back;
        }

        private void LoadConferencesTable()
        {
            try
            {
                List<Presentation> presentations = _presentationRepository.ReadPresentations();
                List<Participant> participants = _participantRepository.ReadParticipants();
                foreach (Presentation presentation in presentations)
                {
                    presentation.Participants = _participantPresentationRepository.ReadParticipantsByPresentation(presentation);
                    Participant author = _participantRepository.ReadParticipantById(presentation.IdAuthor);
                    presentation.Author = new List<Participant> { author };
                }
                List<Conference> conferences = _conferenceRepository.ReadConferences();                
                var completeList = (from conference in conferences
                                    join presentation in presentations on conference.Id equals presentation.IdConference
                                    join participant in participants on presentation.IdAuthor equals participant.Id
                                    orderby presentation.Id
                                    select new
                                    {
                                        ConferenceId = conference.Id,
                                        ConferenceTitle = conference.Title,
                                        ConferenceLocation = conference.Location,
                                        ConferenceDate = conference.Date,
                                        PresentationId = presentation.Id,
                                        PresentationTitle = presentation.Title,
                                        PresentationDescription = presentation.Description,
                                        PresentationAuthor = participant.Name,
                                        PresentationDate = presentation.Date,
                                        PresentationHour = presentation.Hour,
                                        PresentationSection = presentation.Section,
                                        PresentationConferenceId = presentation.IdConference,
                                        Title = presentation.Title,
                                        Author = presentation.Author,
                                        Description = presentation.Description,
                                        Date = presentation.Date,
                                        Section = presentation.Section,
                                        ImageUrl = participant.PdfFilePath,
                                        Participants = presentation.Participants,
                                    }).ToList();

                ObservableCollection<Presentation> prezentari_ = new ObservableCollection<Presentation>(presentations);
                _utilizatorGUI.GetTabelConferinte().ItemsSource = completeList;

            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading conferences: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

        private void Back(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Content = new HomeGUI();
        }
    }
}
