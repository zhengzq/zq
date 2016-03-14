using System;
using System.Collections.Generic;

namespace Example.Web.Core.Application.Roles
{
    public class CreateRoleCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        /// <summary>
        /// Id ,Selected, Name
        /// </summary>
        public List<Tuple<string, bool, string>> Permissions { get; set; }
    }
}
