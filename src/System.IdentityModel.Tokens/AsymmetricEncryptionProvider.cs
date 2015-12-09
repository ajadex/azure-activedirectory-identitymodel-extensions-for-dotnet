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
    public class AsymmetricEncryptionProvider : EncryptionProvider
    {
#if DOTNET5_4
        private bool _disposeRsa;
        private RSA _rsa;
#else
        private RSACryptoServiceProvider _rsaCryptoServiceProvider;
#endif

#if DOTNET5_4
        public AsymmetricEncryptionProvider(AsymmetricSecurityKey key, string algorithm, RSAEncryptionPadding padding) : base(key, algorithm)
        {
            if (key == null)
                throw LogHelper.LogException<ArgumentNullException>("key");

            Padding = padding;
            ResolveDotNetCoreEncryptionProvider(key, algorithm);
        }
#else
        public AsymmetricEncryptionProvider(AsymmetricSecurityKey key, string algorithm, bool isOAEP) : base(key, algorithm)
        {
            if (key == null)
                throw LogHelper.LogException<ArgumentNullException>("key");

            IsOAEP = isOAEP;
            ResolveDotNetDesktopEncryptionProvider(key, algorithm);
        }
#endif

#if DOTNET5_4

        public RSAEncryptionPadding Padding { get; set; }

        private void ResolveDotNetCoreEncryptionProvider(AsymmetricSecurityKey key, string algorithm)
        {
            RsaSecurityKey rsaKey = key as RsaSecurityKey;
            if (rsaKey != null)
            {
                _rsa = new RSACng();
                (_rsa as RSA).ImportParameters(rsaKey.Parameters);
                _disposeRsa = true;
                return;
            }

            throw LogHelper.LogException<ArgumentOutOfRangeException>(LogMessages.IDX10641, key);
        }
#else
        public bool IsOAEP { get; set; }

        private void ResolveDotNetDesktopEncryptionProvider(AsymmetricSecurityKey key, string algorithm)
        {
            RsaSecurityKey rsaKey = key as RsaSecurityKey;
            if (rsaKey != null)
            {
                _rsaCryptoServiceProvider = new RSACryptoServiceProvider();
                (_rsaCryptoServiceProvider as RSA).ImportParameters(rsaKey.Parameters);
                return;
            }

            throw LogHelper.LogException<ArgumentOutOfRangeException>(LogMessages.IDX10641, key);
        }
#endif

        public override byte[] Encrypt(byte[] input)
        {
            if (input == null)
                throw LogHelper.LogArgumentNullException("input");

            if (input.Length == 0)
                throw LogHelper.LogException<ArgumentException>(LogMessages.IDX10624);

#if DOTNET5_4
            if (_rsa != null)
                return _rsa.Encrypt(input, Padding);
#else
            if (_rsaCryptoServiceProvider != null)
                return _rsaCryptoServiceProvider.Encrypt(input, IsOAEP);
#endif
            throw LogHelper.LogException<InvalidOperationException>(LogMessages.IDX10644);
        }

        public override byte[] Decrypt(byte[] input)
        {
            if (input == null)
                throw LogHelper.LogArgumentNullException("input");

            if (input.Length == 0)
                throw LogHelper.LogException<ArgumentException>(LogMessages.IDX10624);

#if DOTNET5_4
            if (_rsa != null)
                return _rsa.Decrypt(input, Padding);
#else
            if (_rsaCryptoServiceProvider != null)
                return _rsaCryptoServiceProvider.Decrypt(input, IsOAEP);
#endif
            throw LogHelper.LogException<InvalidOperationException>(LogMessages.IDX10644);
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
            if (disposing)
            {
#if DOTNET5_4
                    if (_rsa != null && _disposeRsa)
                        _rsa.Dispose();
#else
                if (_rsaCryptoServiceProvider != null)
                    _rsaCryptoServiceProvider.Dispose();
#endif
            }
        }
    }
}