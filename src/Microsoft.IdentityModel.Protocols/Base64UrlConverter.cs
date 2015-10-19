﻿//-----------------------------------------------------------------------
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

using System;
using System.Diagnostics.Tracing;
using System.Globalization;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.IdentityModel.Protocols
{
    public class Base64UrlConverter : JsonConverter
    {
        
        /// <summary>
        /// Converts a byte array to a Base64Url encoded string
        /// </summary>
        /// <param name="input">The byte array to convert</param>
        /// <returns>The Base64Url encoded form of the input</returns>
        private static string ToBase64UrlString( byte[] input )
        {
            if ( input == null )
                LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10000, "Base64UrlConverter.ToBase64UrlString: input"), typeof(ArgumentNullException), EventLevel.Verbose);

            return Convert.ToBase64String( input ).TrimEnd( '=' ).Replace( '+', '-' ).Replace( '/', '_' );
        }

        /// <summary>
        /// Converts a Base64Url encoded string to a byte array
        /// </summary>
        /// <param name="input">The Base64Url encoded string</param>
        /// <returns>The byte array represented by the enconded string</returns>
        private static byte[] FromBase64UrlString( string input )
        {
            if ( string.IsNullOrEmpty( input ) )
                LogHelper.Throw(string.Format(CultureInfo.InvariantCulture, LogMessages.IDX10000, "Base64UrlConverter.FromBase64UrlString: input"), typeof(ArgumentNullException), EventLevel.Verbose);

            return Convert.FromBase64String( Pad( input.Replace( '-', '+' ).Replace( '_', '/' ) ) );
        }

        /// <summary>
        /// Adds padding to the input
        /// </summary>
        /// <param name="input"> the input string </param>
        /// <returns> the padded string </returns>
        private static string Pad( string input )
        {
            var count = 3 - ( ( input.Length + 3 ) % 4 );

            if ( count == 0 )
            {
                return input;
            }

            return input + new string( '=', count );
        }

        public override bool CanConvert( Type objectType )
        {
            if ( objectType == typeof( byte[] ) )
                return true;

            return false;
        }

        public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        {
            if ( objectType != typeof( byte[] ) )
            {
                return serializer.Deserialize( reader, objectType );
            }
            else
            {
                var value = serializer.Deserialize<string>( reader );

                if ( !string.IsNullOrEmpty( value ) )
                {
                    return FromBase64UrlString( value );
                }
            }

            return null;
        }

        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        {
            if ( value.GetType() != typeof( byte[] ) )
            {
                JToken.FromObject( value ).WriteTo( writer ); 
            }
            else
            {
                JToken.FromObject( ToBase64UrlString( ( byte[] )value ) ).WriteTo( writer );
            }
        }
    }
}
