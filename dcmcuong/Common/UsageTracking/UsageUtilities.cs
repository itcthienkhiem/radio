#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.Globalization;
using System.Net;
using System.ServiceModel;
using System.Threading;
using ClearCanvas.Common.Utilities;

namespace ClearCanvas.Common.UsageTracking
{
    /// <summary>
    /// Enum for specifying if a Usage Tracking message will be sent on the current thread or a background thread.
    /// </summary>
    public enum UsageTrackingThread
    {
        /// <summary>
        /// The Usage Tracking message will be sent on a background thread
        /// </summary>
        Background,
        /// <summary>
        /// The UsageTracking message will be sent on the current thread.
        /// </summary>
        Current,
    }

    /// <summary>
    /// Static helper class for implementing usage tracking of ClearCanvas applications.
    /// </summary>
    public static class UsageUtilities
    {
        #region Private Members
        
        private static event EventHandler<ItemEventArgs<DisplayMessage>> Message;
        private static readonly object SyncLock = new object();
        private static bool _first = true;
        #endregion

        #region Public Static Properties

        /// <summary>
        /// Event which can receive display messages from the UsageTracking server
        /// </summary>
        /// <remarks>
        /// Note that the configuration option in <see cref="UsageTrackingSettings"/> must be enabled to receive these
        /// messages.
        /// </remarks>
        public static event EventHandler<ItemEventArgs<DisplayMessage>> MessageEvent
		{
            add
            {
                lock (SyncLock)
                    Message += value;
            }
            remove
            {
                lock (SyncLock)
                    Message -= value;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Send the UsageTracking message.
        /// </summary>
        /// <param name="theMessage"></param>
        private static void Send(object theMessage)
        {
            try
            {
                lock (SyncLock)
                {
                    if (_first)
                    {
                        //TODO double check to see if this is required on final configuration
                        ServicePointManager.ServerCertificateValidationCallback +=
                            ((sender, certificate, chain, sslPolicyErrors) =>
                             true);
                        _first = false;
                    }
                }

                UsageMessage message = theMessage as UsageMessage;
                if (message != null)
                {
                    RegisterRequest req = new RegisterRequest
                                              {
                                                  Message = message
                                              };

                  

#if UNIT_TESTS_USAGE_TRACKING
                    WSHttpBinding binding = new WSHttpBinding();
                    EndpointAddress endpointAddress = new EndpointAddress("http://localhost:8080/UsageTracking");
#elif	DEBUG
                    WSHttpBinding binding = new WSHttpBinding();
                    EndpointAddress endpointAddress = new EndpointAddress("http://localhost/Tracking/Service.svc");
#else
                    //TODO:  This should be updated to real address
                    WSHttpBinding binding = new WSHttpBinding(SecurityMode.Transport);
                    binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                    binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                    EndpointAddress endpointAddress = new EndpointAddress("https://4rf/Tracking/Service.svc");
#endif

                    RegisterResponse response;
                    using (UsageTrackingServiceClient client = new UsageTrackingServiceClient(binding, endpointAddress))
                    {
                        response = client.Register(req);
                    }
                    if (response != null 
                        && response.Message != null
                        && UsageTrackingSettings.Default.DisplayMessages)
                    {
                        EventsHelper.Fire(Message, null, new ItemEventArgs<DisplayMessage>(response.Message)); 
                    }
                }
            }
            catch (Exception e)
            {
                Platform.Log(LogLevel.Debug, e);
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Register the usage of the application with a ClearCanvas server on a background thread.
        /// </summary>
        /// <remarks>
        /// A check is done of the <see cref="UsageTrackingSettings"/>, and if usage tracking is enabled, the 
        /// <paramref name="message"/> is sent to the ClearCanvas server.
        /// </remarks>
        /// <param name="message">The usage message to send.</param>
        /// <param name="thread">Flag telling if the usage will be sent on the current thread or a background thread.</param>
        public static void Register(UsageMessage message, UsageTrackingThread thread)
        {
            if (UsageTrackingSettings.Default.Enabled)
                try
                {
                    UsageMessage theMessage = message;
                    if (thread == UsageTrackingThread.Current)
                        Send(theMessage);
                    else if (thread == UsageTrackingThread.Background)
                        ThreadPool.QueueUserWorkItem(Send,theMessage);
                }
                catch (Exception e)
                {
                    // Fail silently
                    Platform.Log(LogLevel.Debug, e);
                }
        }

        /// <summary>
        /// Get a <see cref="UsageMessage"/> for the application.
        /// </summary>
        /// <returns>
        /// <para>
        /// A new <see cref="UsageMessage"/> object with product, region, timestamp, license, and OS information filled in.
        /// </para>
        /// <para>
        /// The <see cref="UsageMessage"/> instance is used in conjunction with <see cref="Register"/> to send a usage message
        /// to ClearCanvas servers.
        /// </para>
        /// </returns>
        public static UsageMessage GetUsageMessage()
        {
            UsageMessage msg = new UsageMessage
                                   {
                                       Version = ProductInformation.GetVersion(true, true),
                                       Product = ProductInformation.Product,
                                       Component = ProductInformation.Component,
                                       Edition = ProductInformation.Edition,
                                       AllowDiagnosticUse = ProductInformation.AllowDiagnosticUse,
                                       Region = CultureInfo.CurrentCulture.Name,
                                       Timestamp = Platform.Time,
                                       OS = Environment.OSVersion.ToString(),
                                       MachineIdentifier =  EnvironmentUtilities.MachineIdentifier,
                                       MessageType = UsageType.Other,
                                       LicenseString = LicenseInformation.LicenseKey
                                   };
            return msg;
        }

        #endregion
    }
}
