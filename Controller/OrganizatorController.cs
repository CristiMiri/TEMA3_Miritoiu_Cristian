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

namespace PS_TEMA3.Controller
{
    internal class OrganizatorController
    {
        private OrganizatorGUI organizatorGUI;
        private ParticipantiRepository participantiRepository;
        private PrezentareRepository prezentareRepository;
        private FileWriter fileWriter;

        public OrganizatorController(OrganizatorGUI organizatorGUI)
        {
            this.organizatorGUI = organizatorGUI;
            participantiRepository = new ParticipantiRepository();
            prezentareRepository = new PrezentareRepository();
            fileWriter = new FileWriter();
            ParticipantiTable();
            PrezentareTable();
            fillComboBox();
            //Buttons
            organizatorGUI.getBackButton().Click += new RoutedEventHandler(Back);
            organizatorGUI.getFilterButtonParticipanti().Click += new RoutedEventHandler(FilterParticipanti);
            organizatorGUI.getFilterPrezentariButton().Click += new RoutedEventHandler(FilterPrezentari);
            organizatorGUI.getDownloadButton().Click += new RoutedEventHandler(Save);
            organizatorGUI.getAcceptButton().Click += new RoutedEventHandler(AcceptParticipant);
            organizatorGUI.getRejectButton().Click += new RoutedEventHandler(RejectParticipant);
            organizatorGUI.GetTabelParticipanti().SelectionChanged += new SelectionChangedEventHandler(SelectParticipant);            
        }

        private void ParticipantiTable()
        {
            organizatorGUI.GetTabelParticipanti().ItemsSource = participantiRepository.ReadParticipanti();
        }

        private void PrezentareTable()
        {
            organizatorGUI.GetTabelPrezentari().ItemsSource = prezentareRepository.ReadPrezentari();
        }
        private void fillComboBox()
        {
            organizatorGUI.getComboBoxFilterParticipanti().ItemsSource = Enum.GetValues(typeof(Sectiune));
            organizatorGUI.getComboBoxFilterParticipanti().SelectedIndex = 0;
            organizatorGUI.getComboBoxFiltrarePrezentare().ItemsSource = Enum.GetValues(typeof(Sectiune));
            organizatorGUI.getComboBoxFiltrarePrezentare().SelectedIndex = 0;
            organizatorGUI.getComboBoxFormat().Items.Add("Csv");
            organizatorGUI.getComboBoxFormat().Items.Add("Doc");
            organizatorGUI.getComboBoxFormat().Items.Add("Json");
            organizatorGUI.getComboBoxFormat().Items.Add("Xml");
            organizatorGUI.getComboBoxFormat().SelectedIndex = 0;
        }

        private void Back(object sender, EventArgs e)
        {
            System.Windows.Application.Current.MainWindow.Content = new HomeGUI();
        }

        private void FilterParticipanti(object sender, EventArgs e)
        {
            Sectiune sectiune = (Sectiune)organizatorGUI.getComboBoxFilterParticipanti().SelectedItem;
            if (sectiune == Sectiune.TOATE)
                organizatorGUI.GetTabelParticipanti().ItemsSource = participantiRepository.ReadParticipanti();
            else
                organizatorGUI.GetTabelParticipanti().ItemsSource = participantiRepository.GetParticipantibySectiune(sectiune);
        }

        private void FilterPrezentari(object sender, EventArgs e)
        {
            Sectiune sectiune = (Sectiune)organizatorGUI.getComboBoxFiltrarePrezentare().SelectedItem;
            if (sectiune == Sectiune.TOATE)
                organizatorGUI.GetTabelPrezentari().ItemsSource = prezentareRepository.ReadPrezentari();
            else
                organizatorGUI.GetTabelPrezentari().ItemsSource = prezentareRepository.ReadPrezentariBySectiune(sectiune);
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
                List<Prezentare> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Prezentare>().ToList();
                fileWriter.SaveCsv(ListaPrezentari);
                MessageBox.Show("Lista prezentarilor salvata cu succes!");
            }
            if (selectedFileFormat == "Json")
            {
                List<Prezentare> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Prezentare>().ToList();
                fileWriter.SaveJson(ListaPrezentari);
                MessageBox.Show("Lista prezentarilor salvata cu succes!");
            }
            if (selectedFileFormat == "Xml")
            {
                List<Prezentare> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Prezentare>().ToList();
                fileWriter.SaveXml(ListaPrezentari);
                MessageBox.Show("Lista prezentarilor salvata cu succes!");

            }
            if (selectedFileFormat == "Doc")
            {
                List<Prezentare> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Prezentare>().ToList();
                fileWriter.SaveDoc(ListaPrezentari);
                MessageBox.Show("Lista prezentarilor salvata cu succes!");
            }
        }


        public void SelectParticipant(object sender , EventArgs e)
        {
            Participant participant = (Participant)organizatorGUI.GetTabelParticipanti().SelectedItem;
            if (participant != null)
            {
                organizatorGUI.getNumeParticipantiTextBox().Text = participant.Nume;
                organizatorGUI.getEmailParticipantiTextBox().Text = participant.Email;
                organizatorGUI.getTelefonParticipantiTextBox().Text = participant.Telefon;                
            }
        }

        public void AcceptParticipant(object sender, EventArgs e)
        {
            string fromAddress = Environment.GetEnvironmentVariable("EmailUsername");
            string fromPassword = Environment.GetEnvironmentVariable("EmailSTMP");
            string subject = "Participare acceptata";
            string body = "Felicitari! Participarea dumneavoastra a fost acceptata! Va asteptam la eveniment!";
            string toAddress = "cristianmiritoiu6@gmail.com";
            //ToDo: get email from participant
            //string toAddress = organizatorGUI.getEmailParticipantiTextBox().Text;
            SendMail(toAddress, subject, body, fromAddress, fromPassword);

        }
        public void RejectParticipant(object sender, EventArgs e)
        {
            string fromAddress = Environment.GetEnvironmentVariable("EmailUsername");
            string fromPassword = Environment.GetEnvironmentVariable("EmailSTMP");
            string subject = "Participare respinsa";
            string body = "Ne pare rau, dar participarea dumneavoastra a fost respinsa!";
            string toAddress = "cristianmiritoiu6@gmail.com";
            //string toAddress = organizatorGUI.getEmailParticipantiTextBox().Text;
            //ToDo: get email from participant
            SendMail(toAddress, subject, body, fromAddress, fromPassword);
        }

        private void SendMail(string toAddress, string subject, string body, string fromAddress, string fromPassword)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromAddress, fromPassword),
                    EnableSsl = true,
                };

                smtpClient.Send(fromAddress, toAddress, subject, body);

                Console.WriteLine("Email sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not send the email. Error: " + ex.Message);
            }
        }

    }
}
