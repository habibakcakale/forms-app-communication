using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FormsApp.Communication.Hubs
{
    public class CharGeneratorHub : Hub
    {
        //The easiest way to get turkish alphabet.
        private static readonly string Alphabet = "abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

        public async IAsyncEnumerable<char> Generate([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var randomNumberSource = new Random();

            while (!cancellationToken.IsCancellationRequested)
            {
                yield return Alphabet[randomNumberSource.Next(0, Alphabet.Length - 1)];
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }
        }
    }
}