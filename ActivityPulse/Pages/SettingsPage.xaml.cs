using ActivityPulse.Settings;
using ActivityPulse.Utils;
using ActivityPulse.Windows;
using PLP_SystemInfo.ComponentInfo;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ActivityPulse.Pages
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public string about = "ActivityPulse - Keep an eye on your digital rhythm! Record your app and system usage and discover exciting insights\nMore information can be found on the website.";
        public string lizenz = "This product was provided by Adeptstack. Use only for private purposes, no rental or resale or similar! \nSee license information. \n© Adeptstack, All rights reserved!\n";

        public SettingsPage()
        {
            InitializeComponent();
            MainWindow.UpdateTitle("ActivityPulse - Settings");

            SP_Custom.Visibility = Visibility.Visible;
            SP_LC.Visibility = Visibility.Collapsed;
            SP_IH.Visibility = Visibility.Collapsed;
            customi_btn.IsChecked = true;

            if (Settings.AppSettings.Default.ThemeMode == -1)
            {
                CB_Mode.SelectedIndex = 0;
            }
            else
            {
                if (Settings.AppSettings.Default.ThemeMode == 0)
                {
                    CB_Mode.SelectedIndex = 1;
                }
                else if (Settings.AppSettings.Default.ThemeMode == 1)
                {
                    CB_Mode.SelectedIndex = 2;
                }
                else if (Settings.AppSettings.Default.ThemeMode == 2)
                {
                    CB_Mode.SelectedIndex = Settings.AppSettings.Default.ThemeID + 3;
                }
            }

            CB_UpdateChannel.SelectedIndex = AppSettings.Default.UpdateChannel;

            var processor = ProcessorInfo.GetProcessors();
            LBL_About_Text.Text = about;
            LBL_Lizenz_Text.Text = lizenz;
            LBL_Version.Text = $"App Version: {AppUtils.apVersion}\nHost Version: {AppUtils.ahVersion}\n";
            LBL_OS_Name.Text = "Operatingsystem: " + OSInfo.GetOperatingSystemInfo();
            LBL_CPU_Name.Text = "Processor: " + processor[0].Name + "\nThreads: " + processor[0].Threads + "\nRAM: " + RamInfo.GetInstalledRAMSize() + "GB";

            LB_LC.Items.Add("Imprint");
            LB_LC.Items.Add("License");
            LB_LC.Items.Add("Privacy");
            LB_LC.Items.Add("Microsoft.Web.WebView2");
            LB_LC.Items.Add("Ookii.Dialogs");
            LB_LC.Items.Add("PLP-SystemInfo");
            LB_LC.SelectedIndex = 3;
        }

        private void customi_btn_Click(object sender, RoutedEventArgs e)
        {
            SP_Custom.Visibility = Visibility.Visible;
            SP_LC.Visibility = Visibility.Collapsed;
            SP_IH.Visibility = Visibility.Collapsed;
        }

        private void data_btn_Click(object sender, RoutedEventArgs e)
        {
            SP_Custom.Visibility = Visibility.Collapsed;
            SP_LC.Visibility = Visibility.Collapsed;
            SP_IH.Visibility = Visibility.Collapsed;
        }

        private void lc_btn_Click(object sender, RoutedEventArgs e)
        {
            SP_Custom.Visibility = Visibility.Collapsed;
            SP_LC.Visibility = Visibility.Visible;
            SP_IH.Visibility = Visibility.Collapsed;
        }

        private void ih_btn_Click(object sender, RoutedEventArgs e)
        {
            SP_Custom.Visibility = Visibility.Collapsed;
            SP_LC.Visibility = Visibility.Collapsed;
            SP_IH.Visibility = Visibility.Visible;
        }


        //Feedback & Support

        private void website_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://adeptstack.vercel.app") { UseShellExecute = true });
        }

        private void feedback_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://forms.office.com/r/ie6wgVHntL") { UseShellExecute = true });
        }

        private void changelog_Click(object sender, RoutedEventArgs e)
        {
            ChangelogWindow changelogWindow = new ChangelogWindow();
            changelogWindow.Show();
        }

        private void CB_Mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Themes.SetMode(CB_Mode.SelectedIndex);
        }

        private void LB_LC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB_LC.SelectedIndex == 0)
            {
                Process.Start(new ProcessStartInfo("https://adeptstack.vercel.app/imprint") { UseShellExecute = true });
            }
            else if (LB_LC.SelectedIndex == 1)
            {
                Process.Start(new ProcessStartInfo("https://adeptstack.vercel.app/license") { UseShellExecute = true });
            }
            else if (LB_LC.SelectedIndex == 2)
            {
                Process.Start(new ProcessStartInfo("https://adeptstack.vercel.app/privacy") { UseShellExecute = true });
            }
            else if (LB_LC.SelectedIndex == 3)
            {
                TB_LC.Text = "Copyright (C) Microsoft Corporation. All rights reserved.\r\n\r\nRedistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:\r\n\r\n   * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.\n\r\r\n   * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.\r\n\r\n   * The name of Microsoft Corporation, or the names of its contributors may not be used to endorse or promote products derived from this software without specific prior written permission.\r\n\r\nTHIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS \"AS IS\" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.";
            }
            else if (LB_LC.SelectedIndex == 4)
            {
                TB_LC.Text = "BSD 3-Clause License\r\n\r\nCopyright (c) C. Augusto Proiete 2018-2021\r\nCopyright (c) Sven Groot         2009-2018\r\nAll rights reserved.\r\n\r\nRedistribution and use in source and binary forms, with or without\r\nmodification, are permitted provided that the following conditions are met:\r\n\r\n1. Redistributions of source code must retain the above copyright notice, this\r\n   list of conditions and the following disclaimer.\r\n\r\n2. Redistributions in binary form must reproduce the above copyright notice,\r\n   this list of conditions and the following disclaimer in the documentation\r\n   and/or other materials provided with the distribution.\r\n\r\n3. Neither the name of the copyright holder nor the names of its\r\n   contributors may be used to endorse or promote products derived from\r\n   this software without specific prior written permission.\r\n\r\nTHIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS \"AS IS\"\r\nAND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE\r\nIMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE\r\nDISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE\r\nFOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL\r\nDAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR\r\nSERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER\r\nCAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,\r\nOR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE\r\nOF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.";
            }
            else if (LB_LC.SelectedIndex == 5)
            {
                TB_LC.Text = "Apache License\r\n                           Version 2.0, January 2004\r\n                        http://www.apache.org/licenses/\r\n\r\n   TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION\r\n\r\n   1. Definitions.\r\n\r\n      \"License\" shall mean the terms and conditions for use, reproduction,\r\n      and distribution as defined by Sections 1 through 9 of this document.\r\n\r\n      \"Licensor\" shall mean the copyright owner or entity authorized by\r\n      the copyright owner that is granting the License.\r\n\r\n      \"Legal Entity\" shall mean the union of the acting entity and all\r\n      other entities that control, are controlled by, or are under common\r\n      control with that entity. For the purposes of this definition,\r\n      \"control\" means (i) the power, direct or indirect, to cause the\r\n      direction or management of such entity, whether by contract or\r\n      otherwise, or (ii) ownership of fifty percent (50%) or more of the\r\n      outstanding shares, or (iii) beneficial ownership of such entity.\r\n\r\n      \"You\" (or \"Your\") shall mean an individual or Legal Entity\r\n      exercising permissions granted by this License.\r\n\r\n      \"Source\" form shall mean the preferred form for making modifications,\r\n      including but not limited to software source code, documentation\r\n      source, and configuration files.\r\n\r\n      \"Object\" form shall mean any form resulting from mechanical\r\n      transformation or translation of a Source form, including but\r\n      not limited to compiled object code, generated documentation,\r\n      and conversions to other media types.\r\n\r\n      \"Work\" shall mean the work of authorship, whether in Source or\r\n      Object form, made available under the License, as indicated by a\r\n      copyright notice that is included in or attached to the work\r\n      (an example is provided in the Appendix below).\r\n\r\n      \"Derivative Works\" shall mean any work, whether in Source or Object\r\n      form, that is based on (or derived from) the Work and for which the\r\n      editorial revisions, annotations, elaborations, or other modifications\r\n      represent, as a whole, an original work of authorship. For the purposes\r\n      of this License, Derivative Works shall not include works that remain\r\n      separable from, or merely link (or bind by name) to the interfaces of,\r\n      the Work and Derivative Works thereof.\r\n\r\n      \"Contribution\" shall mean any work of authorship, including\r\n      the original version of the Work and any modifications or additions\r\n      to that Work or Derivative Works thereof, that is intentionally\r\n      submitted to Licensor for inclusion in the Work by the copyright owner\r\n      or by an individual or Legal Entity authorized to submit on behalf of\r\n      the copyright owner. For the purposes of this definition, \"submitted\"\r\n      means any form of electronic, verbal, or written communication sent\r\n      to the Licensor or its representatives, including but not limited to\r\n      communication on electronic mailing lists, source code control systems,\r\n      and issue tracking systems that are managed by, or on behalf of, the\r\n      Licensor for the purpose of discussing and improving the Work, but\r\n      excluding communication that is conspicuously marked or otherwise\r\n      designated in writing by the copyright owner as \"Not a Contribution.\"\r\n\r\n      \"Contributor\" shall mean Licensor and any individual or Legal Entity\r\n      on behalf of whom a Contribution has been received by Licensor and\r\n      subsequently incorporated within the Work.\r\n\r\n   2. Grant of Copyright License. Subject to the terms and conditions of\r\n      this License, each Contributor hereby grants to You a perpetual,\r\n      worldwide, non-exclusive, no-charge, royalty-free, irrevocable\r\n      copyright license to reproduce, prepare Derivative Works of,\r\n      publicly display, publicly perform, sublicense, and distribute the\r\n      Work and such Derivative Works in Source or Object form.\r\n\r\n   3. Grant of Patent License. Subject to the terms and conditions of\r\n      this License, each Contributor hereby grants to You a perpetual,\r\n      worldwide, non-exclusive, no-charge, royalty-free, irrevocable\r\n      (except as stated in this section) patent license to make, have made,\r\n      use, offer to sell, sell, import, and otherwise transfer the Work,\r\n      where such license applies only to those patent claims licensable\r\n      by such Contributor that are necessarily infringed by their\r\n      Contribution(s) alone or by combination of their Contribution(s)\r\n      with the Work to which such Contribution(s) was submitted. If You\r\n      institute patent litigation against any entity (including a\r\n      cross-claim or counterclaim in a lawsuit) alleging that the Work\r\n      or a Contribution incorporated within the Work constitutes direct\r\n      or contributory patent infringement, then any patent licenses\r\n      granted to You under this License for that Work shall terminate\r\n      as of the date such litigation is filed.\r\n\r\n   4. Redistribution. You may reproduce and distribute copies of the\r\n      Work or Derivative Works thereof in any medium, with or without\r\n      modifications, and in Source or Object form, provided that You\r\n      meet the following conditions:\r\n\r\n      (a) You must give any other recipients of the Work or\r\n          Derivative Works a copy of this License; and\r\n\r\n      (b) You must cause any modified files to carry prominent notices\r\n          stating that You changed the files; and\r\n\r\n      (c) You must retain, in the Source form of any Derivative Works\r\n          that You distribute, all copyright, patent, trademark, and\r\n          attribution notices from the Source form of the Work,\r\n          excluding those notices that do not pertain to any part of\r\n          the Derivative Works; and\r\n\r\n      (d) If the Work includes a \"NOTICE\" text file as part of its\r\n          distribution, then any Derivative Works that You distribute must\r\n          include a readable copy of the attribution notices contained\r\n          within such NOTICE file, excluding those notices that do not\r\n          pertain to any part of the Derivative Works, in at least one\r\n          of the following places: within a NOTICE text file distributed\r\n          as part of the Derivative Works; within the Source form or\r\n          documentation, if provided along with the Derivative Works; or,\r\n          within a display generated by the Derivative Works, if and\r\n          wherever such third-party notices normally appear. The contents\r\n          of the NOTICE file are for informational purposes only and\r\n          do not modify the License. You may add Your own attribution\r\n          notices within Derivative Works that You distribute, alongside\r\n          or as an addendum to the NOTICE text from the Work, provided\r\n          that such additional attribution notices cannot be construed\r\n          as modifying the License.\r\n\r\n      You may add Your own copyright statement to Your modifications and\r\n      may provide additional or different license terms and conditions\r\n      for use, reproduction, or distribution of Your modifications, or\r\n      for any such Derivative Works as a whole, provided Your use,\r\n      reproduction, and distribution of the Work otherwise complies with\r\n      the conditions stated in this License.\r\n\r\n   5. Submission of Contributions. Unless You explicitly state otherwise,\r\n      any Contribution intentionally submitted for inclusion in the Work\r\n      by You to the Licensor shall be under the terms and conditions of\r\n      this License, without any additional terms or conditions.\r\n      Notwithstanding the above, nothing herein shall supersede or modify\r\n      the terms of any separate license agreement you may have executed\r\n      with Licensor regarding such Contributions.\r\n\r\n   6. Trademarks. This License does not grant permission to use the trade\r\n      names, trademarks, service marks, or product names of the Licensor,\r\n      except as required for reasonable and customary use in describing the\r\n      origin of the Work and reproducing the content of the NOTICE file.\r\n\r\n   7. Disclaimer of Warranty. Unless required by applicable law or\r\n      agreed to in writing, Licensor provides the Work (and each\r\n      Contributor provides its Contributions) on an \"AS IS\" BASIS,\r\n      WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or\r\n      implied, including, without limitation, any warranties or conditions\r\n      of TITLE, NON-INFRINGEMENT, MERCHANTABILITY, or FITNESS FOR A\r\n      PARTICULAR PURPOSE. You are solely responsible for determining the\r\n      appropriateness of using or redistributing the Work and assume any\r\n      risks associated with Your exercise of permissions under this License.\r\n\r\n   8. Limitation of Liability. In no event and under no legal theory,\r\n      whether in tort (including negligence), contract, or otherwise,\r\n      unless required by applicable law (such as deliberate and grossly\r\n      negligent acts) or agreed to in writing, shall any Contributor be\r\n      liable to You for damages, including any direct, indirect, special,\r\n      incidental, or consequential damages of any character arising as a\r\n      result of this License or out of the use or inability to use the\r\n      Work (including but not limited to damages for loss of goodwill,\r\n      work stoppage, computer failure or malfunction, or any and all\r\n      other commercial damages or losses), even if such Contributor\r\n      has been advised of the possibility of such damages.\r\n\r\n   9. Accepting Warranty or Additional Liability. While redistributing\r\n      the Work or Derivative Works thereof, You may choose to offer,\r\n      and charge a fee for, acceptance of support, warranty, indemnity,\r\n      or other liability obligations and/or rights consistent with this\r\n      License. However, in accepting such obligations, You may act only\r\n      on Your own behalf and on Your sole responsibility, not on behalf\r\n      of any other Contributor, and only if You agree to indemnify,\r\n      defend, and hold each Contributor harmless for any liability\r\n      incurred by, or claims asserted against, such Contributor by reason\r\n      of your accepting any such warranty or additional liability.\r\n\r\n   END OF TERMS AND CONDITIONS\r\n\r\n   APPENDIX: How to apply the Apache License to your work.\r\n\r\n      To apply the Apache License to your work, attach the following\r\n      boilerplate notice, with the fields enclosed by brackets \"[]\"\r\n      replaced with your own identifying information. (Don't include\r\n      the brackets!)  The text should be enclosed in the appropriate\r\n      comment syntax for the file format. We also recommend that a\r\n      file or class name and description of purpose be included on the\r\n      same \"printed page\" as the copyright notice for easier\r\n      identification within third-party archives.\r\n\r\n   Copyright [2023] [ProgrammerLP]\r\n\r\n   Licensed under the Apache License, Version 2.0 (the \"License\");\r\n   you may not use this file except in compliance with the License.\r\n   You may obtain a copy of the License at\r\n\r\n       http://www.apache.org/licenses/LICENSE-2.0\r\n\r\n   Unless required by applicable law or agreed to in writing, software\r\n   distributed under the License is distributed on an \"AS IS\" BASIS,\r\n   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.\r\n   See the License for the specific language governing permissions and\r\n   limitations under the License.";
            }
        }

        private void CB_UpdateChannel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_UpdateChannel.SelectedIndex >= 0)
            {
                AppSettings.Default.UpdateChannel = CB_UpdateChannel.SelectedIndex;
                Settings.AppSettings.Default.Save();
            }
        }

        //private void btnSetStoragePath_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(tbStoragePath.Text))
        //    {
        //        if (Directory.Exists(tbStoragePath.Text))
        //        {
        //            TellBox tb = new TellBox("Are you sure you want to change the location? If you do, make sure your notes are backed up offsite in case something goes wrong with the change, such as a network drive becoming unavailable.", "Warning");
        //            bool? result = tb.ShowDialog();

        //            if (result == true)
        //            {
        //                string oldPath = Settings.AppSettings.Default.UserDataLocation;
        //                Settings.AppSettings.Default.UserDataLocation = tbStoragePath.Text;
        //                Settings.AppSettings.Default.Save();
        //                Data.CopyDirectory(oldPath + @"\ActivityPulse", Settings.AppSettings.Default.UserDataLocation + @"\ActivityPulse", true);
        //            }
        //        }
        //        else
        //        {
        //            TellBox tb = new TellBox("The specified file path does not exist", "Error");
        //            tb.ShowDialog();
        //        }
        //    }
        //    else
        //    {
        //        TellBox tb = new TellBox("The file path cannot be empty.", "Error");
        //        tb.ShowDialog();
        //    }
        //}

        //private void search_storagepath_btn_Click(object sender, RoutedEventArgs e)
        //{
        //    Ookii.Dialogs.Wpf.VistaFolderBrowserDialog fbd = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
        //    fbd.RootFolder = Environment.SpecialFolder.MyDocuments;
        //    fbd.ShowNewFolderButton = true;

        //    bool? result = fbd.ShowDialog();

        //    if (result == true)
        //    {
        //        tbStoragePath.Text = fbd.SelectedPath;
        //    }
        //}
    }
}
