//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

using Microsoft.IdentityModel.Logging;

namespace System.IdentityModel.Tokens
{
    /// <summary>
    /// This class defines the encrypting credentials which can be used for encryption.
    /// </summary>
    public class EncryptingCredentials
    {
        /// <summary>
        /// Constructs an <see cref="EncryptingCredentials /> with a security key the encryption algorithm.
        /// </summary>
        /// <param name="key">A security key for encryption.</param>
        /// <param name="keyIdentifier">A security key identifier for the encryption key.</param>
        /// <param name="algorithm">The encryption algorithm.</param>
        /// <exception cref="ArgumentNullException">When key is null.</exception>
        /// <exception cref="ArgumentNullException">When algorithm is null.</exception>
        public EncryptingCredentials(SecurityKey key, string algorithm)
        {
            if (key == null)
                throw LogHelper.LogArgumentNullException("key");

            if (string.IsNullOrEmpty(algorithm))
                throw LogHelper.LogArgumentNullException("algorithm");

            Algorithm = algorithm;
            Key = key;
        }

        /// <summary>
        /// Gets or sets the encryption algorithm.
        /// </summary>
        public string Algorithm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the encryption key material.
        /// </summary>
        public SecurityKey Key
        {
            get;
            private set;
        }

        public string Kid
        {
            get { return Key.KeyId; }
        }
    }
}
