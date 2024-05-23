using PS_TEMA3.Model;
using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Documents;
using System.Net.Mail;
using System.Net;
using System.Windows.Controls;
using Section = PS_TEMA3.Model.Section;

namespace PS_TEMA3.Controller
{
    internal class OrganizatorController
    {
        private OrganizatorGUI organizatorGUI;
        private ParticipantRepository participantiRepository;
        private PresentationRepository prezentareRepository;
        private Participant_PrezentareRepository participant_PrezentareRepository;
        private FileWriter fileWriter;
        private EmailSender emailSender = new EmailSender();
        private FileManager fileManager = new FileManager();

        public OrganizatorController(OrganizatorGUI organizatorGUI)
        {
            this.organizatorGUI = organizatorGUI;
            participantiRepository = new ParticipantRepository();
            prezentareRepository = new PresentationRepository();
            participant_PrezentareRepository = new Participant_PrezentareRepository(this.participantiRepository, this.prezentareRepository);
            fileWriter = new FileWriter();
            LoadParticipantsTable();
            LoadPresentationTable();
            fillComboBox();
            InitializeEvents();
        }

        //Auxiliary methods
        private void InitializeEvents()
        {
            //Presentation Management Section
            organizatorGUI.GetTabelPrezentari().SelectionChanged += new SelectionChangedEventHandler(SelectPresentation);
            organizatorGUI.getCreatePresentationButton().Click += new RoutedEventHandler(CreatePresentation);
            organizatorGUI.getUpdatePresentationButton().Click += new RoutedEventHandler(UpdatePresentation);
            organizatorGUI.getDeletePresentationButton().Click += new RoutedEventHandler(DeletePresentation);



            organizatorGUI.getBackButton().Click += new RoutedEventHandler(Back);
            organizatorGUI.getFilterPrezentariButton().Click += new RoutedEventHandler(FilterPresentations);
            organizatorGUI.getDownloadButton().Click += new RoutedEventHandler(Save);


            //Participant Management Section
            organizatorGUI.getFilterParticipantsButton().Click += new RoutedEventHandler(FilterParticipanti);
            organizatorGUI.GetTabelParticipanti().SelectionChanged += new SelectionChangedEventHandler(SelectParticipant);
            organizatorGUI.getCreateParticipantButton().Click += new RoutedEventHandler(CreateParticipant);
            organizatorGUI.getUpdateParticipantButton().Click += new RoutedEventHandler(UpdateParticipant);
            organizatorGUI.getDeleteParticipantButton().Click += new RoutedEventHandler(DeleteParticipant);
            organizatorGUI.getAcceptParticipantButton().Click += new RoutedEventHandler(AcceptParticipant);
            organizatorGUI.getRejectParticipantButton().Click += new RoutedEventHandler(RejectParticipant);

            //Charts Section
            organizatorGUI.getShowParticipantsByConferenceChartButton().Click += new RoutedEventHandler(ShowParticipantsByConferenceChart);
            organizatorGUI.getShowParticipantsBySectionChartButton().Click += new RoutedEventHandler(ShowParticipantsBySectionChart);
            organizatorGUI.getShowPresentationsByAuthorChartButton().Click += new RoutedEventHandler(ShowPresentationsByAuthorChart);
            organizatorGUI.getShowPresentationsPerDayChartButton().Click += new RoutedEventHandler(ShowPresentationsPerDayChart);
            organizatorGUI.getShowLineChartDialogButton().Click += new RoutedEventHandler(ShowLineChart);
            organizatorGUI.getShowRingChartButton().Click += new RoutedEventHandler(ShowRingChart);

        }
        public void Save(object sender, EventArgs e)
        {
            String selectedFileFormat = organizatorGUI.getComboBoxFormat().SelectedItem.ToString();
            if (String.IsNullOrEmpty(selectedFileFormat))
            {
                MessageBox.Show("Va rog alegeti un format de fisier!");
                return;
            }
            if (selectedFileFormat == "Csv")
            {
                List<Presentation> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Presentation>().ToList();
                fileWriter.SaveCsv(ListaPrezentari);
                MessageBox.Show("Lista prezentarilor salvata cu succes!");
            }
            if (selectedFileFormat == "Json")
            {
                List<Presentation> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Presentation>().ToList();
                fileWriter.SaveJson(ListaPrezentari);
                MessageBox.Show("Lista prezentarilor salvata cu succes!");
            }
            if (selectedFileFormat == "Xml")
            {
                List<Presentation> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Presentation>().ToList();
                fileWriter.SaveXml(ListaPrezentari);
                MessageBox.Show("Lista prezentarilor salvata cu succes!");

            }
            if (selectedFileFormat == "Doc")
            {
                List<Presentation> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Presentation>().ToList();
                //fileWriter.SaveCsv(ListaPrezentari);
                //fileWriter.SaveDocFromCsv();
                fileWriter.SaveDoc(ListaPrezentari);
                MessageBox.Show("Lista prezentarilor salvata cu succes!");
            }
        }

