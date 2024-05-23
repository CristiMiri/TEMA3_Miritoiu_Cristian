using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS_TEMA3.Model.Builders
{
    public class ParticipantBuilder
    {
        private int _id;
        private string _name;
        private string _email;
        private string _phone;
        private string _cnp;
        private string _pdfFilePath;
        private string _photoFilePath;

        public ParticipantBuilder SetId(int id)
        {
            _id = id;
            return this;
        }

        public ParticipantBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ParticipantBuilder SetEmail(string email)
        {
            _email = email;
            return this;
        }

        public ParticipantBuilder SetPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public ParticipantBuilder SetCNP(string cnp)
        {
            _cnp = cnp;
            return this;
        }

        public ParticipantBuilder SetPdfFilePath(string pdfFilePath)
        {
            _pdfFilePath = pdfFilePath;
            return this;
        }

        public ParticipantBuilder SetPhotoFilePath(string photoFilePath)
        {
            _photoFilePath = photoFilePath;
            return this;
        }

        public Participant Build()
        {
            return new Participant(_id, _name, _email, _phone, _cnp, _pdfFilePath, _photoFilePath);
        }
    }
}
