using PS_TEMA3.Model;
using PS_TEMA3.Model.Repository;
using PS_TEMA3.View;
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Documents;

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
            ParticipantiTable();
            PrezentareTable();
            fillComboBox();
            //Buttons
            organizatorGUI.getBackButton().Click += new RoutedEventHandler(Back);            
            organizatorGUI.getFilterButtonParticipanti().Click += new RoutedEventHandler(FilterParticipanti);
            organizatorGUI.getFilterPrezentariButton().Click += new RoutedEventHandler(FilterPrezentari);
            organizatorGUI.getDownloadButton().Click += new RoutedEventHandler(Save);
        }

        private void ParticipantiTable()
        {
            organizatorGUI.GetTabelParticipanti().ItemsSource = participantiRepository.GetParticipanti();
        }

        private void PrezentareTable()
        {
            organizatorGUI.GetTabelPrezentari().ItemsSource = prezentareRepository.GetPrezentari();
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
                organizatorGUI.GetTabelParticipanti().ItemsSource = participantiRepository.GetParticipanti();
            else
            organizatorGUI.GetTabelParticipanti().ItemsSource = participantiRepository.GetParticipantibySectiune(sectiune);
        }

        private void FilterPrezentari(object sender, EventArgs e)
        {
            Sectiune sectiune = (Sectiune)organizatorGUI.getComboBoxFiltrarePrezentare().SelectedItem;
            if (sectiune == Sectiune.TOATE)
                organizatorGUI.GetTabelPrezentari().ItemsSource = prezentareRepository.GetPrezentari();
            else
                organizatorGUI.GetTabelPrezentari().ItemsSource = prezentareRepository.getPrezentariBySectiune(sectiune);
        }

        public void Save(object sender,EventArgs e)
        {
            String selectedFileFormat = organizatorGUI.getComboBoxFormat().SelectedItem.ToString();
            if (String.IsNullOrEmpty(selectedFileFormat))
            {
                MessageBox.Show("Va rog alegeti un format de fisier!");
                return;
            }
            if (selectedFileFormat == "Csv")
            {
                SaveCsv();
            }
            if (selectedFileFormat == "Json")
            {
                SaveJson();
            }
            if (selectedFileFormat == "Xml")
            {
                SaveXml();
            }
            if (selectedFileFormat == "Doc")
            {
                SaveDoc();
            }
        }
        private void SaveCsv()
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.csv";
            StringBuilder csvBuilder = new StringBuilder();
            // Add the header
            csvBuilder.AppendLine("ID,Titlu,Autor,Descriere,Data,Ora,Sectiune,ID Conferinta");
            List<Prezentare> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Prezentare>().ToList();
            foreach (var prezentare in ListaPrezentari)
            {
                // Convert enum to string
                string sectiuneName = Enum.GetName(typeof(Sectiune), prezentare.Sectiune);

                // Escape commas in strings
                string descriere = prezentare.Descriere.Contains(",") ? $"\"{prezentare.Descriere}\"" : prezentare.Descriere;
                string titlu = prezentare.Titlu.Contains(",") ? $"\"{prezentare.Titlu}\"" : prezentare.Titlu;

                // Append each property to the CSV line
                var line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                                         prezentare.Id,
                                         titlu,
                                         prezentare.IdAutor,
                                         descriere,
                                         prezentare.Data,
                                         prezentare.Ora,
                                         sectiuneName,
                                         prezentare.IdConferinta);

                csvBuilder.AppendLine(line);
            }

            // Write the CSV content to a file
            File.WriteAllText(filePath, csvBuilder.ToString());
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }
        private void SaveJson()
        {
            List<Prezentare> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Prezentare>().ToList();
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(ListaPrezentari, options);
            File.WriteAllText(filePath, json);
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }
        private void SaveXml()
        {
            List<Prezentare> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Prezentare>().ToList();
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.xml";
            var xml = new System.Xml.Serialization.XmlSerializer(typeof(List<Prezentare>));
            using (var writer = new StreamWriter(filePath))
            {
                xml.Serialize(writer, ListaPrezentari);
            }
            MessageBox.Show("Lista prezentari salvata cu succes!");
        }
        private void SaveDoc()
        {
            List<Prezentare> ListaPrezentari = organizatorGUI.GetTabelPrezentari().ItemsSource.Cast<Prezentare>().ToList();
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.doc";

            using (DocumentFormat.OpenXml.Packaging.WordprocessingDocument wordDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                DocumentFormat.OpenXml.Wordprocessing.Body body = mainPart.Document.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Body());
                DocumentFormat.OpenXml.Wordprocessing.Table table = new DocumentFormat.OpenXml.Wordprocessing.Table();

                // Define the properties for the table
                TableProperties props = new TableProperties(
                    new TableBorders(
                        new TopBorder { Val = new DocumentFormat.OpenXml.EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new BottomBorder { Val = new DocumentFormat.OpenXml.EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new LeftBorder { Val = new DocumentFormat.OpenXml.EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new RightBorder { Val = new DocumentFormat.OpenXml.EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new InsideHorizontalBorder { Val = new DocumentFormat.OpenXml.EnumValue<BorderValues>(BorderValues.Single), Size = 12 },
                        new InsideVerticalBorder { Val = new DocumentFormat.OpenXml.EnumValue<BorderValues>(BorderValues.Single), Size = 12 }
                    )
                );
                table.AppendChild(props);

                // Add header row
                DocumentFormat.OpenXml.Drawing.TableRow headerRow = new DocumentFormat.OpenXml.Drawing.TableRow();
                string[] headers = { "ID", "Titlu", "Autor", "Descriere", "Data", "Ora", "Sectiune", "ID Conferinta" };
                foreach (string header in headers)
                {
                    DocumentFormat.OpenXml.Drawing.TableCell headerCell = new DocumentFormat.OpenXml.Drawing.TableCell
                        (new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text(header))));
                    headerRow.Append(headerCell);
                }
                table.Append(headerRow);

                // Add data rows
                foreach (var prez in ListaPrezentari)
                {
                    DocumentFormat.OpenXml.Drawing.TableRow dataRow = new DocumentFormat.OpenXml.Drawing.TableRow();
                    DocumentFormat.OpenXml.Drawing.TableCell[] cells = {
                new DocumentFormat.OpenXml.Drawing.TableCell (new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text(prez.Id.ToString())))),
                new DocumentFormat.OpenXml.Drawing.TableCell(new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run (new DocumentFormat.OpenXml.Drawing.Text(prez.Titlu)))),
                new DocumentFormat.OpenXml.Drawing.TableCell (new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text(prez.IdAutor.ToString())))),
                new DocumentFormat.OpenXml.Drawing.TableCell (new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text(prez.Descriere)))),
                new DocumentFormat.OpenXml.Drawing.TableCell (new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text(prez.Data.ToString())))),
                new DocumentFormat.OpenXml.Drawing.TableCell (new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text(prez.Ora.ToString())))),
                new DocumentFormat.OpenXml.Drawing.TableCell (new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text(prez.Sectiune.ToString())))),
                new DocumentFormat.OpenXml.Drawing.TableCell (new DocumentFormat.OpenXml.Drawing.Paragraph(new DocumentFormat.OpenXml.Drawing.Run(new DocumentFormat.OpenXml.Drawing.Text(prez.IdConferinta.ToString()))))
            };

                    foreach (DocumentFormat.OpenXml.Drawing.TableCell cell in cells)
                    {
                        dataRow.Append(cell);
                    }
                    table.Append(dataRow);
                }

                // Add the table to the body of the document
                body.AppendChild(table);
            }

            MessageBox.Show("Lista prezentari salvata cu succes!");
        }
    }
}
