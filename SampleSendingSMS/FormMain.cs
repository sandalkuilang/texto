using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using Cryptography;

namespace SampleSendingSMS
{
    public partial class FormMain : Form
    {

        private GSMClient.IClient gsmClient;
        private ICollection<string> command;
        private Crypter c = new Cryptography.Crypter(new DefaultConfigurationKey());

        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxNumber.Text))
            {
                /*
                 * Command Format Sending SMS 
                 * --------------------------
                 *      ISMS.Send(+param: number={0} +param: message={1})
                 *      
                 * e.g. ISMS.Send(+param: number=081818181 +param: message=lorem ipsum)
                 * --------------------------
                 */
                string formattedCommand = string.Format(command.Get("ISMS.Send"), textBoxNumber.Text, textBoxMessage.Text);
                for (int i = 0; i < numericUpDownMultiple.Value; i++)
                {
                    Thread.Sleep(250);
                    gsmClient.Send(formattedCommand);
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            gsmClient = new GSMClient.Client(Properties.Settings.Default.SMSGatewayIP, Properties.Settings.Default.SMSGatewayPort);
            gsmClient.Open();
            command = new CommandCollection();
            Thread threadRead = new Thread(new ParameterizedThreadStart(Read));
            threadRead.Start(gsmClient.GetTcpClient());
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            gsmClient.Close();
        }

        private void Read(object target)
        {
            TcpClient client = (TcpClient)target;
            NetworkStream stream = client.GetStream();
            while (true)
            {
                if (!client.Connected)
                    break;
                byte[] read = new byte[client.ReceiveBufferSize];
                int readInt;
                try
                {
                    readInt = stream.Read(read, 0, read.Length);
                    string decrypt = c.Decrypt(ASCIIEncoding.ASCII.GetString(read).Replace("\0", ""));
                    UpdateStatus(decrypt);
                }
                catch
                {
                    return;
                }
            }
        }

        private delegate void delegateUpdateStatus(string value);
        private void UpdateStatus(string value)
        {
            if (this.InvokeRequired)
            {
                delegateUpdateStatus O = new delegateUpdateStatus(UpdateStatus);
                this.Invoke(O, new object[] { value });
            }
            else
            {
                if (value.Contains(textBoxNumber.Text))
                {
                    labelStatus.Text = "Successfully";
                }
            }
        }

    }
}
