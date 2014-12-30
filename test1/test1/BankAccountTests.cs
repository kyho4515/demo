using System.IO;
using System;
using System.Windows.Input;
using BankAccountNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test1
{
  [TestClass]
  public class BankAccountTests
  {
    private string _TestFail;
    private string _TestSuc;
    private bool _IsFail;
    private static StreamWriter file = new StreamWriter("D:/SourceControl/Victor Ho/report.txt");

    [ClassInitialize]
    public static void Init(TestContext x)
    {
      file.Write("The report about debit function is:");
      file.WriteLine();
    }

    [TestCleanup]
    public void TestClean()
    {
      if (_IsFail)
      {
        file.Write(_TestFail);
      }
      else
      {
        file.Write(_TestSuc);
      }
      file.WriteLine();
    }

    [ClassCleanup]
    public static void Clean()
    {
      file.Close();
    }

    private void AllDebug(double beginningBalance, double debitAmount, double expectedInt, string expectedStr = "Nothing")
    {
      BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
      try
      {
        account.Debit(debitAmount);
      }
      catch(ArgumentOutOfRangeException e)
      {
        try
        {
          StringAssert.Contains (e.Message, expectedStr);
        }
        catch (AssertFailedException)
        {
          _IsFail = true;
          if (expectedInt < 0)
          {
            _TestFail = "Throwing incorrect exception message";
            StringAssert.Contains (e.Message, expectedStr);
          }
          else
          {
            _TestFail = "Throwing exception at incorrect condition";
            Assert.Fail ("We don't expect any exception");
          }
        }
      }
      if (expectedInt < 0)
      {
        _IsFail = false;
        _TestSuc = "Throwing exception correctly";
        return;
      }
      double actual = account.Balance;
      try
      {
        Assert.AreEqual (expectedInt, actual, 0.01, "Account not debited correctly");
      }
      catch (AssertFailedException)
      {
        _IsFail = true;
        _TestFail = "Account not debited correctly";
        Assert.AreEqual (expectedInt, actual, 0.01, "Account not debited correctly");
      }
      _IsFail = false;
      _TestSuc = "Account debited correctly";
    }


    [TestMethod]
    public void Debit_WithValidAmount_UpdatesBalance()
    {
      AllDebug(11.99,3.55,7.44);
    }

    [TestMethod]
    public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
    {
      AllDebug(11.99,-5.00,-1,BankAccount.DebitAmountLessThanZeroMessage);
    }

     //unit test method
     [TestMethod]
     public void Debit_WhenAmountIsGreaterThanBalance_ShouldThrowArgumentOutOfRange()
     {
       AllDebug(11.99,100.00,-1,BankAccount.DebitAmountExceedsBalanceMessage);
     }
   }
}
