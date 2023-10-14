using System.Media;

namespace Blackjack
{
    internal class Sounds
    {
        public static void WinSound()
        {
            string soundFilePath = "../../../Files/KACHING.WAV";
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
                soundPlayer.Load();
                soundPlayer.Play();
            }
        }
    }
}
