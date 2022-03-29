using System.Threading.Tasks;
using AVFoundation;
using SensoStat.Mobile.Services.Interfaces;

namespace SensoStat.Mobile.iOS.Services
{
    public class MicrophoneService : IMicrophoneService
    {
        private TaskCompletionSource<bool> _tcsPermissions;

        public Task<bool> GetPermissionAsync()
        {
            _tcsPermissions = new TaskCompletionSource<bool>();
            RequestMicPermission();
            return _tcsPermissions.Task;
        }

        public void OnRequestPermissionResult(bool isGranted)
        {
            _tcsPermissions.TrySetResult(isGranted);
        }

        private void RequestMicPermission()
        {
            var session = AVAudioSession.SharedInstance();
            session.RequestRecordPermission((granted) =>
            {
                _tcsPermissions.TrySetResult(granted);
            });
        }
    }
}

