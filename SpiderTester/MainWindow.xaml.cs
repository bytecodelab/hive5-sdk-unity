using Hive5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpiderTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Hive5Client apiClient { get; set; }
        public const string ValidUserId = "88197948207226176";
        public static Hive5APIZone TestZone = Hive5APIZone.Beta;
        public const string ValidAppKey = "a40e4122-99d9-44a6-b916-68ed756f79d6";
        public const string Uuid = "747474747";
        public const string GoogleSdkVersion = "3";

        List<string> logs = new List<string>();

        Hive5Spider spider { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            var client = Hive5Client.Instance;
            string appKey = MainWindow.ValidAppKey;
            string uuid = MainWindow.Uuid;

            client.Init(appKey, uuid, MainWindow.TestZone);
            this.apiClient = client;

            spider = new Hive5Spider(client);

            LogListView.ItemsSource = logs;
            PlayerListView.ItemsSource = players;

            Logger.LogOutput += Logger_LogOutput;

            Login();
        }

        void Logger_LogOutput(string log)
        {
            logs.Insert(0, log);
        }

        private void Login()
        {
            string userId = MainWindow.ValidUserId;
            string sdkVersion = MainWindow.GoogleSdkVersion;
            string[] objectKeys = new string[] { "" };		// 로그인 후 가져와야할 사용자 object의 key 목록
            string[] configKeys = new string[] { "time_event1" };	// 로그인 후 가져와야할 사용자 configuration의 key

            try
            {
                this.apiClient.Login(OSType.Android, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, (response) =>
                {
                    spider.Closed += spider_Closed;
                    spider.Error += spider_Error;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void spider_Error(object sender, ErrorMessage error)
        {
            
        }

        void spider_Closed(object sender, EventArgs e)
        {
            
        }

        private void ConnectToggle_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectToggle.IsChecked == true)
            {
                spider.Connect((success) =>
                {
                    if (success == false)
                        ConnectToggle.IsChecked = false;    
                });
            }
            else
            {
                spider.Disconnect((success) =>
                {
                    if (success == false)
                        ConnectToggle.IsChecked = true;                           
                });
            }
        }

        private void UpdateChannelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        List<string> players = new List<string>();

        private void UpdatePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            spider.GetPlayers((success, result) =>
                {
                    players.Clear();

                    GetPlayersResult castedResult = result as GetPlayersResult;

                    foreach (var item in castedResult.PlatformUserIds)
                    {
                        players.Add(item);
                    }
                });
        }

        private void ChannelCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PrivateCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NoticeCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SystemCheck_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
