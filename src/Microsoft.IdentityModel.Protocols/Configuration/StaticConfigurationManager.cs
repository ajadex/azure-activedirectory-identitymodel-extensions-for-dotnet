//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Protocols
{
    /// <summary>
    /// This type is for users that want a fixed and static Configuration.
    /// In this case, the configuration is obtained and passed to the constructor.
    /// </summary>
    /// <typeparam name="T">must be a class.</typeparam>
    public class StaticConfigurationManager<T> : IConfigurationManager<T> where T : class
    {
        private T _configuration;

        /// <summary>
        /// Initializes an new instance of <see cref="StaticConfigurationManager{T}"/> with a Configuration instance.
        /// </summary>
        /// <param name="configuration">Configuration of type <see cref="OpenIdConnectConfiguration"/> or <see cref="WsFederationConfiguration"/>.</param>
        public StaticConfigurationManager(T configuration)
        {
            if (configuration == null)
            {
                LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10000, GetType() + ": configuration"), typeof(ArgumentNullException), EventLevel.Verbose);
            }

            _configuration = configuration;
        }

        /// <summary>
        /// Obtains an updated version of Configuration.
        /// </summary>
        /// <param name="cancel"><see cref="CancellationToken"/>.</param>
        /// <returns>Configuration of type T.</returns>
        public Task<T> GetConfigurationAsync(CancellationToken cancel)
        {
            return Task.FromResult(_configuration);
        }

        /// <summary>
        /// For the this type, this is a no-op
        /// </summary>
        public void RequestRefresh()
        {
        }
    }
}
