﻿// ---------------------------------------------------------------
// Copyright (c) Brian Parker & Hassan Habib
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace RESTFulSense.WebAssembly.Models
{
    public class MisdirectedRequestObjectResult : ObjectResult
    {
        public MisdirectedRequestObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status421MisdirectedRequest;
        }
    }
}