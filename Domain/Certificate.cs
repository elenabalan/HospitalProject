using System;

namespace Domain
{
    public class Certificate : Entity
    {
        public virtual string CertificateNumber { get; protected set; }
        public virtual MedicalSpecialty Specialty { get; protected set; }
        public virtual Doctor Doctor { get; protected set; }
        public virtual DateTime DataOfReceiving { get; set; }
        public virtual long ValidFor { get; set; }


        public Certificate(string certificateNumber, MedicalSpecialty specialty, Doctor doctor, DateTime dateOfReceiving,
            long validFor)
        {
            if (String.IsNullOrEmpty(certificateNumber))
                throw new ArgumentNullException($"{nameof(certificateNumber)} e inadmisibil");
            CertificateNumber = certificateNumber;

            Specialty = specialty;

            if (doctor == null) throw new ArgumentNullException($"{nameof(doctor)} nu e indicat");
            Doctor = doctor;

            if (dateOfReceiving > DateTime.Now) throw new ArgumentException($"{nameof(dateOfReceiving)} este incorect");
            DataOfReceiving = dateOfReceiving;

            if (validFor <= 0) throw new ArgumentException($"{nameof(validFor)} este negativ");
            ValidFor = validFor;
        }

        [Obsolete]
        protected Certificate()
        {
        }
    }
}
