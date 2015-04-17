using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public class DefaultAudioService : IAudioService
    {

        private List<Audio> bags; 

        public System.Collections.ObjectModel.ReadOnlyCollection<Audio> Audios
        {
            get { return new ReadOnlyCollection<Audio>(bags.ToList()); }
        }

        public DefaultAudioService()
        {
            bags = new List<Audio>();
        }

        public void Register(Audio audio)
        {
            bags.Add(audio);
        }

        public void Unregister(Audio audio)
        {
            bags.Remove(audio);
        }

        public void Play(AudioEnum type)
        {
            Audio audio = GetAudio(type); 
            if (audio != null)
            {
                IAudioPlayer player = new SoundPlayer();
                player.Play(audio.Url);
            }
        }
         
        public void PlayAsync(AudioEnum type)
        {
            Audio audio = GetAudio(type);
            if (audio != null)
            {
                IAudioPlayer player = new SoundPlayer();
                player.PlayAsync(audio.Url);
            }
        }

        protected Audio GetAudio(AudioEnum type)
        {
            Audio audio = null;
            for (int i = 0; i < bags.Count; i++)
            {
                if (bags[i].Type == type)
                {
                    audio = bags[i];
                    break;
                }
            }
            return audio;
        }
    }
}
