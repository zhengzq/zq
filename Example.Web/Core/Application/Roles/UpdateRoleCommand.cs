using System;
using System.Collections.Generic;

namespace Example.Web.Core.Application.Roles
{
    public class UpdateRoleCommand
    {
        /// <summary>
        /// Id ,Selected, Name
        /// </summary>
        public List<Tuple<int, bool, string>> Permissions { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
