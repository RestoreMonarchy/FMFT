// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RESTFulSense.WebAssembly.Exceptions
{
    public class ValidationProblemDetails
    {
        public ValidationProblemDetails()
        {
            Errors = new Dictionary<string, string[]>(StringComparer.Ordinal);
        }

        public string Title { get; set; }
        [JsonPropertyName("errors")]
        public IDictionary<string, string[]> Errors { get; set; }
        public string Type { get; set; }
    }
}