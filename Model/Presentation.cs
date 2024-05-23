using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PS_TEMA3.Model
{
    public enum Section
    {
        SCIENCE,
        TECHNOLOGY,
        MEDICINE,
        ART,
        SPORT,
        ALL
    }

    public class Presentation :INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private string _description;
        private DateTime _date;
        private TimeSpan _hour;
        private Section _section;
        private int _idConference;
        private int _idAuthor;
        private List<Participant> _participants;
        private List<Participant> _author;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Hour
        {
            get => _hour;
            set
            {
                _hour = value;
                OnPropertyChanged();
            }
        }

        public Section Section
        {
            get => _section;
            set
            {
                _section = value;
                OnPropertyChanged();
            }
        }

        public int IdConference
        {
            get => _idConference;
            set
            {
                _idConference = value;
                OnPropertyChanged();
            }
        }

        public int IdAuthor
        {
            get => _idAuthor;
            set
            {
                _idAuthor = value;
                OnPropertyChanged();
            }
        }

        public List<Participant> Participants
        {
            get => _participants;
            set
            {
                _participants = value;
                OnPropertyChanged();
            }
        }

        public List<Participant> Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public Presentation() // Default constructor initializes default values
        {
            Id = 0;
            Title = "";
            Description = "";
            Date = DateTime.Today;
            Hour = TimeSpan.Zero;
            Section = Section.ALL;
            IdConference = 0;
            IdAuthor = 0;
            Participants = new List<Participant>();
            Author = new List<Participant>();
        }

        public Presentation(int id, string title, string description, DateTime date, TimeSpan hour, Section section, int idConference, int idAuthor)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            Hour = hour;
            Section = section;
            IdConference = idConference;
            IdAuthor = idAuthor;
            Participants = new List<Participant>();
            Author = new List<Participant>();
        }

        public Presentation(Presentation presentation)
        {
            Id = presentation.Id;
            Title = presentation.Title;
            Description = presentation.Description;
            Date = presentation.Date;
            Hour = presentation.Hour;
            Section = presentation.Section;
            IdConference = presentation.IdConference;
            IdAuthor = presentation.IdAuthor;
            Participants = presentation.Participants;
            Author = presentation.Author;
        }

        public List<Presentation> dummyPresentationData()
        {
            List<Presentation> presentations = new List<Presentation>
            {
                new Presentation(1, "Introduction to Quantum Computing", "An overview of quantum computing principles", new DateTime(2024, 4, 10), new TimeSpan(9, 0, 0), Section.SCIENCE, 1, 1),
                new Presentation(2, "Web Development Trends", "Exploring the latest trends in web development", new DateTime(2024, 4, 11), new TimeSpan(10, 30, 0), Section.TECHNOLOGY, 1, 1),
                new Presentation(3, "Advancements in Cancer Treatment", "Recent breakthroughs in cancer treatment", new DateTime(2024, 4, 12), new TimeSpan(13, 45, 0), Section.MEDICINE, 2, 2),
                new Presentation(4, "Modern Art and Its Impact", "Analyzing contemporary art movements", new DateTime(2024, 4, 13), new TimeSpan(15, 0, 0), Section.ART, 2, 2),
                new Presentation(5, "The Science of Athletic Performance", "Understanding the physiology behind sports performance", new DateTime(2024, 4, 14), new TimeSpan(11, 0, 0), Section.SPORT, 3, 3)
            };
            return presentations;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
