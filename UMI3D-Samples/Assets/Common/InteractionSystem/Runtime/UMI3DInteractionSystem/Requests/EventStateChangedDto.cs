﻿/*
Copyright 2019 - 2021 Inetum

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;

namespace umi3d.common.interaction
{
    /// <summary>
    /// Dto to request an event to be started/stopped.
    /// </summary>
    public class EventStateChangedDto : InteractionRequestDto
    {
        /// <summary>
        /// The requested state.
        /// </summary>
        public bool active;

        protected override uint GetOperationId() { return UMI3DOperationKeys.EventStateChanged; }

        public override (int, Func<byte[], int, int>) ToByteArray(params object[] parameters)
        {
            var fb = base.ToByteArray(parameters);

            int size = sizeof(bool) + fb.Item1;
            Func<byte[], int, int> func = (b, i) =>
            {
                i += fb.Item2(b,i);
                i += UMI3DNetworkingHelper.Write(active, b, i);
                return size;
            };
            return (size, func);
        }
    }
}
