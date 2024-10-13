using Android.Media.Metrics;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class SoundManager
    {
        public Dictionary<string, SoundEffect> _sounds = new Dictionary<string, SoundEffect>();
        public int[] channelVolumes;

        public SoundEffectInstance PlaySoundInstance(string _soundName, float volume, float pan, int channel)
        {
            var instance = _sounds[_soundName].CreateInstance();
            instance.Volume = volume * channelVolumes[channel];
            instance.Pan = pan;
            instance.Play();
            return instance;
        }
    }
}
