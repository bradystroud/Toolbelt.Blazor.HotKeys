﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Toolbelt.Blazor.HotKeys
{
    /// <summary>
    /// Current active hotkeys set.
    /// </summary>
    public class HotKeysContext : IDisposable
    {
        /// <summary>
        /// The collection of Hotkey entries.
        /// </summary>
        public List<HotKeyEntry> Keys { get; } = new List<HotKeyEntry>();

        private readonly IJSRuntime JSRuntime;

        private readonly Task AttachTask;

        private readonly ILogger<HotKeys> Logger;

        /// <summary>
        /// Initialize a new instance of the HotKeysContext class.
        /// </summary>
        /// <param name="jSRuntime"></param>
        /// <param name="attachTask"></param>
        /// <param name="logger"></param>
        internal HotKeysContext(IJSRuntime jSRuntime, Task attachTask, ILogger<HotKeys> logger)
        {
            this.JSRuntime = jSRuntime;
            this.AttachTask = attachTask;
            this.Logger = logger;
        }

        /// <summary>
        /// Add a new hotkey entry to this context.<br/>
        /// if the key that you want to hook is not covered by the Keys enum values, use the other overload version that accepts key name as a string.
        /// </summary>
        /// <param name="modKeys">The combination of modifier keys flags.</param>
        /// <param name="key">The identifier of hotkey.</param>
        /// <param name="action">The callback action that will be invoked when user enter modKeys + key combination on the browser.</param>
        /// <param name="description">The description of the meaning of this hot key entry.</param>
        /// <param name="allowIn">The combination of HTML element flags that will be allowed hotkey works.</param>
        /// <returns>This context.</returns>
        public HotKeysContext Add(ModKeys modKeys, Keys key, Func<HotKeyEntry, Task> action, string description = "", AllowIn allowIn = AllowIn.None)
        {
            this.Keys.Add(Register(new HotKeyEntry(modKeys, key, allowIn, description, action)));
            return this;
        }

        /// <summary>
        /// Add a new hotkey entry to this context.<br/>
        /// if the key that you want to hook is not covered by the Keys enum values, use the other overload version that accepts key name as a string.
        /// </summary>
        /// <param name="modKeys">The combination of modifier keys flags.</param>
        /// <param name="key">The identifier of hotkey.</param>
        /// <param name="action">The callback action that will be invoked when user enter modKeys + key combination on the browser.</param>
        /// <param name="description">The description of the meaning of this hot key entry.</param>
        /// <param name="allowIn">The combination of HTML element flags that will be allowed hotkey works.</param>
        /// <returns>This context.</returns>
        public HotKeysContext Add(ModKeys modKeys, Keys key, Func<Task> action, string description = "", AllowIn allowIn = AllowIn.None)
        {
            this.Keys.Add(Register(new HotKeyEntry(modKeys, key, allowIn, description, action)));
            return this;
        }

        /// <summary>
        /// Add a new hotkey entry to this context.<br/>
        /// if the key that you want to hook is not covered by the Keys enum values, use the other overload version that accepts key name as a string.
        /// </summary>
        /// <param name="modKeys">The combination of modifier keys flags.</param>
        /// <param name="key">The identifier of hotkey.</param>
        /// <param name="action">The callback action that will be invoked when user enter modKeys + key combination on the browser.</param>
        /// <param name="description">The description of the meaning of this hot key entry.</param>
        /// <param name="allowIn">The combination of HTML element flags that will be allowed hotkey works.</param>
        /// <returns>This context.</returns>
        public HotKeysContext Add(ModKeys modKeys, Keys key, Action<HotKeyEntry> action, string description = "", AllowIn allowIn = AllowIn.None)
        {
            this.Keys.Add(Register(new HotKeyEntry(modKeys, key, allowIn, description, action)));
            return this;
        }

        /// <summary>
        /// Add a new hotkey entry to this context.<br/>
        /// if the key that you want to hook is not covered by the Keys enum values, use the other overload version that accepts key name as a string.
        /// </summary>
        /// <param name="modKeys">The combination of modifier keys flags.</param>
        /// <param name="key">The identifier of hotkey.</param>
        /// <param name="action">The callback action that will be invoked when user enter modKeys + key combination on the browser.</param>
        /// <param name="description">The description of the meaning of this hot key entry.</param>
        /// <param name="allowIn">The combination of HTML element flags that will be allowed hotkey works.</param>
        /// <returns>This context.</returns>
        public HotKeysContext Add(ModKeys modKeys, Keys key, Action action, string description = "", AllowIn allowIn = AllowIn.None)
        {
            this.Keys.Add(Register(new HotKeyEntry(modKeys, key, allowIn, description, action)));
            return this;
        }

        /// <summary>
        /// Add a new hotkey entry to this context.<br/>
        /// if the key that you want to hook is not covered by the Keys enum values, use this overload version that accepts key name as a string.
        /// </summary>
        /// <param name="modKeys">The combination of modifier keys flags.</param>
        /// <param name="keyName">The name of the identifier of hotkey.<para>The "key name" is a bit different from the "key" and "code" properties of the DOM event object.<br/> The "key name" comes from "key" and "code", but it is tried to converting to one of the Keys enum values names.<br/>if the keyboard event is not covered by Keys enum values, the "key name" will be the value of "code" or "key".</para></param>
        /// <param name="action">The callback action that will be invoked when user enter modKeys + key combination on the browser.</param>
        /// <param name="description">The description of the meaning of this hot key entry.</param>
        /// <param name="allowIn">The combination of HTML element flags that will be allowed hotkey works.</param>
        /// <returns>This context.</returns>
        public HotKeysContext Add(ModKeys modKeys, string keyName, Func<HotKeyEntry, Task> action, string description = "", AllowIn allowIn = AllowIn.None)
        {
            this.Keys.Add(Register(new HotKeyEntry(modKeys, keyName, allowIn, description, action)));
            return this;
        }

        /// <summary>
        /// Add a new hotkey entry to this context.<br/>
        /// if the key that you want to hook is not covered by the Keys enum values, use this overload version that accepts key name as a string.
        /// </summary>
        /// <param name="modKeys">The combination of modifier keys flags.</param>
        /// <param name="keyName">The name of the identifier of hotkey.<para>The "key name" is a bit different from the "key" and "code" properties of the DOM event object.<br/> The "key name" comes from "key" and "code", but it is tried to converting to one of the Keys enum values names.<br/>if the keyboard event is not covered by Keys enum values, the "key name" will be the value of "code" or "key".</para></param>
        /// <param name="action">The callback action that will be invoked when user enter modKeys + key combination on the browser.</param>
        /// <param name="description">The description of the meaning of this hot key entry.</param>
        /// <param name="allowIn">The combination of HTML element flags that will be allowed hotkey works.</param>
        /// <returns>This context.</returns>
        public HotKeysContext Add(ModKeys modKeys, string keyName, Func<Task> action, string description = "", AllowIn allowIn = AllowIn.None)
        {
            this.Keys.Add(Register(new HotKeyEntry(modKeys, keyName, allowIn, description, action)));
            return this;
        }

        /// <summary>
        /// Add a new hotkey entry to this context.<br/>
        /// if the key that you want to hook is not covered by the Keys enum values, use this overload version that accepts key name as a string.
        /// </summary>
        /// <param name="modKeys">The combination of modifier keys flags.</param>
        /// <param name="keyName">The name of the identifier of hotkey.<para>The "key name" is a bit different from the "key" and "code" properties of the DOM event object.<br/> The "key name" comes from "key" and "code", but it is tried to converting to one of the Keys enum values names.<br/>if the keyboard event is not covered by Keys enum values, the "key name" will be the value of "code" or "key".</para></param>
        /// <param name="action">The callback action that will be invoked when user enter modKeys + key combination on the browser.</param>
        /// <param name="description">The description of the meaning of this hot key entry.</param>
        /// <param name="allowIn">The combination of HTML element flags that will be allowed hotkey works.</param>
        /// <returns>This context.</returns>
        public HotKeysContext Add(ModKeys modKeys, string keyName, Action<HotKeyEntry> action, string description = "", AllowIn allowIn = AllowIn.None)
        {
            this.Keys.Add(Register(new HotKeyEntry(modKeys, keyName, allowIn, description, action)));
            return this;
        }

        /// <summary>
        /// Add a new hotkey entry to this context.<br/>
        /// if the key that you want to hook is not covered by the Keys enum values, use this overload version that accepts key name as a string.
        /// </summary>
        /// <param name="modKeys">The combination of modifier keys flags.</param>
        /// <param name="keyName">The name of the identifier of hotkey.<para>The "key name" is a bit different from the "key" and "code" properties of the DOM event object.<br/> The "key name" comes from "key" and "code", but it is tried to converting to one of the Keys enum values names.<br/>if the keyboard event is not covered by Keys enum values, the "key name" will be the value of "code" or "key".</para></param>
        /// <param name="action">The callback action that will be invoked when user enter modKeys + key combination on the browser.</param>
        /// <param name="description">The description of the meaning of this hot key entry.</param>
        /// <param name="allowIn">The combination of HTML element flags that will be allowed hotkey works.</param>
        /// <returns>This context.</returns>
        public HotKeysContext Add(ModKeys modKeys, string keyName, Action action, string description = "", AllowIn allowIn = AllowIn.None)
        {
            this.Keys.Add(Register(new HotKeyEntry(modKeys, keyName, allowIn, description, action)));
            return this;
        }

        private HotKeyEntry Register(HotKeyEntry hotKeyEntry)
        {
            hotKeyEntry.ObjectReference = DotNetObjectReference.Create(hotKeyEntry);
            this.AttachTask.ContinueWith(t =>
            {
                if (t.IsFaulted) return Task.FromException<int>(t.Exception);
                return this.JSRuntime.InvokeAsync<int>(
                    "Toolbelt.Blazor.HotKeys.register",
                    hotKeyEntry.ObjectReference, hotKeyEntry.ModKeys, hotKeyEntry.KeyName, hotKeyEntry.AllowIn).AsTask();
            })
            .Unwrap()
            .ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var e = t.Exception is AggregateException ? t.Exception.InnerException : t.Exception;
                    //if (!(e is InvalidOperationException)) this.Logger.LogError(e, e.Message);
                }
                else if (!t.IsCanceled) { hotKeyEntry.Id = t.Result; }
            });
            return hotKeyEntry;
        }

        private void Unregister(HotKeyEntry hotKeyEntry)
        {
            if (hotKeyEntry.Id == -1) return;

            this.JSRuntime.InvokeVoidAsync("Toolbelt.Blazor.HotKeys.unregister", hotKeyEntry.Id)
                .AsTask()
                .ContinueWith(t =>
                {
                    hotKeyEntry.Id = -1;
                    hotKeyEntry.ObjectReference?.Dispose();
                    hotKeyEntry.ObjectReference = null;
                });
        }

        /// <summary>
        /// Deactivate the hot key entry contained in this context.
        /// </summary>
        public void Dispose()
        {
            foreach (var entry in this.Keys)
            {
                this.Unregister(entry);
            }
            this.Keys.Clear();
        }
    }
}