        private void fillComboBox()
        {
            organizatorGUI.GetFilterParticipantsComboBox().ItemsSource = Enum.GetValues(typeof(Section));
            organizatorGUI.GetFilterParticipantsComboBox().SelectedIndex = 0;
            organizatorGUI.getComboBoxFiltrarePrezentare().ItemsSource = Enum.GetValues(typeof(Section));
            organizatorGUI.getComboBoxFiltrarePrezentare().SelectedIndex = 0;
            organizatorGUI.getComboBoxFormat().Items.Add("Csv");
            organizatorGUI.getComboBoxFormat().Items.Add("Doc");
            organizatorGUI.getComboBoxFormat().Items.Add("Json");
            organizatorGUI.getComboBoxFormat().Items.Add("Xml");
            organizatorGUI.getComboBoxFormat().SelectedIndex = 0;

            organizatorGUI.getSectionComboBox().ItemsSource = Enum.GetValues(typeof(Section));
            organizatorGUI.getSectionComboBox().SelectedIndex = 0;
        }

        private void Back(object sender, EventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Content = new HomeGUI();
        }

    //Presentation Management Section
    //Auxiliary methods
    private void ClearPresentationFields()
        {
            organizatorGUI.GetIdPresentationTextBox().Text = "";
            organizatorGUI.GetTitleTextBox().Text = "";
            organizatorGUI.GetDescritionTextBox().Text = "";
            organizatorGUI.GetDataDatePicker().SelectedDate = DateTime.Now;
            organizatorGUI.GetTimeTextBox().Text = "";
            organizatorGUI.GetAuthorTextBox().Text = "";
        }
        private void LoadPresentationTable()
        {
            List<Presentation> presentations = prezentareRepository.ReadPresentations();
            foreach (Presentation presentation in presentations)
            {
                Participant Author = participantiRepository.ReadParticipantById(presentation.IdAuthor);
                presentation.Author.Add(Author);
                
                List<Participant> participants = participant_PrezentareRepository.ReadParticipantsByPresentation(presentation);
                foreach (Participant participant in participants)
                {
                    presentation.Participants.Add(participant);
                }

            }
            organizatorGUI.GetTabelPrezentari().ItemsSource = presentations;
        }
        private void LoadPresentationTablebySection(Section section)
        {
            List<Presentation> presentations = prezentareRepository.ReadPresentationsBySection(section);
            foreach (Presentation presentation in presentations)
            {
                Participant Author = participantiRepository.ReadParticipantById(presentation.IdAuthor);
                presentation.Author.Add(Author);

                List<Participant> participants = participant_PrezentareRepository.ReadParticipantsByPresentation(presentation);
                foreach (Participant participant in participants)
                {
                    presentation.Participants.Add(participant);
                }

            }
            organizatorGUI.GetTabelPrezentari().ItemsSource = presentations;
        }
        private Presentation validPresentationData()
        {
            int id = Convert.ToInt32(organizatorGUI.GetIdPresentationTextBox().Text);
            string title = organizatorGUI.GetTitleTextBox().Text;
            string description = organizatorGUI.GetDescritionTextBox().Text;
            DateTime date = organizatorGUI.GetDataDatePicker().SelectedDate.Value;
            TimeSpan hour = TimeSpan.Parse(organizatorGUI.GetTimeTextBox().Text);
            Section section = (Section)organizatorGUI.getSectionComboBox().SelectedItem;
            int idAuthor = Convert.ToInt32(organizatorGUI.GetAuthorTextBox().Text);
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(description) || String.IsNullOrEmpty(hour.ToString()))
            {
                MessageBox.Show("Va rog completati toate campurile!");
                return null;
            }
            return new Presentation(id, title, description, date, hour, section, 1, idAuthor);
        }

