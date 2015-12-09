// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

namespace PowerCommandLine
{
    public static class Command
    {
        public static ICommandRegistration For(string key)
        {
            return new CommandRegistration(key);
        }
    }
}
