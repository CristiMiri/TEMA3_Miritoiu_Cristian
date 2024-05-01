using PS_TEMA3.Model;
using PS_TEMA3.View;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml;
using System.Windows;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Text;
using System.IO;
using System.Text.Json;

namespace PS_TEMA3.Controller
{
    internal class FileWriter
    {
        public void SaveCsv(List<Prezentare> ListaPrezentari)
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.csv";
            StringBuilder csvBuilder = new StringBuilder();
            // Add the header
            csvBuilder.AppendLine("ID;Titlu;Autor;Descriere;Data;Ora;Sectiune;ID Conferinta");

            foreach (var prezentare in ListaPrezentari)
            {
                // Convert enum to string
                string sectiuneName = Enum.GetName(typeof(Sectiune), prezentare.Sectiune);

                // Escape commas in strings
                string descriere = prezentare.Descriere.Contains(",") ? $"\"{prezentare.Descriere}\"" : prezentare.Descriere;
                string titlu = prezentare.Titlu.Contains(",") ? $"\"{prezentare.Titlu}\"" : prezentare.Titlu;

                // Append each property to the CSV line
                var line = string.Format("{0};{1};{2};{3};{4};{5};{6};{7}",
                                         prezentare.Id,
                                         titlu,
                                         descriere,
                                         prezentare.Data,
                                         prezentare.Ora,
                                         sectiuneName,
                                         prezentare.IdConferinta);

                csvBuilder.AppendLine(line);
            }

            // Write the CSV content to a file
            File.WriteAllText(filePath, csvBuilder.ToString());

        }
        public void SaveJson(List<Prezentare> ListaPrezentari)
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(ListaPrezentari, options);
            File.WriteAllText(filePath, json);
        }
        public void SaveXml(List<Prezentare> ListaPrezentari)
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.xml";
            var xml = new System.Xml.Serialization.XmlSerializer(typeof(List<Prezentare>));
            using (var writer = new StreamWriter(filePath))
            {
                xml.Serialize(writer, ListaPrezentari);
            }

        }
        public void SaveDoc(List<Prezentare> ListaPrezentari)
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.docx";
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Table table = new Table();

                // Set the styles for the table
                TableProperties tblProps = new TableProperties(
                    new TableBorders(
                        new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                        new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                        new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                        new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                        new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                        new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 }
                    ));
                table.AppendChild(tblProps);

                // Add header row
                TableRow headerRow = new TableRow();
                string[] headers = new string[] { "ID", "Titlu", "Autor", "Descriere", "Data", "Ora", "Sectiune", "ID Conferinta" };
                foreach (string header in headers)
                {
                    TableCell headerCell = new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(header))));
                    headerRow.Append(headerCell);
                }
                table.Append(headerRow);

                // Add data rows
                foreach (var prez in ListaPrezentari)
                {
                    TableRow dataRow = new TableRow();
                    TableCell[] cells = new TableCell[]
                    {
                    new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.Id.ToString())))),
                    new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.Titlu)))),
                    new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.Descriere)))),
                    new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.Data.ToString())))),
                    new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.Ora.ToString())))),
                    new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.Sectiune.ToString())))),
                    new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.IdConferinta.ToString()))))
                    };

                    foreach (var cell in cells)
                    {
                        dataRow.Append(cell);
                    }

                    table.Append(dataRow);
                }

                body.Append(table);
                mainPart.Document.Save();
            }
        }
    }
}







