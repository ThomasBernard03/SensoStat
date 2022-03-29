using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using SensoStat.Mobile.Services.Interfaces;

namespace SensoStat.Mobile.Services
{
    public class SpeechService : ISpeechService
    {
        #region Privates
        private readonly IMicrophoneService _microphoneService;
        private readonly SpeechConfig _speechConfig;
        private readonly SourceLanguageConfig _sourceLanguageConfig;
        private readonly AudioConfig _audioConfig;

        #endregion

        #region Publics
        public SpeechRecognizer SpeechRecognizer { get; set; }
        public SpeechSynthesizer SpeechSynthesizer { get; set; }

        #endregion

        #region CTOR
        public SpeechService(IMicrophoneService microphoneService)
        {
            _microphoneService = microphoneService;
            _speechConfig = SpeechConfig.FromSubscription(Commons.Constants.AzureKey, Commons.Constants.AzureRegion);
            _sourceLanguageConfig = SourceLanguageConfig.FromLanguage("fr-FR");
            _speechConfig.SpeechSynthesisLanguage = "fr-FR";
            _speechConfig.SpeechSynthesisVoiceName = "fr-BE-CharlineNeural";
            _audioConfig = AudioConfig.FromDefaultSpeakerOutput();
        }

        #endregion

        #region Methodes
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

        public async Task StopTextToSpeech()
        {
            await SpeechSynthesizer.StopSpeakingAsync();
        }

        #endregion
    }
}
