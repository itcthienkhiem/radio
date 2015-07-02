#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;

namespace ClearCanvas.Common
{
    /// <summary>
    /// Abstract base class for extension points.
    /// </summary>
    public abstract class ExtensionPoint : IExtensionPoint
    {
        #region PredicateExtensionFilter

        private class PredicateExtensionFilter : ExtensionFilter
        {
            private Predicate<ExtensionInfo> _predicate;

            public PredicateExtensionFilter(Predicate<ExtensionInfo> predicate)
            {
                _predicate = predicate;
            }

            public override bool Test(ExtensionInfo extension)
            {
                return _predicate(extension);
            }
        }

        #endregion

        #region Static members

        // initialize with the default extension factory
        private static IExtensionFactory _extensionFactory = new DefaultExtensionFactory();


        /// <summary>
        /// Sets the <see cref="IExtensionFactory"/> that is used to create extensions.
        /// </summary>
        internal static void SetExtensionFactory(IExtensionFactory extensionFactory)
        {
            Platform.CheckForNullReference(extensionFactory, "extensionFactory");

            _extensionFactory = extensionFactory;
        }

        #endregion

        #region IExtensionPoint methods

        /// <summary>
        /// Gets the interface on which the extension is defined.
        /// </summary>
        public abstract Type InterfaceType { get; }

        /// <summary>
        /// Lists meta-data for all enabled extensions of this point.
        /// </summary>
        /// <returns></returns>
        public ExtensionInfo[] ListExtensions()
        {
            return ListExtensionsHelper(null);
        }

        /// <summary>
        /// List meta-data for enabled extensions of this point that match the supplied filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ExtensionInfo[] ListExtensions(ExtensionFilter filter)
        {
            return ListExtensionsHelper(filter);
        }

        /// <summary>
        /// List meta-data for enabled extensions of this point that match the supplied filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public ExtensionInfo[] ListExtensions(Predicate<ExtensionInfo> filter)
        {
            return ListExtensionsHelper(new PredicateExtensionFilter(filter));
        }

        /// <summary>
        /// Instantiates one extension of this point.
        /// </summary>
        /// <returns></returns>
        public object CreateExtension()
        {
            return AtLeastOne(CreateExtensionsHelper(null, true), this.GetType());
        }

        /// <summary>
        /// Instantiates one extension of this point that matches the specified filter.
        /// </summary>
        public object CreateExtension(ExtensionFilter filter)
        {
            return AtLeastOne(CreateExtensionsHelper(filter, true), this.GetType());
        }

        /// <summary>
        /// Instantiates one extension of this point that matches the specified filter.
        /// </summary>
        public object CreateExtension(Predicate<ExtensionInfo> filter)
        {
            return CreateExtension(new PredicateExtensionFilter(filter));
        }

        /// <summary>
        /// Instantiates all enabled extensions of this point.
        /// </summary>
        public object[] CreateExtensions()
        {
            return CreateExtensionsHelper(null, false);
        }

        /// <summary>
        /// Instantiates all enabled extensions of this point that match the supplied filter.
        /// </summary>
        public object[] CreateExtensions(ExtensionFilter filter)
        {
            return CreateExtensionsHelper(filter, false);
        }

        /// <summary>
        /// Instantiates all enabled extensions of this point that match the supplied filter.
        /// </summary>
        public object[] CreateExtensions(Predicate<ExtensionInfo> filter)
        {
            return CreateExtensions(new PredicateExtensionFilter(filter));
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Protected method that actually performs the extension creation
        /// from an internal <see cref="IExtensionFactory"/>.
        /// </summary>
		protected object[] CreateExtensionsHelper(ExtensionFilter filter, bool justOne)
        {
            // we assume that the factory itself is thread-safe, and therefore we don't need to lock
            // (don't want to incur the cost of locking if not necessary)
            return _extensionFactory.CreateExtensions(this, filter, justOne);
        }

        /// <summary>
		/// Protected method that actually retrieves the <see cref="ExtensionInfo"/>
		/// objects from an internal <see cref="IExtensionFactory"/>.
        /// </summary>
		protected ExtensionInfo[] ListExtensionsHelper(ExtensionFilter filter)
        {
            // we assume that the factory itself is thread-safe, and therefore we don't need to lock
            // (don't want to incur the cost of locking if not necessary)
            return _extensionFactory.ListExtensions(this, filter);
        }

		/// <summary>
		/// Checks to see if there is at least one object in <paramref name="objs"/> and returns 
		/// the first one, otherwise an exception is thrown.
		/// </summary>
		/// <exception cref="NotSupportedException">Thrown if <paramref name="objs"/> is empty.</exception>
        protected object AtLeastOne(object[] objs, Type extensionPointType)
        {
            if (objs.Length > 0)
            {
                return objs[0];
            }
            else
            {
                throw new NotSupportedException(
                    string.Format(SR.ExceptionNoExtensionsCreated, extensionPointType.FullName));
            }
        }

        #endregion
    }


    /// <summary>
    /// Abstract base class for all extension points.
    /// </summary>
    /// <typeparam name="TInterface">The interface that extensions are expected to implement.</typeparam>
    /// <remarks>
    /// <para>
    /// To define an extension point, create a dedicated subclass of this class, specifying the interface
    /// that extensions are expected to implement.  The name of the subclass should be chosen
    /// with care, as the name effectively acts as a unique identifier which all extensions
    /// will reference.  Once chosen, the name should not be changed, as doing so will break all
    /// existing extensions to this extension point.  There is no need to add any methods to the subclass,
    /// and it is recommended that the class be left empty, such that it serves as a dedicated
    /// factory for creating extensions of this extension point.
    /// </para>
    /// <para>The subclass must also be marked with the <see cref="ExtensionPointAttribute" /> in order
    /// for the framework to discover it at runtime.
    /// </para>
    /// </remarks>
    public abstract class ExtensionPoint<TInterface> : ExtensionPoint
    {
        /// <summary>
        /// Gets the interface that the extension point is defined on.
        /// </summary>
        public override Type InterfaceType
        {
            get { return typeof(TInterface); }
        }
    }

}
