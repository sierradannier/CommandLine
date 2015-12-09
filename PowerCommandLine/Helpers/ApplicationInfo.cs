// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;
using System.Reflection;

namespace PowerCommandLine.Helpers
{
    static internal class ApplicationInfo
    {
        public static Version CurrentVersion
        {
            get
            {
                return Assembly.GetCallingAssembly().GetName().Version;
            }
        }
    }
}
