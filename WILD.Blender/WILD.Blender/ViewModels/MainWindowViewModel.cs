using Caliburn.Micro;
using System.Threading.Tasks;
using WILD.Blender.Helpers;
using WILD.Blender.Models;

namespace WILD.Blender.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        private BindableCollection<TrackModel> _tracks = new();
        private TrackModel? _selectedTrack;

        public TrackModel SelectedTrack
        {
            get { return _selectedTrack ?? new TrackModel(); }
            set { _selectedTrack = value; }
        }

        public BindableCollection<TrackModel> Tracks
        {
            get { return _tracks; }
            set { _tracks = value; }
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            Task.Run(GetTrack);
        }

        private async Task GetTrack()
        {
            var helper = SpotifyClientHelper.GetInstance();

            var clientTrack = await helper.SpotifyClient.Tracks.Get("38EMs44DpiWdDEk246yt8T");

            var track = new TrackModel(clientTrack);

            Tracks.Add(track);
            SelectedTrack = track;
        }
    }
}