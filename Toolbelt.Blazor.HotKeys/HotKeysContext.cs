﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private readonly Task<JSInvoker> _AttachTask;

        /// <summary>
        /// Initialize a new instance of the HotKeysContext class.
        /// </summary>
        internal HotKeysContext(Task<JSInvoker> attachTask)
        {
            this._AttachTask = attachTask;
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
            this.Keys.Add(this.Register(new HotKeyEntry(modKeys, key, allowIn, description, action)));
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
            this.Keys.Add(this.Register(new HotKeyEntry(modKeys, key, allowIn, description, action)));
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
            this.Keys.Add(this.Register(new HotKeyEntry(modKeys, key, allowIn, description, action)));
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
            this.Keys.Add(this.Register(new HotKeyEntry(modKeys, key, allowIn, description, action)));
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
            this.Keys.Add(this.Register(new HotKeyEntry(modKeys, keyName, allowIn, description, action)));
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
            this.Keys.Add(this.Register(new HotKeyEntry(modKeys, keyName, allowIn, description, action)));
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
            this.Keys.Add(this.Register(new HotKeyEntry(modKeys, keyName, allowIn, description, action)));
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
            this.Keys.Add(this.Register(new HotKeyEntry(modKeys, keyName, allowIn, description, action)));
            return this;
        }

        private HotKeyEntry Register(HotKeyEntry hotKeyEntry)
        {
            hotKeyEntry.ObjectReference = DotNetObjectReference.Create(hotKeyEntry);
            this._AttachTask.ContinueWith(t =>
            {
                if (t.IsCompleted && !t.IsFaulted)
                {
                    return t.Result.InvokeAsync<int>(
                        "Toolbelt.Blazor.HotKeys.register",
                        hotKeyEntry.ObjectReference, hotKeyEntry.ModKeys, hotKeyEntry.KeyName, hotKeyEntry.AllowIn).AsTask();
                }
                else
                {
                    var tcs = new TaskCompletionSource<int>();
                    tcs.TrySetException(t.Exception.InnerExceptions);
                    return tcs.Task;
                }
            })
            .Unwrap()
            .ContinueWith(t =>
            {
                if (!t.IsCanceled && !t.IsFaulted) { hotKeyEntry.Id = t.Result; }
            });
            return hotKeyEntry;
        }

        private void Unregister(HotKeyEntry hotKeyEntry)
        {
            if (hotKeyEntry.Id == -1) return;

            this._AttachTask.ContinueWith(t =>
            {
                if (t.IsCompleted && !t.IsFaulted)
                {
                    return t.Result.InvokeAsync<object>("Toolbelt.Blazor.HotKeys.unregister", hotKeyEntry.Id).AsTask();
                }
                else
                {
                    var tcs = new TaskCompletionSource<int>();
                    tcs.TrySetException(t.Exception.InnerExceptions);
                    return tcs.Task as Task;
                }
            })
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