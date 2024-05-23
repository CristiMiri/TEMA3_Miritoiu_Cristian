using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Builders
{
    public class PresentationBuilder
    {
        private int _id;
        private string _title;
        private string _description;
        private DateTime _date;
        private TimeSpan _hour;
        private Section _section;
        private int _idConference;
        private int _idAuthor;
        private List<Participant> _participants = new List<Participant>();
        private List<Participant> _author = new List<Participant>();

        public PresentationBuilder SetId(int id)
        {
            _id = id;
            return this;
        }

        public PresentationBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public PresentationBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public PresentationBuilder SetDate(DateTime date)
        {
            _date = date;
            return this;
        }

        public PresentationBuilder SetHour(TimeSpan hour)
        {
            _hour = hour;
            return this;
        }

        public PresentationBuilder SetSection(Section section)
        {
            _section = section;
            return this;
        }

        public PresentationBuilder SetIdConference(int idConference)
        {
            _idConference = idConference;
            return this;
        }

        public PresentationBuilder SetIdAuthor(int idAuthor)
        {
            _idAuthor = idAuthor;
            return this;
        }

        public PresentationBuilder SetParticipants(List<Participant> participants)
        {
            _participants = participants;
            return this;
        }

        public PresentationBuilder SetAuthor(List<Participant> author)
        {
            _author = author;
            return this;
        }

        public Presentation Build()
        {
            return new Presentation
            {
                Id = _id,
                Title = _title,
                Description = _description,
                Date = _date,
                Hour = _hour,
                Section = _section,
                IdConference = _idConference,
                IdAuthor = _idAuthor,
                Participants = _participants,
                Author = _author
            };
        }
    }
}
