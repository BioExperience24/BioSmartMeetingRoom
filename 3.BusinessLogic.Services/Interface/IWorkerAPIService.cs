namespace _3.BusinessLogic.Services.Interface;

public interface IWorkerAPIService
{
    Task<ReturnalModel> CheckMeetingToday(DateOnly? dateNow);
}
