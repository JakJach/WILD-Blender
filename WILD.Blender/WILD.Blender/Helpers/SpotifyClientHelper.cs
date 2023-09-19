using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web;
using System;
using System.Configuration;
using System.Threading.Tasks;
using NLog;

namespace WILD.Blender.Helpers
{
    public sealed class SpotifyClientHelper
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private static readonly string? _clientId = ConfigurationManager.AppSettings["ClientId"];
        private static readonly string? _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];

        private static readonly EmbedIOAuthServer _authenticationServer = new(new Uri("https://localhost:5543/callback"), 5543);

        public SpotifyClient SpotifyClient { get; set; }

        private SpotifyClientHelper()
        {
            Task.Run(LaunchAuthenticationServer);

            if (SpotifyClient == null)
            {
                var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator(_clientId, _clientSecret));
                SpotifyClient = new SpotifyClient(config);
            }
        }

        private static SpotifyClientHelper? _instance;

        public static SpotifyClientHelper GetInstance()
        {
            _instance ??= new SpotifyClientHelper();
            return _instance;
        }

        private async Task LaunchAuthenticationServer()
        {
            try
            {
                await _authenticationServer.Start();

                _authenticationServer.ImplictGrantReceived += OnImplicitGrantReceived;
                _authenticationServer.ErrorReceived += OnErrorReceived;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
        }

        private async Task OnErrorReceived(object sender, string error, string? stat)
        {
            _logger.Error(error);
            await _authenticationServer.Stop();
        }

        private async Task OnImplicitGrantReceived(object sender, ImplictGrantResponse response)
        {
            await _authenticationServer.Stop();
            SpotifyClient = new SpotifyClient(response.AccessToken);
        }
    }
}