        private void SelectPresentation(object sender, EventArgs e)
        {
            Presentation presentation = (Presentation)organizatorGUI.GetTabelPrezentari().SelectedItem;
            if (presentation != null)
            {
                organizatorGUI.GetIdPresentationTextBox().Text = presentation.Id.ToString();
                organizatorGUI.GetTitleTextBox().Text = presentation.Title;
                organizatorGUI.GetDescritionTextBox().Text = presentation.Description;
                organizatorGUI.GetDataDatePicker().SelectedDate = presentation.Date;
                organizatorGUI.GetTimeTextBox().Text = presentation.Hour.ToString();
                organizatorGUI.getSectionComboBox().SelectedItem = presentation.Section;
                organizatorGUI.GetAuthorTextBox().Text = presentation.IdAuthor.ToString();
            }
        }
        private void FilterPresentations(object sender, EventArgs e)
        {
            Section sectiune = (Section)organizatorGUI.getComboBoxFiltrarePrezentare().SelectedItem;
            if (sectiune == Section.ALL)
                LoadPresentationTable();
            else
               LoadPresentationTablebySection(sectiune);
        }
        private void CreatePresentation(object sender, EventArgs e)
        {
            Presentation presentation = validPresentationData();
            if (presentation != null)
            {
                bool result = prezentareRepository.CreatePresentation(presentation);
                if (result)
                {
                    MessageBox.Show("Prezentare adaugata cu succes!");
                    LoadPresentationTable();
                    ClearPresentationFields();
                }
            }
        }
        private void UpdatePresentation(object sender, EventArgs e)
        {
            Presentation presentation = validPresentationData();
            if (presentation != null)
            {
                bool result = prezentareRepository.UpdatePresentation(presentation);
                if (result)
                {
                    MessageBox.Show("Prezentare actualizata cu succes!");
                    LoadPresentationTable();
                    ClearPresentationFields();
                }
            }
        }
        private void DeletePresentation(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(organizatorGUI.GetIdPresentationTextBox().Text);
            prezentareRepository.DeletePresentation(id);
            MessageBox.Show("Prezentare stearsa cu succes!");
            LoadPresentationTable();
            ClearPresentationFields();
        }


