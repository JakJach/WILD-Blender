using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Linq;

namespace WILD.Blender.Models
{
    public class TrackModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public List<string>? FeaturedArtists { get; set; }
        public string Album { get; set; }
        public bool Explicit { get; set; }
        public bool IsPlayable { get; set; }
        public int Popularity { get; set; }

        public TrackModel()
        {
            Id = string.Empty; 
            Title = string.Empty; 
            Artist = string.Empty; 
            FeaturedArtists = new List<string>(); 
            Album = string.Empty;
            Explicit = false;
            IsPlayable = false;
            Popularity = 0;
        }

        public TrackModel(FullTrack fullTrack)
        {
            Id = fullTrack.Id;
            Title = fullTrack.Name;
            Artist = fullTrack.Artists.FirstOrDefault().Name ?? "UNKNOWN";

            FeaturedArtists = new List<string>();
            foreach (var artist in fullTrack.Artists)
            {
                if (artist.Name != Artist)
                {
                    FeaturedArtists.Add(artist.Name);
                }
            }
            Album = fullTrack.Album.Name;
            Explicit = fullTrack.Explicit;
            IsPlayable = fullTrack.IsPlayable;
            Popularity = fullTrack.Popularity;
        }
    }
}
