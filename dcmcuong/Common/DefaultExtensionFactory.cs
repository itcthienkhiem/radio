#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Collections.Generic;
using ClearCanvas.Common.Utilities;

namespace ClearCanvas.Common
{
    /// <summary>
    /// The default implementation of <see cref="IExtensionFactory"/> that creates extensions from
    /// the set of plugins discovered at runtime.
    /// </summary>
    /// <remarks>
    /// This class is safe for use by mutliple concurrent threads.
    /// </remarks>
    internal class DefaultExtensionFactory : IExtensionFactory
    {
    	private volatile IDictionary<Type, List<ExtensionInfo>> _extensionMap;
		private readonly object _syncLock = new object();

        internal DefaultExtensionFactory()
        {
        }

        #region IExtensionFactory Members

        /// <summary>
        /// Creates one of each type of object that extends the input <paramref name="extensionPoint" />, 
        /// matching the input <paramref name="filter" />; creates a single extension if <paramref name="justOne"/> is true.
        /// </summary>
        /// <param name="extensionPoint">The <see cref="ExtensionPoint"/> to create extensions for.</param>
        /// <param name="filter">The filter used to match each extension that is discovered.</param>
        /// <param name="justOne">Indicates whether or not to return only the first matching extension that is found.</param>
        /// <returns></returns>
        public object[] CreateExtensions(ExtensionPoint extensionPoint, ExtensionFilter filter, bool justOne)
        {
            // get subset of applicable extensions
            List<ExtensionInfo> extensions = ListExtensionsHelper(extensionPoint, filter);

            // attempt to instantiate the extension classes
            List<object> createdObjects = new List<object>();
            foreach (ExtensionInfo extension in extensions)
            {
                if (justOne && createdObjects.Count > 0)
                    break;

                // is the extension a concrete class?
                if (!IsConcreteClass(extension.ExtensionClass))
                {
                    Platform.Log(LogLevel.Warn, SR.ExceptionExtensionMustBeConcreteClass,
                        extension.ExtensionClass.FullName);
                    continue;
                }

                // does the extension implement the required interface?
                if (!extensionPoint.InterfaceType.IsAssignableFrom(extension.ExtensionClass))
                {
                    Platform.Log(LogLevel.Warn, SR.ExceptionExtensionDoesNotImplementRequiredInterface,
                        extension.ExtensionClass.FullName,
                        extensionPoint.InterfaceType);

                    continue;
                }

                try
                {
                    // instantiate
                    object o = Activator.CreateInstance(extension.ExtensionClass);
                    createdObjects.Add(o);
                }
                catch (Exception e)
                {
                    // instantiation failed
					// this should not be considered an exceptional circumstance
					// instantiation may fail by design in some cases (e.g extension is designed only to run on a particular platform)
					Platform.Log(LogLevel.Debug, e);
                }
            }

            return createdObjects.ToArray();
        }

        /// <summary>
        /// Gets metadata describing all enabled extensions of the input <paramref name="extensionPoint"/>, 
        /// matching the given <paramref name="filter"/>.
        /// </summary>
        /// <param name="extensionPoint">The <see cref="ExtensionPoint"/> whose extension metadata is to be retrieved.</param>
        /// <param name="filter">An <see cref="ExtensionFilter"/> used to filter out extensions with particular characteristics.</param>
        /// <returns></returns>
        public ExtensionInfo[] ListExtensions(ExtensionPoint extensionPoint, ExtensionFilter filter)
        {
        	return ListExtensionsHelper(extensionPoint, filter).ToArray();
        }

        #endregion

		private List<ExtensionInfo> ListExtensionsHelper(ExtensionPoint extensionPoint, ExtensionFilter filter)
		{
			// ensure extension map has been constructed
			BuildExtensionMapOnce();

			Type extensionPointClass = extensionPoint.GetType();

			List<ExtensionInfo> extensions;
			if (_extensionMap.TryGetValue(extensionPointClass, out extensions))
			{
				return CollectionUtils.Select(extensions,
					delegate(ExtensionInfo extension)
					{
						return extension.Enabled
								&& (filter == null || filter.Test(extension));
					});
			}
			else
			{
				return new List<ExtensionInfo>();
			}

		}

		private static bool IsConcreteClass(Type type)
		{
			return !type.IsAbstract && type.IsClass;
		}

		private void BuildExtensionMapOnce()
		{
			// build extension map if not already built
			// note that this is the only place where we need to lock, because once built, map is safe for concurrent readers
			if(_extensionMap == null)
			{
				lock(_syncLock)
				{
					if(_extensionMap == null)
					{
						// group extensions by extension point
						// (note that grouping preserves the order of the original Extensions list)
						_extensionMap = CollectionUtils.GroupBy<ExtensionInfo, Type>(Platform.PluginManager.Extensions,
							delegate(ExtensionInfo extension) { return extension.PointExtended; });
					}
				}
			}
		}
    }
}
