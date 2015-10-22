//-----------------------------------------------------------------------
// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
// Apache License 2.0
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using Microsoft.IdentityModel.Logging;
using System.Diagnostics.Tracing;
using System.Globalization;

namespace System.IdentityModel.Tokens
{
    public class SigningCredentials
    {
        public SigningCredentials(SecurityKey key, string algorithm)
        {
            if (key == null)
                throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10000, GetType() + ": key", EventLevel.Verbose);

            if (string.IsNullOrEmpty(algorithm))
                throw LogHelper.LogException<ArgumentNullException>(LogMessages.IDX10000, GetType() + ": algorithm", EventLevel.Verbose);

            Algorithm = algorithm;
            Key = key;
        }

        public string Algorithm
        {
            get;
            private set;
        }

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
