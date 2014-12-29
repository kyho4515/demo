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
    private static string _TestFail;
    private static bool _IsFail;
    private static StreamWriter file = new StreamWriter("D:/SourceControl/Victor Ho/testfail.txt");

    [ClassInitialize]
    public static void Init(TestContext x)
    {
      file.Write("The bugs in debit function are:");
      file.WriteLine();
    }
    [TestCleanup]
    public void TestClean()
    {

        file.Write(_TestFail);
        file.WriteLine();

    }
    [TestInitialize]
    public void TestInit()
    {
      _IsFail = false;
    }

    [ClassCleanup]
    public static void Clean()
    {
      file.Close();
    }
    [TestMethod]
    public void AllBug()
    {
      double beginningBalance = 11.99;
      double debitAmount = 3.55;
      double expected = 7.44;
      bool IsCatchExceed = false;
      bool IsCatchLess = false;
      BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
      try
      {
        account.Debit(debitAmount);
      }
      catch(ArgumentOutOfRangeException e)
      {
        
        if(e.Message == BankAccount.DebitAmountExceedsBalanceMessage)
        {
          StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
          IsCatchExceed = true;
          _IsFail = false;
          _TestFail = "Debit amount exceeding balance problem is OK";
        }
        else
        {
          StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
          IsCatchLess = true;
          _IsFail = false;
          _TestFail = "Debit amount less than balance problem is OK";
        }
      }
      double actual = account.Balance;
      if (debitAmount > beginningBalance && !IsCatchExceed)
      {
        _IsFail = true;
        _TestFail = BankAccount.DebitAmountExceedsBalanceMessage;
      }
      else if (debitAmount < 0 && !IsCatchLess)
      {
        _IsFail = true;
        _TestFail = BankAccount.DebitAmountLessThanZeroMessage;
      }
      try
      {
        Assert.AreEqual(expected, actual, 0.01, "Account not debited correctly");
      }
      catch(AssertFailedException)
      {
        _TestFail = "Account not debited correctly";
        Assert.AreEqual(expected, actual, 0.01, "Account not debited correctly");
      }
      _TestFail = "Account debited correctly";
    }


    /*
    [TestMethod]
    public void Debit_WithValidAmount_UpdatesBalance()
    {
      // arrange 
      double beginningBalance = 11.99;
      double debitAmount = 3.55;
      double expected = 7.44;
      BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
      // act
      account.Debit(debitAmount);
      // assert
      double actual = account.Balance;
      try
      {
        Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
      }
      catch (AssertFailedException)
      {
        _IsFail = true;
        _TestFail = "Account not debited correctly";
        throw new AssertFailedException();
      }
    }

    [TestMethod]
    public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
    {
      // arrange
      double beginningBalance = 11.99;
      double debitAmount = -5.00;
      bool iscatch = false;
      BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
      // act
      try
      {
        account.Debit(debitAmount);
      }
      catch (ArgumentOutOfRangeException e)
      {
        // assert
        iscatch = true;
        StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
      }
      if (!iscatch)
      {
        _IsFail = true;
        _TestFail = "Debit amount less than zero";
      }
      // assert is handled by ExpectedException
    }

     //unit test method
     [TestMethod]
     public void Debit_WhenAmountIsGreaterThanBalance_ShouldThrowArgumentOutOfRange()
     {
       // arrange
       double beginningBalance = 11.99;
       double debitAmount = 100.00;
       bool iscatch = false;
       BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

       // act
       try
       {
         account.Debit(debitAmount);
       }
       catch (ArgumentOutOfRangeException e)
       {
         // assert         
         iscatch = true;
         StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
       }
       if (!iscatch)
       {
         _IsFail = true;
         _TestFail = "Debit amount exceeds balance";
       }
       
       default
       {

       }
       
       // assert is handled by ExpectedException
     }
  */
   }
}
