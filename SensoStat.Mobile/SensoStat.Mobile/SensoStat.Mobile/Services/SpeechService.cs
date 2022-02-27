using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using SensoStat.Mobile.Services.Interfaces;

namespace SensoStat.Mobile.Services
{
    public class SpeechService : ISpeechService
    {
        private readonly IMicrophoneService _microphoneService;

        public SpeechRecognizer SpeechRecognizer { get; set; }
        public SpeechConfig SpeechConfig { get; set; }
        public SourceLanguageConfig SourceLanguageConfig { get; set; }

        public SpeechService(IMicrophoneService microphoneService)
        {
            _microphoneService = microphoneService;
            SpeechConfig = SpeechConfig.FromSubscription(Commons.Constants.AzureKey, Commons.Constants.AzureRegion);
            SourceLanguageConfig = SourceLanguageConfig.FromLanguage("fr-FR");

        }

        public async Task SpeechToText()
        {
            try
            {
                var permissionsAllowed = await _microphoneService.GetPermissionAsync();
                if (!permissionsAllowed)
                    new Exception("Can't get microphone access");

                SpeechRecognizer = new SpeechRecognizer(SpeechConfig, SourceLanguageConfig, AudioConfig.FromDefaultMicrophoneInput());

                await SpeechRecognizer.StartContinuousRecognitionAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public Task TextToSpeech(string content)
        {
            throw new NotImplementedException();
        }
    }
}
