using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FormsApp.Communication.Hubs
{
    public class UInt8GeneratorHub : Hub
    {
        public async IAsyncEnumerable<int> Generate([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var randomNumberSource = new Random();

            var buffer = new byte[1];
            while (!cancellationToken.IsCancellationRequested)
            {
                randomNumberSource.NextBytes(buffer); // basically it will be a byte(uint8) 

                yield return buffer[0];
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}