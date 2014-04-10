// -----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BtcE
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public static class Extensions
  {
    public static int GetPrecision(this decimal value, bool ignoreTrailingZeros = false)
    {
      var argument = ignoreTrailingZeros ? (decimal)(double)value : value;
      return BitConverter.GetBytes(decimal.GetBits(argument)[3])[2];
    }
  }
}
