#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;

namespace ClearCanvas.Common.Utilities
{
	/// <summary>
	/// Event used to notify observers of a change in a dictionary.
	/// </summary>
	/// <remarks>
	/// This class is used internally by the <see cref="ObservableDictionary{TKey,TItem}"/>, but can be used
	/// for any dictionary-related event.
	/// </remarks>
	/// <typeparam name="TKey">The type of key in the dictionary.</typeparam>
	/// <typeparam name="TItem">The type of item in the dictionary.</typeparam>
	public class DictionaryEventArgs<TKey, TItem> : EventArgs
	{
		private TKey _key;
		private TItem _item;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="key">The key for the <paramref name="item"/> that has changed.</param>
		/// <param name="item">The item that has changed.</param>
		public DictionaryEventArgs(TKey key, TItem item)
		{
			Platform.CheckForNullReference(key, "key");
			_key = key;
			_item = item;
		}

		/// <summary>
		/// Gets the key for the <see cref="Item"/> that has changed.
		/// </summary>
		public TKey Key
		{
			get { return _key; }
		}

		/// <summary>
		/// Gets the item that has changed.
		/// </summary>
		public TItem Item
		{
			get { return _item; }
		}
	}
}
