using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Trial.Scheduler.Admin.Navigation
{
    public class NavigationBuilder
    {
        private readonly List<NavigationGroupBuilder> _groups = new List<NavigationGroupBuilder>();

        /// <summary>
        /// Gets a list of groups.
        /// </summary>
        public IEnumerable<NavigationGroupBuilder> Groups { get { return _groups; } }

        /// <summary>
        /// Gets or creates a group with the given name.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>A builder of the group.</returns>
        public NavigationGroupBuilder UsingGroup(string groupName)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(groupName));
            var group = Groups.FirstOrDefault(x => x.GroupName == groupName);
            if (group == null)
            {
                group = new NavigationGroupBuilder(groupName);
                _groups.Add(group);
            }
            return group;
        }
    }
}