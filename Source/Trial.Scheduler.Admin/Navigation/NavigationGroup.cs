using System.Diagnostics.Contracts;

namespace Trial.Scheduler.Admin.Navigation
{
    public class NavigationGroup
    {
        public NavigationGroup(string displayName, float position, NavigationLink[] links, string url)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(displayName));
            Contract.Requires(links != null);

            DisplayName = displayName;
            Links = links;
            Position = position;
            Url = url;
        }

        public string DisplayName { get; private set; }

        public float Position { get; private set; }

        public string Url { get; private set; }

        public NavigationLink[] Links { get; private set; }
    }
}