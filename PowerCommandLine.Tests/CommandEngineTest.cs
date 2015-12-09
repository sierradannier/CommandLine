// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;
using FluentAssertions;
using Xunit;

namespace PowerCommandLine.Tests
{
    public class CommandEngineTest
    {
        [Fact]
        public void Parse_command_without_params()
        {
            var flag = false;
            CommandEngine.CommandContainer.Register(new CommandRegistration("sample").AddAction(() =>
            {
                flag = true;
            }));
            CommandEngine.Initialize();
            CommandEngine.ParseCommand("sample");
            flag.Should().Be(true);
        }

        [Fact]
        public void Parse_command_with_params()
        {
            var flag = false;
            var p = new string[2];
            CommandEngine.CommandContainer.Register(new CommandRegistration("sample").AddAction((args) =>
            {
                flag = true;
                p = args;
            }));
            CommandEngine.Initialize();
            CommandEngine.ParseCommand("sample -a:5 -b:7");
            flag.Should().Be(true);
            p.Should().Contain(new string[]
            {
                "-a:5", "-b:7"
            });
        }
    }
}
