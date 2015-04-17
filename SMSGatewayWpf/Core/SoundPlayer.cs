using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSGatewayWpf.Core
{
    public class SoundPlayer : IAudioPlayer
    {
        public void Play(string url)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(url);
            player.Play();
        } 

        public void PlayAsync(string url)
        {
            System.Threading.EventWaitHandle wait = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset);
            Task.Factory.StartNew(() =>
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(url);
                player.PlaySync();
                wait.Set();
            }).ContinueWith(x =>
            {
                wait.WaitOne();
            });
        }
    }
}
