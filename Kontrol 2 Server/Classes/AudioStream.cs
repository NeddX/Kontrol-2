using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;

namespace Kontrol_2_Server
{
    public class AudioStream
    {
        /// <summary>
        /// Audio Stream provider
        /// </summary>
        NAudio.Wave.BufferedWaveProvider provider;
        /// <summary>
        /// Audio stream player
        /// </summary>
        NAudio.Wave.WaveOut waveOut;

        /// <summary>
        /// Init audio stream playing
        /// </summary>
        public void Init()
        {
            provider = new NAudio.Wave.BufferedWaveProvider(new NAudio.Wave.WaveFormat()); //Setup the provider
            waveOut = new NAudio.Wave.WaveOut(); //Setup the player
            waveOut.Init(provider); //Bind the player to the provider
            waveOut.Play(); //Start playing incoming data with the player
        }

        /// <summary>
        /// Send audio to the playback device
        /// </summary>
        /// <param name="recv">The audio buffer to play</param>
        public void BufferPlay(byte[] recv)
        {
            provider.AddSamples(recv, 0, recv.Length); //Feed the buffer to the provider
            recv = null; //Remove references to the buffer
        }

        /// <summary>
        /// Release audio playing object
        /// </summary>
        public void Destroy()
        {
            waveOut.Stop(); //Stop playing the audio
            provider.ClearBuffer(); //Clear the audio buffer
            waveOut.Dispose(); //Dispose the player
            waveOut = null; //Remove references to the player
            provider = null; //Remove references to the provider
        }
    }
}
