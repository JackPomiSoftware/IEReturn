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
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Claims;

namespace IE_Return
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            RegistryKey startPageKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main");
            HomPag.Text = (string)startPageKey.GetValue("Start Page");

            RegistryKey BHOKeySearch = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Ext\CLSID");
            string BHOKey = BHOKeySearch.GetValue("{1FD49718-1D00-4B19-AF5F-070AF6D5D54C}").ToString();
            if (BHOKey != "0")
            {
                BHOon.Checked = true;
            }
            else
            {
                BHOoff.Checked = true;
            }

            RegistryKey StartIfKeySearch1 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Internet Explorer");
            if (StartIfKeySearch1 == null)
            {
                StartHome.Checked = false;
                StartTabs.Checked = false;
            }
            else
            {
                RegistryKey StartIfKeySearch2 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Internet Explorer\ContinuousBrowsing");
                if (StartIfKeySearch2 == null)
                {
                    StartHome.Checked = false;
                    StartTabs.Checked = false;
                }
                else
                {
                    RegistryKey StartIfKeySearch3 = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Internet Explorer\ContinuousBrowsing");
                    var StartKey3 = StartIfKeySearch3.GetValue("Enabled");
                    if (StartKey3 == null)
                    {
                        StartHome.Checked = false;
                        StartTabs.Checked = false;
                    }
                    else
                    {
                        RegistryKey StartKeySearch = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Internet Explorer\ContinuousBrowsing");
                        string StartKey = StartKeySearch.GetValue("Enabled").ToString();
                        if (StartKey == "0")
                        {
                            StartHome.Checked = true;
                        }
                        if (StartKey == "1")
                        {
                            StartTabs.Checked = true;
                        }
                    }
                }
            }
            ApplyStartup.Enabled = false;
            applyBHO.Enabled = false;
        }

        private void EDopen_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("msedge.exe");

        }
        private void IEopen_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("ie.vbs");
        }

        private void IEexe_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe");
        }

        private void applyBHO_Click(object sender, EventArgs e)
        {
            if (BHOon.Checked == true)
            {
                RegistryKey BHOKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Ext\CLSID", true);
                BHOKey.SetValue("{1FD49718-1D00-4B19-AF5F-070AF6D5D54C}", "1");
                //System.Diagnostics.Process.Start("UseEdge.reg");
            }
            if (BHOoff.Checked == true)
            {
                RegistryKey BHOKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Ext\CLSID", true);
                BHOKey.SetValue("{1FD49718-1D00-4B19-AF5F-070AF6D5D54C}", "0");
                //System.Diagnostics.Process.Start("UseIE.reg");
            }
            applyBHO.Enabled = false;
        }

        private void inetcpl_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("inetcpl.cpl");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JackPomiSoftware/IEReturn");
        }

        private void usedef_Click(object sender, EventArgs e)
        {
            RegistryKey startPageKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main", true);
            startPageKey.SetValue("Start Page", "https://www.msn.com");
            startPageKey.Close();
            RegistryKey startPageKey1 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main");
            HomPag.Text = (string)startPageKey1.GetValue("Start Page");
        }

        private void usebla_Click(object sender, EventArgs e)
        {
            RegistryKey startPageKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main", true);
            startPageKey.SetValue("Start Page", "about:blank");
            startPageKey.Close();
            RegistryKey startPageKey1 = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main");
            HomPag.Text = (string)startPageKey1.GetValue("Start Page");
        }

        private void usecur_Click(object sender, EventArgs e)
        {
            RegistryKey startPageKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main", true);
            startPageKey.SetValue("Start Page", HomPag.Text);
            startPageKey.Close();
        }

        private void StartTabs_CheckedChanged(object sender, EventArgs e)
        {
            ApplyStartup.Enabled = true;
        }

        private void StartHome_CheckedChanged(object sender, EventArgs e)
        {
            ApplyStartup.Enabled = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe",@".\help.txt");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://jackpomisoftware.github.io/ier.html");
        }

        private void ApplyStartup_Click(object sender, EventArgs e)
        {
            if (StartHome.Checked == true)
            {
                RegistryKey StartKeySearch = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Internet Explorer\ContinuousBrowsing");
                StartKeySearch.SetValue("Enabled", "0");
                StartKeySearch.Close();
            }
            if (StartTabs.Checked == true)
            {
                RegistryKey StartKeySearch = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Internet Explorer\ContinuousBrowsing");
                StartKeySearch.SetValue("Enabled", "1");
                StartKeySearch.Close();
            }
            ApplyStartup.Enabled = false;
        }

        private void BHOon_CheckedChanged(object sender, EventArgs e)
        {
            applyBHO.Enabled = true;
        }

        private void BHOoff_CheckedChanged(object sender, EventArgs e)
        {
            applyBHO.Enabled = true;
        }
    }
}
