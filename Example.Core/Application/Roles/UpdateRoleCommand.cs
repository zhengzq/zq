﻿using System;
using System.Collections.Generic;

namespace Example.Core.Application.Roles
{
    public class UpdateRoleCommand
    {
        /// <summary>
        /// Id ,Selected, Name
        /// </summary>
        public List<Tuple<string, bool, string>> Permissions { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
