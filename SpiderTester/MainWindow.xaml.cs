using Hive5;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        ObservableCollection<string> logs = new ObservableCollection<string>();
        ObservableCollection<string> messages = new ObservableCollection<string>();

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

            ChannelListView.ItemsSource = channels;
            LogListView.ItemsSource = logs;
            PlayerListView.ItemsSource = players;
            MessageListView.ItemsSource = messages;

            Logger.LogOutput += Logger_LogOutput;

            Login();
        }

        void Logger_LogOutput(string log)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    logs.Insert(0, log);
                }));
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
                    spider.MessageReceived += spider_MessageReceived;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void spider_MessageReceived(object sender, TopicKind topicKind, Dictionary<string, string> mesageContents)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
                       {
                           messages.Add(mesageContents["content"]);
                       }));
        }

        void spider_Error(object sender, ErrorMessage error)
        {
            logs.Add("Error" + error.MessageCodeOfError.ToString());
        }

        void spider_Closed(object sender, EventArgs e)
        {
            logs.Add("Closed");
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

            spider.GetChannels((success, result) =>
                {
                    this.Dispatcher.BeginInvoke((Action)(() =>
                       {
                           channels.Clear();

                           GetChannelsResult castedResult = result as GetChannelsResult;

                           foreach (var item in castedResult.Channels)
                           {
                               channels.Add(string.Format("[{0}]{1}({2})", item.app_id, item.channel_number, item.session_count));
                           }
                       }));
                });
        }

        ObservableCollection<string> players = new ObservableCollection<string>();
        ObservableCollection<string> channels = new ObservableCollection<string>();

        private void UpdatePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            spider.GetPlayers((success, result) =>
                {
                    this.Dispatcher.BeginInvoke((Action)(() =>
                       {
                           players.Clear();

                           GetPlayersResult castedResult = result as GetPlayersResult;

                           foreach (var item in castedResult.PlatformUserIds)
                           {
                               players.Add(item);
                           }
                       }));
                });
        }

        private void ChannelCheck_Click(object sender, RoutedEventArgs e)
        {
            spider.Subscribe(TopicKind.Channel, (success, subscriptionId) =>
                {

                });
        }

        private void PrivateCheck_Click(object sender, RoutedEventArgs e)
        {
            spider.Subscribe(TopicKind.Private, (success, subscriptionId) =>
                {

                });
        }

        private void NoticeCheck_Click(object sender, RoutedEventArgs e)
        {
            spider.Subscribe(TopicKind.Notice, (success, subscriptionId) =>
                {

                });
        }

        private void SystemCheck_Click(object sender, RoutedEventArgs e)
        {
            spider.Subscribe(TopicKind.System, (success, subscriptionId) =>
                {

                });

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = TopicCombo.SelectedItem as ComboBoxItem;

            var contents = new Dictionary<string, string>();
            contents.Add("content", InputBox.Text);

            switch ((string)selected.Tag)
            {
                case "Channel":
                    spider.SendChannelMessage(contents, (success, a) =>
                        {
                        });
                    break;
            }

        }
    }
}
