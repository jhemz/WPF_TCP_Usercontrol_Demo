using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace Demo_Usercontrols.UserControls.SFX
{

    public partial class MediaPlayer : UserControl
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;

        public List<string> Media { get; set; } = new List<string>();

       


        public MediaPlayer()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            GetMediaForPlaylist();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                // every tick, set slider values based on video length
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // open from MyDocuments by default.
            var myDocumentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = myDocumentsDirectory;
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg;*.mkv;*.png;*.mp4;*.jpeg;*.jpg)|*.mp3;*.mpg;*.mpeg;*.mkv;*.png;*.mp4;*.jpeg;*.jpg|All files (*.*)|*.*";
            
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var file in openFileDialog.FileNames)
                {
                    // TODO files must be open from Documents folder which could cause bug if user manually changes directory at file load.
                    // Add clause to check user hasent changed file upload directory
                    var sourcePath = myDocumentsDirectory;
                    var targetPath = @"C:\Users\Chris\Documents\GitHub\WPF_TCP_Usercontrol_Demo\Demo Usercontrols\Media\";
                    var sourceFile = Path.Combine(sourcePath, file);
                    var destFile =   Path.Combine(targetPath, openFileDialog.SafeFileName);
                    File.Copy(sourceFile, destFile, true);
                }
                // reload playlist
                GetMediaForPlaylist();
            }

        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
            //TODO make dynamil
            var targetPath = @"C:\Users\Chris\Documents\GitHub\WPF_TCP_Usercontrol_Demo\Demo Usercontrols\Media\";
            if (lbPlaylist.SelectedItem != null)
            {
                var mediaSource = Path.Combine(targetPath, lbPlaylist.SelectedItem.ToString());
                mePlayer.Source = new Uri(mediaSource);
            }

         
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        public void GetMediaForPlaylist()
        {
            // TODO make dynamic
            var filePath = @"C:\Users\Chris\Documents\GitHub\WPF_TCP_Usercontrol_Demo\Demo Usercontrols\Media\";

            var directoryItems = Directory.GetFiles(filePath, "*.png").Select(Path.GetFileName);
            lbPlaylist.ItemsSource = Directory
                                    .EnumerateFiles(filePath)
                                    .Where(file => file.ToLower().EndsWith("jpg") 
                                    || file.ToLower().EndsWith("png") 
                                    || file.ToLower().EndsWith("wav") 
                                    || file.ToLower().EndsWith("mpg") 
                                    || file.ToLower().EndsWith("mpeg") 
                                    || file.ToLower().EndsWith("mp3")
                                    || file.ToLower().EndsWith("mp4")
                                    || file.ToLower().EndsWith("jpg")
                                    || file.ToLower().EndsWith("jpeg")
                                    ).Select(Path.GetFileName)
                                    .ToList();

        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}
