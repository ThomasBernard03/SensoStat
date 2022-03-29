using System.Threading.Tasks;

namespace SensoStat.Mobile.Services.Interfaces
{
    public interface IMicrophoneService
    {
        Task<bool> GetPermissionAsync();
        void OnRequestPermissionResult(bool isGranted);

    }
}