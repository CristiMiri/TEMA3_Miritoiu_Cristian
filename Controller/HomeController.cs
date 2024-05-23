using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Win32;
using PS_TEMA3.Model;
using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PS_TEMA3.Controller
{
    internal class HomeController:IObserver
    {
        public ObservableCollection<Presentation> Presentations { get; set; }
        private readonly Subject _subject;
        private readonly ParticipantRepository _participantRepository;
        private readonly PresentationRepository _presentationRepository;
        private readonly Participant_PrezentareRepository _participantPresentationRepository;
        private string _imagesDirectory;
        private string _documentsDirectory;
        private readonly HomeGUI _homeGUI;

        public HomeController(HomeGUI homeGUI, Subject subject)
        {
            _homeGUI = homeGUI;
            _participantRepository = new ParticipantRepository();
            _presentationRepository = new PresentationRepository();
            _participantPresentationRepository = new Participant_PrezentareRepository(_participantRepository, _presentationRepository);

            InitializeEvents();
            InitializeDirectories();
            InitializePrezentareParticipantComboBox();
            InitializePresentationSectionComboBox();
            _subject = subject;
            Presentations = new ObservableCollection<Presentation>(_presentationRepository.ReadPresentations());
            _subject.Attach(this);
            LoadPresentationsTable();

        }

        public void AddPresentation(Presentation presentation)
        {
            Presentations.Add(presentation);
            _subject.Notify();
        }

        public void UpdatePresentation(Presentation presentation)
        {
            int index = Presentations.IndexOf(Presentations.FirstOrDefault(x => x.Id == presentation.Id));
            Presentations[index] = presentation;
            _subject.Notify();
        }

        public void RemovePresentation(Presentation presentation)
        {
            Presentations.Remove(presentation);
            _subject.Notify();
        }

        public void Update()
        {
            LoadPresentationsTable();
        }


        //public HomeController(HomeGUI homeGUI)
        //{
        //    this.homeGUI = homeGUI;
        //    participantiRepository = new ParticipantRepository();
        //    prezentareRepository = new PresentationRepository();
        //    participant_PrezentareRepository = new Participant_PrezentareRepository(participantiRepository, prezentareRepository);
        //    homeGUI.GetCmbSectiune().ItemsSource = Enum.GetValues(typeof(Section));

        //    //Buttons
        //    homeGUI.getFilterPrezenariButton().Click += new RoutedEventHandler(FilterTablePrezentari);
        //    homeGUI.getLoginButton().Click += new RoutedEventHandler(Login);
        //    homeGUI.getBrowsePhotoButton().Click += new RoutedEventHandler(BrowsePhoto_Click);
        //    homeGUI.getBrowseDocumentButton().Click += new RoutedEventHandler(BrowseDocument_Click);
        //    homeGUI.getInscriereButton().Click += new RoutedEventHandler(Inscriere);
        //    homeGUI.GetCmbAutor().SelectionChanged += new SelectionChangedEventHandler(Changed);

        //    //File Directories
        //    string appDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //    // Navigate up to the project root folder from bin\Debug\net8.0-windows
        //    string projectRoot = Path.GetFullPath(Path.Combine(appDirectory, @"..\..\.."));

        //    imagesDirectory = Path.Combine(projectRoot, "Images");
        //    documentsDirectory = Path.Combine(projectRoot, "Documents");
        //    TablePrezentari();
        //    //homeGUI.GetHyperlink().Click += new RoutedEventHandler(DownloadLink_Click);

        //    homeGUI.getEnglishButton().Click += new RoutedEventHandler(EnglishButton_Click);
        //    homeGUI.getRomanianButton().Click += new RoutedEventHandler(RomanianButton_Click);
        //}

        //Auxiliary methods
        private void InitializeEvents()
        {            
            _homeGUI.GetFilterPresentationButton().Click += FilterTablePrezentari;
            _homeGUI.getLoginButton().Click += Login;
            _homeGUI.getBrowsePhotoButton().Click += BrowsePhoto_Click;
            _homeGUI.getBrowseDocumentButton().Click += BrowseDocument_Click;
            _homeGUI.getSingUpButton().Click += Inscriere;
            _homeGUI.GetTypeComboBox().SelectionChanged += AuthorSelectionChanged;
            _homeGUI.getEnglishButton().Click += EnglishButton_Click;
            _homeGUI.getRomanianButton().Click += RomanianButton_Click;
        }
        private void InitializeDirectories()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(appDirectory, @"..\..\.."));
            _imagesDirectory = Path.Combine(projectRoot, "Images");
            _documentsDirectory = Path.Combine(projectRoot, "Documents");
        }
        private void InitializePrezentareParticipantComboBox()
        {
            List<Presentation> presentations = _presentationRepository.ReadPresentations();
            // select prezentare.Id, prezentare.Titlu and put it in the combobox
            var presentationItems = presentations.Select(p => new
            {
                Id = p.Id,
                Title = p.Title
            }).ToList();
            _homeGUI.GetAttendPresentationComboBox().ItemsSource = presentationItems;
            _homeGUI.GetAttendPresentationComboBox().DisplayMemberPath = "Title";
            _homeGUI.GetAttendPresentationComboBox().SelectedValuePath = "Id";
            _homeGUI.GetAttendPresentationComboBox().SelectedIndex = 0;
        }
        private void InitializePresentationSectionComboBox()
        {
            _homeGUI.GetSectionComboBox().ItemsSource = Enum.GetValues(typeof(Section));
            _homeGUI.GetSectionComboBox().SelectedIndex = 0;
            _homeGUI.GetFilterPresentationComboBox().ItemsSource = Enum.GetValues(typeof(Section));
            _homeGUI.GetFilterPresentationComboBox().SelectedIndex = 0;
        }       
        private void ShowAuthorFields(bool isSelected)
        {
            _homeGUI.GetTitleTextBox().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
            _homeGUI.GetDescriptionTextBox().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
            _homeGUI.GetTimeTextBox().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
            _homeGUI.GetDataDatePicker().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
            _homeGUI.GetSectionComboBox().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;

            _homeGUI.GetTitleLabel().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
            _homeGUI.GetDescriptionLabel().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
            _homeGUI.GetTimeLabel().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
            _homeGUI.GetDateLabel().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;
            _homeGUI.GetSectionLabel().Visibility = isSelected ? Visibility.Visible : Visibility.Collapsed;            
        }
        private void ShowParticipantFields(bool isSelected)
        {
            _homeGUI.GetPresentationLabel().Visibility = isSelected ? Visibility.Collapsed : Visibility.Visible;
            _homeGUI.GetAttendPresentationComboBox().Visibility = isSelected ? Visibility.Collapsed : Visibility.Visible;
        }

        //Inscriere functions
        //Utility methods
        private Participant ValidParticipantData()
        {
            string name = _homeGUI.GetNameTextBox().Text;
            string email = _homeGUI.GetEmailTextBox().Text;
            string phone = _homeGUI.GetPhoneTextBox().Text;
            string cnp = _homeGUI.GetPinTextBox().Text;
            string photoPath = _homeGUI.GetPhotoPathTexBox().Text;
            string photoFileName = Path.GetFileName(photoPath);
            string documentPath = _homeGUI.GetDocumentPathTexBox().Text;
            string documentFileName = Path.GetFileName(documentPath);
            int idPresentation = (int)_homeGUI.GetAttendPresentationComboBox().SelectedValue;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(photoPath) || string.IsNullOrEmpty(documentPath))
            {
                _homeGUI.ShowMessage("All fields are required!");
                return null;
            }
            return new Participant(0, name, email, phone, cnp, documentPath, photoFileName);
        }
        private Presentation ValidPresentationData()
        {
            string title = _homeGUI.GetTitleTextBox().Text;
            string description = _homeGUI.GetDescriptionTextBox().Text;
            DateTime date = _homeGUI.GetDataDatePicker().SelectedDate.Value;
            string hour = _homeGUI.GetTimeTextBox().Text;
            Section section = (Section)_homeGUI.GetSectionComboBox().SelectedValue;
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || date == null || section == null)
            {
                _homeGUI.ShowMessage("All fields are required!");
                return null;
            }
            return new Presentation(0, title, description, date, TimeSpan.Parse(hour), section, 1, 0);
        }
        private void ClearFields()
        {
            _homeGUI.GetNameTextBox().Clear();
            _homeGUI.GetEmailTextBox().Clear();
            _homeGUI.GetPhoneTextBox().Clear();
            _homeGUI.GetPhotoPathTexBox().Clear();
            _homeGUI.GetDocumentPathTexBox().Clear();
            _homeGUI.GetPinTextBox().Clear();
            //author fields
            _homeGUI.GetTitleTextBox().Clear();
            _homeGUI.GetDescriptionTextBox().Clear();
            _homeGUI.GetTimeTextBox().Clear();
            _homeGUI.GetDataDatePicker().SelectedDate = null;
            _homeGUI.GetSectionComboBox().SelectedIndex = 0;

        }
        private void CopyFile(string sourcePath, string destinationDirectory)
        {
            string fileName = Path.GetFileName(sourcePath);
            string destinationPath = Path.Combine(destinationDirectory, fileName);
            File.Copy(sourcePath, destinationPath, true);
        }

        private void CreateParticipant()
        {
            try
            {
                Participant participant = ValidParticipantData();
                if (participant != null)
                {
                    CopyFile(_homeGUI.GetPhotoPathTexBox().Text, _imagesDirectory);
                    CopyFile(_homeGUI.GetDocumentPathTexBox().Text, _documentsDirectory);
                    _homeGUI.ShowMessage("Files have been successfully uploaded!");
                    _participantRepository.UpsertParticipant(participant);
                    int idParticipant = _participantRepository.GetLastParticipantId();
                    int idPresentation = (int)_homeGUI.GetAttendPresentationComboBox().SelectedValue;
                    _participantPresentationRepository.CreateParticipantPresentation(idParticipant, idPresentation);
                    _homeGUI.ShowMessage("Participant created successfully!");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                _homeGUI.ShowMessage($"Error creating participant: {ex.Message}");
            }
        }
        private void CreateAuthor()
        {
            try
            {
                Participant author = ValidParticipantData();
                if (author != null)
                {
                    CopyFile(_homeGUI.GetPhotoPathTexBox().Text, _imagesDirectory);
                    CopyFile(_homeGUI.GetDocumentPathTexBox().Text, _documentsDirectory);
                    _homeGUI.ShowMessage("Files have been successfully uploaded!");
                    _participantRepository.UpsertParticipant(author);
                    _homeGUI.ShowMessage("Author created successfully!");
                    Presentation presentation = ValidPresentationData();
                    if (presentation != null)
                    {
                        int idAuthor = _participantRepository.GetLastParticipantId();
                        presentation.IdAuthor = idAuthor;
                        _presentationRepository.CreatePresentation(presentation);
                        _homeGUI.ShowMessage("Presentation created successfully!");
                        ClearFields();
                        AddPresentation(presentation);

                    }
                }
            }
            catch (Exception ex)
            {
                _homeGUI.ShowMessage($"Error creating author: {ex.Message}");
            }
        }

        //Button methods
        public void Inscriere(object sender, RoutedEventArgs e)
        {
            try
            {
                //CopyFile(_homeGUI.getPhotoPathTexBox().Text, _imagesDirectory);
                //CopyFile(_homeGUI.getDocumentPathTexBox().Text, _documentsDirectory);
                //_homeGUI.ShowMessage("Files have been successfully uploaded!");
                //ClearFields();
                if (_homeGUI.GetTypeComboBox().SelectedIndex == 0)
                {
                    CreateAuthor();
                }
                else
                {
                    CreateParticipant();
                }
            }
            catch (Exception ex)
            {
                _homeGUI.ShowMessage($"Error uploading files: {ex.Message}");
            }
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
        private string BrowseFile(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = filter };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : string.Empty;
        }
        private void BrowsePhoto_Click(object sender, RoutedEventArgs e)
        {
            string photoPath = BrowseFile("Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*");
            if (!string.IsNullOrEmpty(photoPath))
            {
                _homeGUI.GetPhotoPathTexBox().Text = photoPath;
            }
        }
        private void BrowseDocument_Click(object sender, RoutedEventArgs e)
        {
            string documentPath = BrowseFile("Document files (*.pdf;*.docx)|*.pdf;*.docx|All files (*.*)|*.*");
            if (!string.IsNullOrEmpty(documentPath))
            {
                _homeGUI.GetDocumentPathTexBox().Text = documentPath;
            }
        }
        /*//Autor fields        
        //TextBox
        public TextBox GetTitluTextBox()
        {
            return this.TitluTextBox;
        }
        public TextBox GetDescriereTextBox()
        {
            return this.DescriereTextBox;
        }
        //DatePicker
        public DatePicker GetDataDatePicker()
        {
            return this.DataDatePicker;
        }
        //ComboBox
        public ComboBox GetSectiuneComboBox()
        {
            return this.SectiuneComboBox;
        }
        //Labels
        public Label getTitluAutorLabel()
        {
            return this.TitluAutorLabel;
        }
        public Label getDescriereAutorLabel()
        {
            return this.DescriereAutorLabel;
        }
        public Label getDataAutorLabel()
        {
            return this.DataAutorLabel;
        }
        public Label getSectiuneAutorLabel()
        {
            return this.SectiuneAutorLabel;
        }*/
        public void AuthorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isSelected = _homeGUI.GetTypeComboBox().SelectedIndex == 0;
            ShowAuthorFields(isSelected);
            ShowParticipantFields(isSelected);
        }

        //Filter section functions
        private void FilterTablePrezentari(object sender, EventArgs e)
        {
            Section section = (Section)_homeGUI.GetFilterPresentationComboBox().SelectedItem;
            if (section == Section.ALL)
            {
               LoadPresentationsTable();
            }
            else
            {
                LoadFilterdPresentationTable(section);
            }


        }
        


        //Table functions
        private void LoadPresentationsTable()
        {
            List<Presentation> presentations = _presentationRepository.ReadPresentations();
            List<Participant> participants = _participantRepository.ReadParticipants();
            foreach (Presentation presentation in presentations)
            {
                presentation.Participants = _participantPresentationRepository.ReadParticipantsByPresentation(presentation);
                foreach (Participant participant in presentation.Participants)
                {
                    participant.PdfFilePath = Path.Combine(_documentsDirectory, participant.PdfFilePath);
                    participant.PhotoFilePath = Path.Combine(_imagesDirectory, participant.PhotoFilePath);                    
                }
                Participant author = _participantRepository.ReadParticipantById(presentation.IdAuthor);
                author.PdfFilePath = Path.Combine(_documentsDirectory, author.PdfFilePath);
                author.PhotoFilePath = Path.Combine(_imagesDirectory, author.PhotoFilePath);
                Console.WriteLine(author.PhotoFilePath);
                presentation.Author = new List<Participant> {author};
            }
            foreach (Participant participant in participants)
            {
                participant.PdfFilePath = Path.Combine(_imagesDirectory, participant.PdfFilePath);
                participant.PhotoFilePath = Path.Combine(_documentsDirectory, participant.PhotoFilePath);
            }
            var ListaCompleta = (from prezentare in presentations
                                 join participant in participants on prezentare.IdAuthor equals participant.Id
                                 orderby prezentare.Id
                                 select new
                                 {
                                     Id = prezentare.Id,
                                     Title = prezentare.Title,
                                     Author = prezentare.Author,
                                     Description= prezentare.Description,
                                     Date = prezentare.Date,
                                     Section= prezentare.Section,
                                     ImageUrl = participant.PdfFilePath,
                                     Participants = prezentare.Participants,
                                 }).ToList();
            ObservableCollection<Presentation> prezentari_ = new ObservableCollection<Presentation>(presentations);
            _homeGUI.GetTabelConferinte().ItemsSource = ListaCompleta;
        }
        private void LoadFilterdPresentationTable(Section section)
        { 
            List<Presentation> presentations = _presentationRepository.ReadPresentationsBySection(section);
            List<Participant> participants = _participantRepository.ReadParticipants();
            foreach (Presentation presentation in presentations)
            {
                presentation.Participants = _participantPresentationRepository.ReadParticipantsByPresentation(presentation);
                foreach (Participant participant in presentation.Participants)
                {
                    participant.PdfFilePath = Path.Combine(_documentsDirectory, participant.PdfFilePath);
                    participant.PhotoFilePath = Path.Combine(_imagesDirectory, participant.PhotoFilePath);
                }
                Participant author = _participantRepository.ReadParticipantById(presentation.IdAuthor);
                author.PdfFilePath = Path.Combine(_documentsDirectory, author.PdfFilePath);
                author.PhotoFilePath = Path.Combine(_imagesDirectory, author.PhotoFilePath);
                Console.WriteLine(author.PhotoFilePath);
                presentation.Author = new List<Participant> { author };
            }
            foreach (Participant participant in participants)
            {
                participant.PdfFilePath = Path.Combine(_imagesDirectory, participant.PdfFilePath);
                participant.PhotoFilePath = Path.Combine(_documentsDirectory, participant.PhotoFilePath);
            }
            var ListaCompleta = (from prezentare in presentations
                                 join participant in participants on prezentare.IdAuthor equals participant.Id
                                 orderby prezentare.Id
                                 select new
                                 {
                                     Id = prezentare.Id,
                                     Title = prezentare.Title,
                                     Author = prezentare.Author,
                                     Description = prezentare.Description,
                                     Date = prezentare.Date,
                                     Section = prezentare.Section,
                                     ImageUrl = participant.PdfFilePath,
                                     Participants = prezentare.Participants,
                                 }).ToList();
            ObservableCollection<Presentation> prezentari_ = new ObservableCollection<Presentation>(presentations);
            _homeGUI.GetTabelConferinte().ItemsSource = ListaCompleta;
        }
        public void DownloadLink_Click(string pdf)
        {
            string fileName = Path.GetFileName(pdf);
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF file (*.pdf)|*.pdf",
                DefaultExt = "pdf",
                AddExtension = true,
                FileName = "File.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string sourceFilePath = Path.Combine(_documentsDirectory, fileName);
                File.Copy(sourceFilePath, saveFileDialog.FileName, true);
            }
        }

        //Auxiliary functions
        private void Login(object sender, EventArgs e)
        {
            //Application.Current.MainWindow.Content = new LoginGUI();
            LoginGUI loginGUI = new LoginGUI(_subject);
            loginGUI.Show();
        }
        private void EnglishButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).ChangeLanguage("en");
        }
        private void RomanianButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).ChangeLanguage("ro");
        }
        private void FrenchButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).ChangeLanguage("fr");
        }
        private void SpanishButton_Click(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).ChangeLanguage("es");
        }
    }
}
