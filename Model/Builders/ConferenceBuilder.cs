using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Builders
{
    public class ConferenceBuilder
    {
        private int _id;
        private string _title;
        private string _location;
        private DateTime _date;

        public ConferenceBuilder SetId(int id)
        {
            _id = id;
            return this;
        }

        public ConferenceBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public ConferenceBuilder SetLocation(string location)
        {
            _location = location;
            return this;
        }

        public ConferenceBuilder SetDate(DateTime date)
        {
            _date = date;
            return this;
        }

        public Conference Build()
        {
            return new Conference
            {
                Id = _id,
                Title = _title,
                Location = _location,
                Date = _date
            };
        }
    }
}
