namespace BtcE
{
  using System;

  [Serializable]
  public class BtceException : Exception
  {
    public BtceException(string p)
      : base(p) { }
  }
}