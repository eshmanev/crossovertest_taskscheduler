using System;
using System.Diagnostics.Contracts;

namespace Trial.Scheduler.Admin.Navigation
{
    public class NavigationLinkBuilder
    {
        private readonly string _displayName;
        private string _url;
        private string _imageUrl;
        private bool _linkToGroup;
        private string _templateUrl;

        public NavigationLinkBuilder(string displayName)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(displayName));
            _displayName = displayName;
        }

        public NavigationLinkBuilder Image(string url)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(url));
            _imageUrl = url;
            return this;
        }

        public NavigationLinkBuilder Template(string templateUrl)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(templateUrl));
            _templateUrl = templateUrl;
            return this;
        }

        public NavigationLinkBuilder Url(string url)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(url));
            _url = url;
            return this;
        }

        public NavigationLinkBuilder Url(Uri url)
        {
            Contract.Requires(url != null);
            _url = url.ToString();
            return this;
        }

        public NavigationLinkBuilder LinkToGroup()
        {
            _linkToGroup = true;
            return this;
        }

        public NavigationLink Build()
        {
            return new NavigationLink(_displayName, _url, _imageUrl, _templateUrl, _linkToGroup);
        }
    }
}