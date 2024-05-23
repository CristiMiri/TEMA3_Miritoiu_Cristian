﻿using PS_TEMA3.Model;
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
        public void SaveCsv(List<Model.Presentation> ListaPrezentari)
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.csv";
            StringBuilder csvBuilder = new StringBuilder();
            // Add the header
            csvBuilder.AppendLine("ID;Titlu;Descriere;Data;Ora;Sectiune;IdAuthor;Author;Participanti");

            foreach (var prezentare in ListaPrezentari)
            {
                // Convert enum to string
                string sectiuneName = Enum.GetName(typeof(Section), prezentare.Section);
                string author = prezentare.Author.Count > 0 ? prezentare.Author[0].Name : "";

                // Escape commas in strings
                string descriere = prezentare.Description.Contains(",") ? $"\"{prezentare.Description}\"" : prezentare.Description;
                string titlu = prezentare.Title.Contains(",") ? $"\"{prezentare.Title}\"" : prezentare.Title;

                string formattedDate = prezentare.Date.ToString("D",new System.Globalization.CultureInfo("ro-Ro")); // "d" is the short date format

                List<string> participants = prezentare.Participants.ConvertAll(p => p.Name);
                StringBuilder participantsBuilder = new StringBuilder();
                foreach (var participant in participants)
                {
                    participantsBuilder.Append(participant);
                    participantsBuilder.Append(", ");
                }
                // Append each property to the CSV line
                var line = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}",
                                         prezentare.Id,
                                         titlu,
                                         descriere,
                                         formattedDate,
                                         prezentare.Hour,
                                         sectiuneName,
                                         prezentare.IdAuthor,
                                         author,
                                         participantsBuilder.ToString());

                csvBuilder.AppendLine(line);
            }

            // Write the CSV content to a file
            //File.WriteAllText(filePath, csvBuilder.ToString());
            File.WriteAllText(filePath, csvBuilder.ToString(), Encoding.UTF8);

        }
        public void SaveJson(List<Model.Presentation> ListaPrezentari)
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(ListaPrezentari, options);
            File.WriteAllText(filePath, json);
        }
        public void SaveXml(List<Model.Presentation> ListaPrezentari)
        {
            string filePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.xml";
            var xml = new System.Xml.Serialization.XmlSerializer(typeof(List<Model.Presentation>));
            using (var writer = new StreamWriter(filePath))
            {
                xml.Serialize(writer, ListaPrezentari);
            }

        }
        public void SaveDoc(List<Model.Presentation> ListaPrezentari)
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
                string[] headers = new string[] { "ID", "Titlu", "Descriere", "Data", "Ora", "Sectiune", "IdAuthor", "Author", "Participanti" };
                foreach (string header in headers)
                {
                    TableCell headerCell = new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(header))));
                    headerRow.Append(headerCell);
                }
                table.Append(headerRow);

                // Add data rows
                foreach (var prez in ListaPrezentari)
                {
                    // Convert enum to string
                    string sectiuneName = Enum.GetName(typeof(Section), prez.Section);
                    string author = prez.Author.Count > 0 ? prez.Author[0].Name : "";
                    string formattedDate = prez.Date.ToString("D", new System.Globalization.CultureInfo("ro-RO")); // "D" is the long date format

                    // Escape commas in strings
                    string descriere = prez.Description.Contains(",") ? $"\"{prez.Description}\"" : prez.Description;
                    string titlu = prez.Title.Contains(",") ? $"\"{prez.Title}\"" : prez.Title;

                    // Convert participants list to a single string with line breaks
                    List<string> participants = prez.Participants.ConvertAll(p => p.Name);
                    string participantsString = string.Join("\n", participants); // Join names with a line break

                    TableRow dataRow = new TableRow();
                    TableCell[] cells = new TableCell[]
                    {
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.Id.ToString())))),
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(titlu)))),
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(descriere)))),
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(formattedDate)))),
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.Hour.ToString())))),
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(sectiuneName)))),
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(prez.IdAuthor.ToString())))),
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(author)))),
                        new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(participantsString))))
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

        public void SaveDocFromCsv()
        {
            // Create the Word document
            string csvFilePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.csv";
            string wordFilePath = "C:\\Users\\crist\\Desktop\\ListaPrezentari.docx";
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(wordFilePath, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Table table = new Table();

                // Set the styles for the table
                TableProperties tblProps = new TableProperties(
                    new TableStyle { Val = "TableGrid" },  // Default Table Style
                    new TableBorders(
                    new TopBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                    new BottomBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                    new LeftBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                    new RightBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                    new InsideHorizontalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 },
                    new InsideVerticalBorder { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 24 }
            ));
                TableStyle tableStyle = new TableStyle() { Val = "TableStyleList3Accent1" };
                tblProps.Append(tableStyle);
                table.AppendChild(tblProps);

                // Read the CSV file and add rows to the Word table
                using (StreamReader sr = new StreamReader(csvFilePath))
                {
                    string line;
                    bool isHeader = true;
                    while ((line = sr.ReadLine()) != null)
                    {
                        TableRow row = new TableRow();
                        string[] values = line.Split(';'); // Assuming CSV uses ';' as delimiter

                        foreach (string value in values)
                        {
                            TableCell cell = new TableCell(new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(value))));
                            row.Append(cell);
                        }

                        table.Append(row);
                        if (isHeader)
                        {
                            isHeader = false;
                        }
                    }
                }

                body.Append(table);
                mainPart.Document.Save();
            }
        }
    }
}







