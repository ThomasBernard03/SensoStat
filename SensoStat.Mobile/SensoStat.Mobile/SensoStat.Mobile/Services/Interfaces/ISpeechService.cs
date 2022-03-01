using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace SensoStat.Mobile.Services.Interfaces
{
    public interface ISpeechService
    {
        SpeechRecognizer SpeechRecognizer { get; set; }
        SpeechSynthesizer SpeechSynthesizer { get; set; }

        /// <summary>
        /// Method which use azure cognitive service
        /// </summary>
        /// <returns>The spoken text</returns>
        Task SpeechToText();

        /// <summary>
        /// Read a content
        /// </summary>
        /// <param name="content">The content which we want to read</param>
        /// <returns></returns>
        Task TextToSpeech(string content);
    }
}
