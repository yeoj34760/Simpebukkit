using utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Simplebukkit
{
    public partial class Form1 : Form
    {
        Process bukkit;
        public Form1()
        {
            InitializeComponent();
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Tools.path(@"\JAR"));
            foreach (var item in di.GetFiles())
            {
                if (comboBox1.Text == "")
                {
                    comboBox1.Text = item.Name;
                }
                comboBox1.Items.Add(item.Name);
            }
            FormClosing += new FormClosingEventHandler(closing);
        }
        private void closing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bukkit.Kill();
            }
            catch (SystemException) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bukkit.StandardInput.WriteLine("stop");
            }
            catch (SystemException) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            start();
            button1.Enabled = false;
        }
        private void start()
        {
            bukkit = new Process();
            bukkit.StartInfo.FileName = "java.exe";
            bukkit.StartInfo.Arguments = "-Djline.terminal=jline.UnsupportedTerminal -Xmx" + Properties.Settings.Default.RAM + "G -Xms" + Properties.Settings.Default.RAM + "G -jar \"" + Tools.path(@"\JAR\") + comboBox1.Text + "\"";
            bukkit.StartInfo.WorkingDirectory = Tools.path(@"\BUKKIT\");
            bukkit.EnableRaisingEvents = true;
            bukkit.StartInfo.CreateNoWindow = true;
            bukkit.StartInfo.UseShellExecute = false;
            bukkit.StartInfo.RedirectStandardError = true;
            bukkit.StartInfo.RedirectStandardInput = true;
            bukkit.StartInfo.RedirectStandardOutput = true;
            bukkit.Exited += new System.EventHandler(ServerExit);
            bukkit.OutputDataReceived += new DataReceivedEventHandler(error_output);
            bukkit.ErrorDataReceived += new DataReceivedEventHandler(error_output);

            bukkit.Start();
            bukkit.BeginErrorReadLine();
            bukkit.BeginOutputReadLine();

        }
        private void ServerExit(object sender, EventArgs e)
        {
            button1.Enabled = true;
            if (Properties.Settings.Default.server_clean == true)
            {
                ppap.Clear();
            }
            ppap.AppendText("삑 서버가 종료된거 같습니다!");
        }
        private void error_output(object sendingProcess, DataReceivedEventArgs e)
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                ppap.AppendText(Environment.NewLine + e.Data);
            }
            catch (SystemException) { }
        }

        private void app_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bukkit.StandardInput.WriteLine(app.Text);
                app.Clear();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var Set = new Setting();
            Set.ShowDialog();
        }
    }
}
