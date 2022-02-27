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
        public SpeechSynthesizer SpeechSynthesizer { get; set; }

        private SpeechConfig _speechConfig;
        private SourceLanguageConfig _sourceLanguageConfig;
        private AudioConfig _audioConfig;

        public SpeechService(IMicrophoneService microphoneService)
        {
            _microphoneService = microphoneService;
            _speechConfig = SpeechConfig.FromSubscription(Commons.Constants.AzureKey, Commons.Constants.AzureRegion);
            _sourceLanguageConfig = SourceLanguageConfig.FromLanguage("fr-FR");
            _speechConfig.SpeechSynthesisLanguage = "fr-FR";
            _speechConfig.SpeechSynthesisVoiceName = "fr-BE-CharlineNeural";
            _audioConfig = AudioConfig.FromDefaultSpeakerOutput();

        }

        public async Task SpeechToText()
        {
            try
            {
                var permissionsAllowed = await _microphoneService.GetPermissionAsync();
                if (!permissionsAllowed)
                    new Exception("Can't get microphone access");

                SpeechRecognizer = new SpeechRecognizer(_speechConfig, _sourceLanguageConfig, AudioConfig.FromDefaultMicrophoneInput());

                await SpeechRecognizer.StartContinuousRecognitionAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public async Task TextToSpeech(string content)
        {
            SpeechSynthesizer = new SpeechSynthesizer(_speechConfig, _audioConfig);

            await SpeechSynthesizer.SpeakTextAsync(content);
        }
    }
}
