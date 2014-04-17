using ScoreApp.Domain.Factories;
using ScoreApp.Domain.Services;

namespace ScoreApp.Application
{
    public class ImageSearch : IImageSearch
    {
        private readonly dynamic userApp;

        public ImageSearch(IUserAppFactory userAppFactory)
        {
            userApp = userAppFactory.Create();
        }

        public string Search(string userId)
        {
            var result = userApp.Oauth.Connection.Search(userId: userId, fields: new[] { "provider_id", "provider_user_data" });
            foreach (var item in result.Items)
            {
                if (item.ProviderId != "facebook")
                    continue;

                return "http://graph.facebook.com/" + item.ProviderUserData.Id + "/picture";
            }

            return null;
        }
    }
}
