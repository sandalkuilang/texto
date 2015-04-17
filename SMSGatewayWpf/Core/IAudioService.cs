using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public interface IAudioService
    {
        ReadOnlyCollection<Audio> Audios { get; }
        void Register(Audio audio);
        void Unregister(Audio audio);
        void Play(AudioEnum type);
        void PlayAsync(AudioEnum type);
    }
}
