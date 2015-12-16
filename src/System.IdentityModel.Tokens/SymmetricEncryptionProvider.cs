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

using System.Security.Cryptography;
using Microsoft.IdentityModel.Logging;

namespace System.IdentityModel.Tokens
{
    public class SymmetricEncryptionProvider : EncryptionProvider
    {
        public SymmetricEncryptionProvider(SymmetricSecurityKey key, string algorithm) : base(key, algorithm)
        {
            if (key == null)
                throw LogHelper.LogException<ArgumentNullException>("key");

#if DOTNET5_4
            ResolveDotNetCoreEncryptionProvider(key, algorithm);
#else
            ResolveDesktopEncryptionProvider(key, algorithm);
#endif
        }

#if DOTNET5_4
        private void ResolveDotNetCoreEncryptionProvider(SymmetricSecurityKey key, string algorithm)
        {
            
        }
#else
        private void ResolveDesktopEncryptionProvider(SymmetricSecurityKey key, string algorithm)
        {
            throw new NotImplementedException();
        }
#endif

        public override byte[] Encrypt(byte[] input)
        {
            return null;
        }

        public override byte[] Decrypt(byte[] input)
        {
            return null;
        }

        public byte[] Encrypt(byte[] input, byte[] iv)
        {
            return null;
        }

        public byte[] Decrypt(byte[] input, byte[] iv)
        {
            return null;
        }

        public byte[] Encrypt(byte[] input, byte[] iv, string authenticationTag)
        {
            return null;
        }

        public byte[] Decrypt(byte[] input, byte[] iv, string authenticationTag)
        {
            return null;
        }

        public bool IsSupportedAlgorithm(string algorithm)
        {
            return false;
        }

        /// <summary>
        /// Can be over written in descendants to dispose of internal components.
        /// </summary>
        /// <param name="disposing">true, if called from Dispose(), false, if invoked inside a finalizer</param>     
        protected override void Dispose(bool disposing)
        {

        }
    }
}