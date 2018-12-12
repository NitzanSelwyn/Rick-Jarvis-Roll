using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RickJarvis
{
    class Program
    {
        private static SpeechSynthesizer synth = new SpeechSynthesizer();

        private static int speachSpeed = 1;

        private static Process p1 = new Process();

        private static ManagementEventWatcher watcher = new ManagementEventWatcher("Select * From Win32_ProcessStopTrace");

        private static string rolledURL = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";


        static void Main(string[] args)
        {
            PerformanceCounter perfCpuCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");

            while (true)
            {
                int currentCpu = (int)perfCpuCounter.NextValue();
                Console.WriteLine($"CPU {currentCpu}");

                if (speachSpeed < 5)
                {
                    speachSpeed++;
                }


                if (currentCpu == 100)
                {
                    string cpuload = string.Format("warning! holy crap your cpu is on fire!");
                    Speaker(cpuload, VoiceGender.Female, speachSpeed);

                    OpenWeb(rolledURL);

                    bool isRunning = Process.GetProcessesByName(p1.ProcessName).Any();
                    if (isRunning == false)
                    {
                        OpenWeb(rolledURL);

                    }
                }
                Thread.Sleep(1000);
            }
        }


        private static void Speaker(string message, VoiceGender voiceGender)
        {
            synth.SelectVoiceByHints(voiceGender);
            synth.Speak(message);
        }

        private static void Speaker(string message, VoiceGender voiceGender, int rate)
        {
            synth.Rate = rate;
            Speaker(message, voiceGender);
        }

        private static void OpenWeb(string URL)
        {
            p1.StartInfo.FileName = "chrome.exe";
            p1.StartInfo.Arguments = URL;
            p1.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p1.Start();
        }
    }
}
