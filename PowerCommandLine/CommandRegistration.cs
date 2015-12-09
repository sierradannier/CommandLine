// Copyright 2015 Dannier Sierra. All rights reserved. See License.md in the project root for license information.

using System;

namespace PowerCommandLine
{
    public class CommandRegistration : ICommandRegistration
    {
        private string _key;
        public string Key
        {
            get
            {
                return _key;
            }
        }
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
        }
        private string _help;
        public string Help
        {
            get
            {
                return _help;
            }
        }
        private ActionType _actionType;
        protected ActionType ActionType
        {
            get
            {
                return _actionType;
            }
        }
        private object _action;
        public object Action
        {
            get
            {
                return _action;
            }
        }

        public CommandRegistration(string key)
        {
            _key = key;
        }

        public ICommandRegistration ImplementedBy<T>(T instance) where T : ICommand
        {
            this._action = instance;
            this._actionType = ActionType.Class;
            return this;
        }

        public ICommandRegistration AddAction(Action action)
        {
            this._action = action;
            this._actionType = ActionType.Delegate;
            return this;
        }

        public ICommandRegistration AddAction(Action<string[]> action)
        {
            this._action = action;
            this._actionType = ActionType.Delegate;
            return this;
        }

        public ICommandRegistration WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        public ICommandRegistration WithHelp(string help)
        {
            this._help = help;
            return this;
        }

        public void Execute()
        {
            if (this.ActionType == ActionType.Delegate)
            {
                if (this.Action != null)
                    (this.Action as Action)();
            }
            else
            {
                if (this.Action != null)
                    (this.Action as ICommand).ExecuteCommand();
            }
        }

        public void Execute(string[] args)
        {
            if (this.ActionType == ActionType.Delegate)
            {
                if (this.Action != null)
                {
                    if(this.Action is Action<string[]>)
                        (this.Action as Action<string[]>)(args);
                    else
                        (this.Action as Action)();
                }
            }
            else
            {
                if (this.Action != null)
                    (this.Action as ICommand).ExecuteCommand();
            }
        }
    }

    public enum ActionType
    {
        Delegate = 1,
        Class = 2,
    }
}
