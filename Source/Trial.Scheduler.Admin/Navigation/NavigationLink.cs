using System.Diagnostics.Contracts;

namespace Trial.Scheduler.Admin.Navigation
{
    public class NavigationLink
    {
        public NavigationLink(string displayName, string url, string imageUrl, string templateUrl, bool linkedToGroup)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(displayName));
            
            DisplayName = displayName;
            Url = url;
            ImageUrl = imageUrl;
            TemplateUrl = templateUrl;
            LinkedToGroup = linkedToGroup;
        }

        public string DisplayName { get; private set; }

        public string Url { get; private set; }

        public string ImageUrl { get; private set; }

        public string TemplateUrl { get; private set; }

        public bool LinkedToGroup { get; private set; }
    }
}