// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using PowerCommandLine.Samples.Commands;

namespace PowerCommandLine.Samples.Registrations
{
    public partial class CommandRegistrar : ICommandRegistrar
    {
        public void Register(ICommandContainer container)
        {
            container.Register(Command.For("sampleA").WithDescription("Sample command registration with delegate.").WithHelp("Help for sample command registration delegate").AddAction(() =>
            {
                Console.WriteLine("Sample command execution");
            }));

            container.Register(Command.For("sampleB").WithDescription("Sample command registration with class.").WithHelp("Help for sample command registration class").ImplementedBy(new SampleCommandDefinition()));
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
