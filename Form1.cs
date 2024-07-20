using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameHub_Installer_V2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeNotifyIcon();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var fileMappings = new Dictionary<CheckBox, string>
    {
        { checkBox6, "SpotifySetup.exe" },
        { checkBox2, "EpicGamesLauncherInstaller.msi" },
        { checkBox1, "SteamSetup.exe" }, // Update to a valid Steam URL
        { checkBox12, "EAappInstaller.exe" },
        { checkBox11, "UbisoftConnectInstaller.exe" },
        { checkBox3, "DiscordSetup.exe" },
        { checkBox4, "OBS-Studio-30.2.0-Windows-Installer.exe" },
        { checkBox5, "OperaSetup.exe" },
        { checkBox16, "ValorantSetup.exe" },
        { checkBox10, "BattleNetSetup.exe" },
        { checkBox7, "GeForceExperience.exe" },
        { checkBox8, "AMDSoftware.exe" },
        { checkBox9, "WinRARSetup.exe" },
        { checkBox19, "MalwarebytesSetup.exe" },
        { checkBox20, "ChromeSetup.exe" },
        { checkBox24, "OverwolfInstaller.exe" },
        { checkBox27, "7z2407-x64.exe" },
        { checkBox15, "Cinebench2024_win_x86_64.zip" },
        { checkBox14, "Geeks3DSetup.exe" },
        { checkBox13, "HWMonitorSetup.exe" },
        { checkBox17, "CoreTempSetup.exe" },
        { checkBox18, "FanControlSetup.exe" },
        { checkBox21, "RyzenMasterSetup.exe" },
        { checkBox22, "MSIAfterburnerSetup.zip" },
        { checkBox25, "VSCodeUserSetup-x64-1.91.1.exe" },
        { checkBox26, "VS2022Setup.exe" }
    };

            var urls = new Dictionary<string, string>
    {
        { "SpotifySetup.exe", "https://download.scdn.co/SpotifySetup.exe" },
        { "EpicGamesLauncherInstaller.msi", "https://launcher-public-service-prod06.ol.epicgames.com/launcher/api/installer/download/EpicGamesLauncherInstaller.msi" },
        { "SteamSetup.exe", "https://cdn.akamai.steamstatic.com/client/installer/SteamSetup.exe" }, // Update to a correct Steam URL
        { "EAappInstaller.exe", "https://origin-a.akamaihd.net/EA-Desktop-Client-Download/installer-releases/EAappInstaller.exe" },
        { "UbisoftConnectInstaller.exe", "https://static3.cdn.ubi.com/orbit/launcher_installer/UbisoftConnectInstaller.exe" },
        { "DiscordSetup.exe", "https://discord.com/api/downloads/distributions/app/installers/latest?channel=stable&platform=win&arch=x64" },
        { "OBS-Studio-30.2.0-Windows-Installer.exe", "https://cdn-fastly.obsproject.com/downloads/OBS-Studio-30.2.0-Windows-Installer.exe" },
        { "OperaSetup.exe", "https://www.opera.com/computer/thanks?ni=eapgx&os=windows" },
        { "ValorantSetup.exe", "https://valorant.secure.dyn.riotcdn.net/channels/public/x/installer/current/live.live.ap.exe" },
        { "BattleNetSetup.exe", "https://downloader.battle.net/download/getInstaller?os=win&installer=Battle.net-Setup.exe" },
        { "GeForceExperience.exe", "https://uk.download.nvidia.com/GFE/GFEClient/3.28.0.417/GeForce_Experience_v3.28.0.417.exe" },
        { "AMDSoftware.exe", "https://drivers.amd.com/drivers/installer/24.10/whql/amd-software-adrenalin-edition-24.7.1-minimalsetup-240718_web.exe" },
        { "WinRARSetup.exe", "https://www.win-rar.com/predownload.html?&L=0" },
        { "MalwarebytesSetup.exe", "https://www.malwarebytes.com/mwb-download/thankyou" },
        { "ChromeSetup.exe", "https://dl.google.com/tag/s/appguid%3D%7B8A69D345-D564-463C-AFF1-A69D9E530F96%7D%26iid%3D%7B0FE355DE-AB08-7A1F-7706-7688623EBFFB%7D%26lang%3Den-GB%26browser%3D5%26usagestats%3D1%26appname%3DGoogle%2520Chrome%26needsadmin%3Dprefers%26ap%3Dx64-statsdef_1%26installdataindex%3Dempty/update2/installers/ChromeSetup.exe" },
        { "OverwolfInstaller.exe", "https://download.overwolf.com/installer/prod/6410b22e5473313b13508ee60a01fa46/OverwolfInstaller.exe" },
        { "7z2407-x64.exe", "https://objects.githubusercontent.com/github-production-release-asset-2e65be/466446150/031b16d5-30d3-42fe-a8aa-4e9b7d7c7f20?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=releaseassetproduction%2F20240720%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20240720T134558Z&X-Amz-Expires=300&X-Amz-Signature=bbedbf9b631e57c42a53853332c6ebdc00abbcf4cb6e4edc3f07ea5dba5c6760&X-Amz-SignedHeaders=host&actor_id=160979076&key_id=0&repo_id=466446150&response-content-disposition=attachment%3B%20filename%3D7z2407-x64.exe&response-content-type=application%2Foctet-stream" },
        { "Cinebench2024_win_x86_64.zip", "https://mx-app-blob-prod.maxon.net/mx-package-production/website/windows/maxon/cinebench/Cinebench2024_win_x86_64.zip" },
        { "Geeks3DSetup.exe", "https://www.geeks3d.com/dl/show/755" },
        { "HWMonitorSetup.exe", "https://www.cpuid.com/downloads/hwmonitor/hwmonitor_1.54.exe" },
        { "CoreTempSetup.exe", "https://www.alcpu.com/CoreTemp/Core-Temp-setup-v1.18.1.0.exe" },
        { "FanControlSetup.exe", "https://github.com/Rem0o/FanControl.Releases/releases/tag/V196" },
        { "RyzenMasterSetup.exe", "https://download.amd.com/Desktop/amd-ryzen-master.exe" },
        { "MSIAfterburnerSetup.zip", "https://download.msi.com/uti_exe/vga/MSIAfterburnerSetup.zip?__token__=exp=1721639424~acl=/*~hmac=241436d396dcc68a41489f50ba2b8ef9cc1aef4b9ceeb9083828315f0976c565" },
        { "VSCodeUserSetup-x64-1.91.1.exe", "https://vscode.download.prss.microsoft.com/dbazure/download/stable/f1e16e1e6214d7c44d078b1f0607b2388f29d729/VSCodeUserSetup-x64-1.91.1.exe" },
        { "VS2022Setup.exe", "https://c2rsetup.officeapps.live.com/c2r/downloadVS.aspx?sku=community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030:c764d91d8872494280437916fd20e627" }
    };

            string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string gameHubFolder = Path.Combine(downloadsFolder, "GameHub Installer");

            try
            {
                // Create the GameHub Installer folder if it doesn't exist
                if (!Directory.Exists(gameHubFolder))
                {
                    Directory.CreateDirectory(gameHubFolder);
                }

                // Determine the number of files to download
                int fileCount = fileMappings.Count(m => m.Key.Checked);
                var filesToDownload = fileMappings.Where(m => m.Key.Checked).ToDictionary(m => m.Key, m => m.Value);
                int filesLeft = filesToDownload.Count;
                if (fileCount > 0)
                {
                    MessageBox.Show(
                        $"You have selected {fileCount} file(s) to download. Depending on your internet speed, this process may take some time.",
                        "Download Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                foreach (var mapping in fileMappings)
                {
                    if (mapping.Key.Checked)
                    {
                        string fileName = mapping.Value;
                        string url = urls[fileName];
                        string destinationPath = Path.Combine(gameHubFolder, fileName);

                        // Log the file name and URL
                        Console.WriteLine($"Downloading {fileName} from {url}");

                        // Call the async method to download the file
                        await DownloadFileAsync(url, destinationPath);
                        Console.WriteLine($"Download complete for {fileName}!");

                        filesLeft--;
                        string message = filesLeft > 0
                            ? $"Download complete for {fileName}! {filesLeft} file(s) left."
                            : $"Download complete for {fileName}! file(s) have been downloaded. Check your downloads folder for the GameHub folder";

                        // Show system tray notification after each download
                        ShowSystemTrayNotification(message);
                    }
                }

                // Open the GameHub Installer folder
                Process.Start(new ProcessStartInfo
                {
                    FileName = gameHubFolder,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        // Async method to download the file
        static async Task DownloadFileAsync(string url, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {
                // Send a GET request to the specified URL
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    // Check if the request was successful
                    response.EnsureSuccessStatusCode();

                    // Read the content as a byte array
                    byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                    // Write the byte array to the destination file using FileStream
                    using (FileStream fs = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                    {
                        await fs.WriteAsync(fileBytes, 0, fileBytes.Length);
                    }
                }
            }
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon1 = new NotifyIcon
            {
                Icon = SystemIcons.Information, // You can set a custom icon here
                Visible = true
            };
        }
        private void ShowSystemTrayNotification(string message)
        {
            notifyIcon1.BalloonTipTitle = "Download Complete";
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.ShowBalloonTip(3000); // Show for 3 seconds
        }
    }
}
