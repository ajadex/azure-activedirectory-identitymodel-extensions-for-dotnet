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
        public EncryptingCredentials(SecurityKey key, SecurityKey contentEncryptionKey, string keyEncryptionAlgorithm, string contentEncryptionAlgorithm, byte[] iv)
        {
            if (key == null)
                throw LogHelper.LogArgumentNullException("key");

            if (contentEncryptionKey == null)
                throw LogHelper.LogArgumentNullException("contentEncryptionKey");

            if (string.IsNullOrEmpty(keyEncryptionAlgorithm))
                throw LogHelper.LogArgumentNullException("keyEncryptionAlgorithm");

            if (string.IsNullOrEmpty(contentEncryptionAlgorithm))
                throw LogHelper.LogArgumentNullException("contentEncryptionAlgorithm");

            KeyEncryptionAlgorithm = keyEncryptionAlgorithm;
            ContentEncryptionAlgorithm = contentEncryptionAlgorithm;
            Key = key;
            ContentEncryptionKey = contentEncryptionKey;
            InitializationVector = iv;
        }

        /// <summary>
        /// Gets or sets the encryption algorithm used to encrypt the Content Encryption Key.
        /// </summary>
        public string KeyEncryptionAlgorithm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the encryption algorithm used to encrypt the plain text.
        /// </summary>
        public string ContentEncryptionAlgorithm
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the encryption key used for encrypting Content Encryption Key.
        /// </summary>
        public SecurityKey Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the key used for encrypting the plain text
        /// </summary>
        public SecurityKey ContentEncryptionKey
        {
            get;
            private set;
        }

        public string Kid
        {
            get { return Key.KeyId; }
        }

        public byte[] InitializationVector
        {
            get;
            set;
        }

        public string AuthenticationTag { get; set; }

        public string AssociatedAuthenticationData { get; set; }
    }
}