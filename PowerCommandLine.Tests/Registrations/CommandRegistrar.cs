// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;

namespace PowerCommandLine.Tests.Registrations
{
    public partial class CommandRegistrar : ICommandRegistrar
    {
        public void Register(ICommandContainer container)
        {
            container.Register(Command.For("sampleA").WithDescription("Sample command registration with delegate.").WithHelp("Help for sample command registration delegate").AddAction(() =>
            {
                Console.WriteLine("Sample command execution");
            }));
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
