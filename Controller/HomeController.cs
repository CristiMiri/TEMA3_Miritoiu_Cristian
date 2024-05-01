using Microsoft.Win32;
using PS_TEMA3.Model;
using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PS_TEMA3.Controller
{
    internal class HomeController
    {
        private ParticipantiRepository participantiRepository;
        private PrezentareRepository prezentareRepository;
        private HomeGUI homeGUI;

        public HomeController(HomeGUI homeGUI)
        {
            this.homeGUI = homeGUI;
            participantiRepository = new ParticipantiRepository();
            prezentareRepository = new PrezentareRepository();
            homeGUI.GetCmbSectiune().ItemsSource = Enum.GetValues(typeof(Sectiune));
            TablePrezentari();

            //Buttons
            homeGUI.getFilterPrezenariButton().Click += new RoutedEventHandler(FilterTablePrezentari);
            homeGUI.getLoginButton().Click += new RoutedEventHandler(Login);
            homeGUI.getBrowsePhotoButton().Click += new RoutedEventHandler(BrowsePhoto_Click);
            homeGUI.getBrowseDocumentButton().Click += new RoutedEventHandler(BrowseDocument_Click);
        }

        public static string GetRelativePath(string basePath, string targetPath)
        {
            // Ensure ends with a slash to indicate folder
            if (!basePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                basePath += Path.DirectorySeparatorChar;

            Uri baseUri = new Uri(basePath);
            Uri targetUri = new Uri(targetPath);

            Uri relativeUri = baseUri.MakeRelativeUri(targetUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            // Convert URL path separator to local filesystem path separator
            relativePath = relativePath.Replace('/', Path.DirectorySeparatorChar);

            return relativePath;
        }

        private void TablePrezentari()
        {
            List<Prezentare> prezentari = prezentareRepository.ReadPrezentari();
            List<Participant> participanti = participantiRepository.ReadParticipanti();
            //get the relative path of the image from
            //Images folder

            string basePath = @"C:\Users\crist\Documents\FACULTATE\ANUL4_SEM2\PS\PS_TEME\PS_TEMA3\";
            string targetPath = @"C:\Users\crist\Documents\FACULTATE\ANUL4_SEM2\PS\PS_TEME\PS_TEMA3\Images\smiley.png";

            string relativePath = GetRelativePath(basePath, targetPath);
            Console.WriteLine(relativePath);  // Output: Images\smiley.png

            participanti.ElementAt(0).PdfFilePath = relativePath;
            //participanti.ElementAt(1).PdfFilePath = "C:\\Users\\crist\\Documents\\FACULTATE\\ANUL4_SEM2\\PS\\PS_TEME\\PS_TEMA3\\Images\\smiley.png";
            var ListaCompleta = (from prezentare in prezentari
                                 //join participant in participanti on prezentare.IdAutor equals participant.Id
                                 orderby prezentare.Id
                                 select new
                                 {
                                     Id = prezentare.Id,
                                     Titlu = prezentare.Titlu,
                                     //Autor = participant.Nume,
                                     Descriere = prezentare.Descriere,
                                     Data = prezentare.Data,
                                     Sectiune = prezentare.Sectiune
                                     //ImageUrl = participant.PdfFilePath
                                 }).ToList();

            homeGUI.GetTabelConferinte().ItemsSource = ListaCompleta;


        }

        private void FilterTablePrezentari(object sender, EventArgs e)
        {
            Sectiune sectiune = (Sectiune)homeGUI.GetCmbSectiune().SelectedItem;
            if (sectiune == Sectiune.TOATE)
            {
                List<Prezentare> prezentari = prezentareRepository.ReadPrezentari();
                homeGUI.GetTabelConferinte().ItemsSource = prezentari;
            }
            else
            {
                List<Prezentare> prezentari = prezentareRepository.ReadPrezentariBySectiune(sectiune);
                homeGUI.GetTabelConferinte().ItemsSource = prezentari;
            }


        }

        private void Login(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Content = new LoginGUI();
        }

        private void BrowsePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                homeGUI.getPhotoPathTexBox().Text = openFileDialog.FileName;
            }
        }

        private void BrowseDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Document files (*.pdf;*.docx)|*.pdf;*.docx|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                homeGUI.getDocumentPathTexBox().Text = openFileDialog.FileName;
            }
        }

        public void Inscriere(object sender,RoutedEventArgs e)
        {

        }
    }
}
