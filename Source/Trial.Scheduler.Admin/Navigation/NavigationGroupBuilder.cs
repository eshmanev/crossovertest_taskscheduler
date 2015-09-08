using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Trial.Scheduler.Admin.Navigation
{
    public class NavigationGroupBuilder
    {
        public const float DefaultPosition = 2;
        private readonly List<NavigationLinkBuilder> _links;
        private float _position = DefaultPosition;

        public NavigationGroupBuilder(string groupName)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(groupName));
            
            GroupName = groupName;
            _links = new List<NavigationLinkBuilder>();
        }

        public string GroupName { get; private set; }
        
        public string DisplayName { get; private set; }

        public NavigationGroupBuilder Position(float position)
        {
            _position = position;
            return this;
        }

        public NavigationGroupBuilder WithDisplayName(string dispayName)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(dispayName));
            DisplayName = dispayName;
            return this;
        }

        public NavigationGroupBuilder WithLink(string displayName)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(displayName));
            return WithLink(displayName, x => { });
        }

        public NavigationGroupBuilder WithLink(string displayName, Action<NavigationLinkBuilder> config)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(displayName));
            Contract.Requires(config != null);
            var linkConfiguration = new NavigationLinkBuilder(displayName);
            config(linkConfiguration);
            _links.Add(linkConfiguration);
            return this;
        }

        public NavigationGroup Build()
        {
            var links = _links.Select(x => x.Build()).ToArray();
            var linkedItem = links.LastOrDefault(x => x.LinkedToGroup);
            var groupUrl = linkedItem != null ? linkedItem.Url : null;
            return new NavigationGroup(DisplayName, _position, links, groupUrl);
        }
    }
}