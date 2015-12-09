// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System.Collections.Generic;

namespace PowerCommandLine
{
    internal partial class CommandContainer : ICommandContainer
    {
        private IList<ICommandRegistration> _commands;

        public IList<ICommandRegistration> Commands
        {
            get { return _commands; }
        }

        public CommandContainer()
        {
            _commands = new List<ICommandRegistration>();
        }

        public ICommandContainer Register(params ICommandRegistration[] registrations)
        {
            foreach (var item in registrations)
	        {
                _commands.Add(item);
	        }

            return this;
        }
    }
}
