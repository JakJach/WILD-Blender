using NLog;
using SpotifyAPI.Web;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace WILD.Blender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
            Task.Run(Test);
        }

        private async Task Test()
        {
            try
            {
                var spotify = new SpotifyClient("");

                var track = await spotify.Tracks.Get("");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }

        }
    }
}
