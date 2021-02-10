using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.IO;

namespace Voiceassist
{
    public partial class Form1 : Form
    {
        /*
         * Voiceassist v1.0
         * Todo: 
         * Add an option to change input device instead of hardcoding to default device
         * Instead of hardcoding command responses, get responses from a text file.
         * Add a command to record input to add as a new command, and the response
         */
        SpeechSynthesizer Synthesizer = new SpeechSynthesizer();
        SpeechRecognitionEngine Speechrecognition = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
        }

        private void Speechrecognized(object seneder, SpeechRecognizedEventArgs e)
        {
        switch (e.Result.Text)
            {
                case "test":
                    Synthesizer.Speak("Test works!");
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Speechrecognition.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Speechrecognition.RecognizeAsyncStop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Speechrecognition.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"commands.txt")))));
            Speechrecognition.SetInputToDefaultAudioDevice();
            Speechrecognition.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Speechrecognized);
        }
    }
}
