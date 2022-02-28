using Plugin.CurrentActivity;
using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Google.Android.Material.Snackbar;
using SensoStat.Mobile.Services.Interfaces;
using Android.App;

namespace SensoStat.Mobile.Droid.Services
{
    public class MicrophoneService : IMicrophoneService
    {
        public const int RecordAudioPermissionCode = 1;
        private TaskCompletionSource<bool> _tcsPermissions;
        readonly string[] _permissions = { Manifest.Permission.RecordAudio };
        public Task<bool> GetPermissionAsync()
        {
            _tcsPermissions = new TaskCompletionSource<bool>();
            if ((int)Build.VERSION.SdkInt < 23)
            {
                _tcsPermissions.TrySetResult(true);
            }
            else
            {
                var currentActivity = MainActivity.Instance;
                if (ContextCompat.CheckSelfPermission(currentActivity, Manifest.Permission.RecordAudio) !=
                    (int)Permission.Granted)
                {
                    RequestMicPermissions();
                }
                else
                {
                    _tcsPermissions.TrySetResult(true);
                }
            }
            return _tcsPermissions.Task;
        }
        public void OnRequestPermissionResult(bool isGranted)
        {
            _tcsPermissions.TrySetResult(isGranted);
        }
        private void RequestMicPermissions()
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(MainActivity.Instance,
                Manifest.Permission.RecordAudio))
            {
            //    Snackbar.Make(MainActivity.Instance.FindViewById(Android.Resource.Id.content),
            //            "Microphone permissions are required for speech transcription!",
            //            BaseTransientBottomBar.LengthIndefinite)
            //        .SetAction("Ok",
            //            v =>
            //            {
            //                ((Activity)MainActivity.Instance).RequestPermissions(_permissions,
            //                    RecordAudioPermissionCode);
            //            })
            //        .Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(MainActivity.Instance, _permissions, RecordAudioPermissionCode);
            }
        }
    }
}

