using Domain;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{

    public interface IRepositorySicknessHistory : IRepository
    {
        //SicknessHistory closed
        IList<SicknessHistory> ClosedSicknessHistoryBeforeData(DateTime date);

        //SicknessHistory ORDING BY doctors
        IList<SicknessHistory> SicknessHistoriesOrderByDoctors();

        //SicknessHistory GroupBy Doctors having finishDate complete
        IList<SicknessHistory> SicknessHistoriesClosedOrderByDoctors();

        //SicknessHist cu specificarea daca e actuala sau inchisa
        IList<SicknessHistorySpecificateDto> SicknessHistoryWithSpecification();

        //Informatie despre persoane in hospital (Doctori si Pacienti aparte)
        int CountSicknessHistoriesAndShortInfoAboutPersonsInHospital();

        //Corespondenta Patient - Doctor
        void CorespondencePatientDoctor();
    }
}
