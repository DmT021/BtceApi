// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BtcE.Extensions
{
  using System;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  internal static class Extensions
  {
    internal static int GetPrecision(this decimal value, bool ignoreTrailingZeros = false)
    {
      var argument = ignoreTrailingZeros ? (decimal)(double)value : value;
      return BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];
    }
  }
}
