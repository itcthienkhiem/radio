#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

namespace ClearCanvas.Common.Authorization
{
    /// <summary>
    /// Interface used by <see cref="DefineAuthorityGroupsExtensionPoint"/>. 
    /// </summary>
    public interface IDefineAuthorityGroups
    {
        /// <summary>
        /// Get the authority group definitions.
        /// </summary>
        /// <returns></returns>
        AuthorityGroupDefinition[] GetAuthorityGroups();
    }
}