        //Participant Management Section
        //Auxiliary methods
        private void ClearParticipantFields()
        {
            organizatorGUI.GetIdParticipantTextBox().Text = "";
            organizatorGUI.GetNameTextBox().Text = "";
            organizatorGUI.GetEmailTextBox().Text = "";
            organizatorGUI.GetPhoneTextBox().Text = "";
            organizatorGUI.GetPinTextBox().Text = "";
            organizatorGUI.GetPhotoPathTextBox().Text = "";
            organizatorGUI.GetDocumentPathTextBox().Text = "";
        }
        private void LoadParticipantsTable()
        {
            List<Participant> participants = participantiRepository.ReadParticipants();
            foreach (Participant participant in participants)
            {
               participant.PhotoFilePath = fileManager.GetImageFilePath(Path.GetFileName(participant.PhotoFilePath));
               participant.PdfFilePath = fileManager.GetDocumentFilePath(Path.GetFileName(participant.PdfFilePath));                                
            }
            organizatorGUI.GetTabelParticipanti().ItemsSource = participants;
        }
        private void LoadParticipantsTableBySection(Section section)
        {
            List<Participant> participants = participantiRepository.GetParticipantsBySection(section);
            foreach (Participant participant in participants)
            {
                participant.PhotoFilePath = fileManager.GetImageFilePath(Path.GetFileName(participant.PhotoFilePath));
                participant.PdfFilePath = fileManager.GetDocumentFilePath(Path.GetFileName(participant.PdfFilePath));
            }
            organizatorGUI.GetTabelParticipanti().ItemsSource = participants;
        }
        private Participant ValidParticipantData()
        {
            int id = Convert.ToInt32(organizatorGUI.GetIdParticipantTextBox().Text);            
            string name = organizatorGUI.GetNameTextBox().Text;
            string email = organizatorGUI.GetEmailTextBox().Text;
            string phone = organizatorGUI.GetPhoneTextBox().Text;
            string pin = organizatorGUI.GetPinTextBox().Text;
            string photoPath = organizatorGUI.GetPhotoPathTextBox().Text;
            string documentPath = organizatorGUI.GetDocumentPathTextBox().Text;           
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(phone) || String.IsNullOrEmpty(pin) || String.IsNullOrEmpty(photoPath) || String.IsNullOrEmpty(documentPath))
            {
                MessageBox.Show("Va rog completati toate campurile!");
                return null;
            }
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Email invalid!");
                return null;
            }
            if (phone.Length != 10)
            {
                MessageBox.Show("Numar de telefon invalid!");
                return null;
            }
            if (pin.Length != 13)
            {
                MessageBox.Show("CNP invalid!");
                return null;
            }
            return new Participant(id,name, email, phone, pin, documentPath, photoPath);
        }

        //Event methods
        public void FilterParticipanti(object sender, EventArgs e)
        {
            Section sectiune = (Section)organizatorGUI.GetFilterParticipantsComboBox().SelectedItem;
            if (sectiune == Section.ALL)
                LoadParticipantsTable();
            else
                LoadParticipantsTableBySection(sectiune);
        }
        public void SelectParticipant(object sender , EventArgs e)
        {
            Participant participant = (Participant)organizatorGUI.GetTabelParticipanti().SelectedItem;
            if (participant != null)
            {
                organizatorGUI.GetIdParticipantTextBox().Text = participant.Id.ToString();
                organizatorGUI.GetNameTextBox().Text = participant.Name;
                organizatorGUI.GetEmailTextBox().Text = participant.Email;
                organizatorGUI.GetPhoneTextBox().Text = participant.Phone;  
                organizatorGUI.GetPinTextBox().Text = participant.CNP;
                organizatorGUI.GetPhotoPathTextBox().Text = participant.PhotoFilePath;
                organizatorGUI.GetDocumentPathTextBox().Text = participant.PdfFilePath;
            }
        }
        public void AcceptParticipant(object sender, EventArgs e)
        {
            string subject = "Participare acceptata";
            string body = "Felicitari! Participarea dumneavoastra a fost acceptata! Va asteptam la eveniment!";
            string toAddress = organizatorGUI.GetEmailTextBox().Text;
            emailSender.SendMail(toAddress, subject, body);      
            MessageBox.Show("Email trimis cu succes!");

        }
        public void RejectParticipant(object sender, EventArgs e)
        {            
            string subject = "Participare respinsa";
            string body = "Ne pare rau, dar participarea dumneavoastra a fost respinsa!";
            string toAddress = "cristianmiritoiu6@gmail.com";
            //Todo - de inlocuit cu email-ul participantului
            emailSender.SendMail(toAddress, subject, body);
            MessageBox.Show("Email trimis cu succes!");
        }
        public void CreateParticipant(object sender, EventArgs e)
        {
            Participant participant = ValidParticipantData();
            if (participant != null)
            {
                bool result =participantiRepository.CreateParticipant(participant);
                if (result)
                {
                    MessageBox.Show("Participant adaugat cu succes!");
                    LoadParticipantsTable();
                    ClearParticipantFields();
                }
            }
        }
        public void UpdateParticipant(object sender, EventArgs e)
        {
            Participant participant = ValidParticipantData();
            if (participant != null)
            {
                bool result =participantiRepository.UpdateParticipant(participant);
                if (result)
                {
                    MessageBox.Show("Participant actualizat cu succes!");
                    LoadParticipantsTable();
                    ClearParticipantFields();
                }
            }
        }
        public void DeleteParticipant(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(organizatorGUI.GetIdParticipantTextBox().Text);
            participantiRepository.DeleteParticipant(id);
            MessageBox.Show("Participant sters cu succes!");
            LoadParticipantsTable();
            ClearParticipantFields();
        }

        //Charts Section

        //ShowRingChartButton
        //ShowLineChartDialogButton
        //ShowPresentationsByAuthorChartButton
        //ShowPresentationsPerDayChartButton
        //ShowParticipantsBySectionChartButton
        //ShowParticipantsByConferenceChart
        private void ShowParticipantsByConferenceChart(object sender, EventArgs e)
        {
            ChartView chartDialog = new ChartView();
            chartDialog.ShowParticipantsByConferenceChart();
        }

        private void ShowParticipantsBySectionChart(object sender, EventArgs e)
        {
            ChartView chartDialog = new ChartView();
            chartDialog.ShowParticipantsBySectionChart();
        }

        private void ShowPresentationsByAuthorChart(object sender, EventArgs e)
        {
            ChartView chartDialog = new ChartView();
            chartDialog.ShowPresentationsByAuthorChart();
        }

        private void ShowPresentationsPerDayChart(object sender, EventArgs e)
        {
            ChartView chartDialog = new ChartView();
            chartDialog.ShowPresentationsPerDayChart();
        }

        private void ShowChartsButton_Click(object sender, EventArgs e)
        {
            ChartView chartDialog = new ChartView();
            chartDialog.ShowDialog();
        }

        private void ShowLineChart(object sender, EventArgs e)
        {
            ChartView chartDialog = new ChartView();
            chartDialog.ShowLineChart();
        }

        private void ShowRingChart(object sender, EventArgs e)
        {
            ChartView chartDialog = new ChartView();
            chartDialog.ShowRingChart();
        }


    }
}
