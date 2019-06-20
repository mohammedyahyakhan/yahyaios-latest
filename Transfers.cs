﻿// <copyright file="Transfers.cs" company="Eastern Bank">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ios
{
    using System;
    using System.Threading;

    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Appium.MultiTouch;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.UI;
    using RelevantCodes.ExtentReports;

    [TestFixture("local", "iphone-8-plus")]
    public class Transfers : BrowserStackNUnitTest
    {
        // Instance of extents reports
        private ExtentReports extent;

        private ExtentTest test;

        [OneTimeSetUp]
        public void StartReport()
        {
            // To obtain the current solution path/project path
            var pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            var actualPath = pth.Substring(0, pth.LastIndexOf("bin", StringComparison.Ordinal));
            var projectPath = new Uri(actualPath).LocalPath;

            // Append the html report file to current project path
            var reportPath = projectPath + "Reports\\TestRunReport.html";

            // Boolean value for replacing existing report
            // this.Extent = new ExtentReports(reportPath, true);
            this.extent = new ExtentReports(reportPath);

            // Add QA system info to html report
            this.extent.AddSystemInfo("Host Name", "iOS Execution").AddSystemInfo("Environment", "QA").AddSystemInfo("Username", "iOSExecution");

            // Adding config.xml file
            this.extent.LoadConfig(projectPath + "Extent-Config.xml"); // Get the config.xml file from http://extentreports.com
        }

        [TearDown]

        public void AfterClass()
        {
            // StackTrace details for failed Test-cases
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            // var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                this.test.Log(LogStatus.Fail, status + errorMessage);
            }

            // End test report
            this.extent.EndTest(this.test);
        }

        [OneTimeTearDown]

        public void EndReport()
        {
            // End Report
            this.extent.Flush();
            this.extent.Close();
        }

        public Transfers(string profile, string environment)
            : base(profile, environment)
        {
        }

        // public ExtentReports extent;
        // public ExtentTest test;
        [Obsolete]
        [Description("Login to the Application under test")]
        private void Login()
        {
            var usernameInputBox = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("XCUIElementTypeTextField")));

            // validating presence of username input
            Assert.AreEqual(true, usernameInputBox.Displayed);
            usernameInputBox.SendKeys("qacon1");

            var passwordInputBox = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.ClassName("XCUIElementTypeSecureTextField")));

            // validating presence of password input
            Assert.AreEqual(true, passwordInputBox.Displayed);
            passwordInputBox.SendKeys("east@1234");
            this.Driver.HideKeyboard();

            var signInButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Name("Sign In")));

            signInButton.Click();
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));

            // WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Eastern Bank Logo")));
        }

        [Test]
        [Description("Verify Regular Loan Payment Type")]
        [Obsolete]
        public void Case106362()
        {
            // Start Report
            this.test = this.extent.StartTest("CASE_106362");

            this.Login();
            var touchTransfer = new TouchAction(this.Driver);

            // TouchAction touchTransfer = new TouchAction(driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var betweenEasternAccountsButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer Between My Eastern Bank Accounts')]")));

            // Validating presence of button transfer between My Eastern Bank Accounts
            // Assert.AreEqual(true, betweenEasternAccountsButton.Displayed);
            Assert.IsTrue(betweenEasternAccountsButton.Displayed);
            this.test.Log(LogStatus.Pass, "transfer between My Eastern Bank Accounts is visible");
            betweenEasternAccountsButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Choose From Account")));

            var chooseFromAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose From Account")));

            // Validating presence of button Choose From Account
            Assert.AreEqual(true, chooseFromAccountButton.Displayed);
            this.test.Log(LogStatus.Pass, "Choose From Account is visible");
            chooseFromAccountButton.Click();

            var fromAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'ACCT *7927')]")));

            // validating presence of from account
            Assert.AreEqual(true, fromAccount.Displayed);
            this.test.Log(LogStatus.Pass, "From Account is visible");
            fromAccount.Click();

            var amountInput = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Enter Amount $0.00")));

            // validating presence of amount input
            Assert.AreEqual(true, amountInput.Displayed);
            this.test.Log(LogStatus.Pass, "Amount Input is visible");
            amountInput.SendKeys("100");

            var chooseToAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose To Account")));

            // validating presence of To account
            Assert.AreEqual(true, chooseToAccountButton.Displayed);
            chooseToAccountButton.Click();

            var toAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Revolving Credit')]")));

            // validating presence of To account
            Assert.AreEqual(true, toAccount.Displayed);
            toAccount.Click();

            var continueButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Continue")));

            // validating presence of continue button
            Assert.AreEqual(true, continueButton.Displayed);
            continueButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Now")));

            var transferNowButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Transfer Now")));

            // validating presence of Transfer Now button
            Assert.AreEqual(true, transferNowButton.Displayed);
            transferNowButton.Click();

            var result = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Transfer Successful")));
            Assert.AreEqual(true, result.Displayed);

            // "transfer successful"
            var doneButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                 ExpectedConditions.ElementExists(By.Name("Done")));

            // validating presence of done button
            Assert.AreEqual(true, doneButton.Displayed);
            doneButton.Click();
        }

        [Test]
        [Obsolete]
        public void Case106363()
        {
            // Start Report
            this.test = this.extent.StartTest("CASE_106363");
            this.Login();
            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var betweenEasternAccountsButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer Between My Eastern Bank Accounts')]")));
            Assert.AreEqual(true, betweenEasternAccountsButton.Displayed);
            this.test.Log(LogStatus.Pass, "Transfer Between My Eastern Bank Accounts is visible");
            betweenEasternAccountsButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Choose From Account")));

            var chooseFromAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose From Account")));
            Assert.AreEqual(true, chooseFromAccountButton.Displayed);
            this.test.Log(LogStatus.Pass, "Choose from account is visible");
            chooseFromAccountButton.Click();

            var fromAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'ACCT *7927')]")));
            Assert.AreEqual(true, fromAccount.Displayed);
            fromAccount.Click();

            var chooseToAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose To Account")));
            Assert.AreEqual(true, chooseToAccountButton.Displayed);
            chooseToAccountButton.Click();

            var toAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Revolving Credit')]")));
            Assert.AreEqual(true, toAccount.Displayed);
            toAccount.Click();

            var amountInput = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Enter Amount $0.00")));
            amountInput.SendKeys("100");

            var principalOnly = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Loan Payment Type Regular')]")));
            Assert.AreEqual(true, principalOnly.Displayed);
            principalOnly.Click();

            var loanPaymentType = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Principal Only A principal payment')]")));
            Assert.AreEqual(true, loanPaymentType.Displayed);
            loanPaymentType.Click();

            var continueButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Continue")));
            continueButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Now")));
            var transferNowButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Transfer Now")));
            transferNowButton.Click();

            // transfer-successful
            var doneButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                 ExpectedConditions.ElementExists(By.Name("Done")));
            Assert.AreEqual(true, doneButton.Displayed);
            doneButton.Click();
        }

        [Test]
        [Obsolete]

        public void Case106364()
        {
            // Start Report
            this.test = this.extent.StartTest("CASE_106364");
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var transferAccountsAtOtherBanks = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer To My Accounts at Other Banks')]")));
            Assert.AreEqual(true, transferAccountsAtOtherBanks.Displayed);
            transferAccountsAtOtherBanks.Click();
        }

        [Test]
        [Obsolete]

        // Verify Transfer Screen
        public void Case106365()
        {
            // Start Report
            this.test = this.extent.StartTest("CASE_106365");
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var betweenEasternAccountsButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer Between My Eastern Bank Accounts')]")));
            Assert.AreEqual(true, betweenEasternAccountsButton.Displayed);

            var transferAccountsAtOtherBank = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer To My Accounts at Other Banks')]")));
            Assert.AreEqual(true, transferAccountsAtOtherBank.Displayed);

            var transferToAnotherAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer To Another Person')]")));
            Assert.AreEqual(true, transferToAnotherAccount.Displayed);

            var viewScheduledTransfers = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'View/Manage Scheduled Transfers')]")));
            Assert.AreEqual(true, viewScheduledTransfers.Displayed);
        }

        [Test]
        [Obsolete]

        // Make One Time Now Transfer between My Eastern Bank Accounts
        public void Case106367()
        {
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var betweenEasternAccountsButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer Between My Eastern Bank Accounts')]")));
            Assert.IsTrue(betweenEasternAccountsButton.Displayed);
            betweenEasternAccountsButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Choose From Account")));

            var chooseFromAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose From Account")));
            chooseFromAccountButton.Click();

            var fromAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'ACCT *7927')]")));
            Assert.AreEqual(true, fromAccount.Displayed);
            fromAccount.Click();

            var amountInput = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Enter Amount $0.00")));
            Assert.AreEqual(true, amountInput.Displayed);
            amountInput.SendKeys("100");

            var chooseToAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose To Account")));
            Assert.AreEqual(true, chooseToAccountButton.Displayed);
            chooseToAccountButton.Click();

            var toAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'FREE CHECKING *5921')]")));
            Assert.AreEqual(true, toAccount.Displayed);
            toAccount.Click();

            var frequency = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Frequency One Time Now')]")));
            frequency.Click();

            var oneTimeNow = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'One Time Now')]")));
            oneTimeNow.Click();

            var continueButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Continue")));
            continueButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Now")));

            var transferNowButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Transfer Now")));
            transferNowButton.Click();

            // transfer successful
            var doneButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                 ExpectedConditions.ElementExists(By.Name("Done")));
            doneButton.Click();
        }

        [Test]
        [Obsolete]

        // Verify One Time Scheduled Transfer Success
        public void Case106369()
        {
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var betweenEasternAccountsButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer Between My Eastern Bank Accounts')]")));
            betweenEasternAccountsButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Choose From Account")));

            var chooseFromAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose From Account")));
            chooseFromAccountButton.Click();

            var fromAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'ACCT *7927')]")));
            fromAccount.Click();

            var amountInput = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Enter Amount $0.00")));
            amountInput.SendKeys("100");

            var chooseToAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose To Account")));
            chooseToAccountButton.Click();

            var toAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'FREE CHECKING *5921')]")));
            toAccount.Click();

            var frequency = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Frequency One Time Now')]")));
            frequency.Click();

            var oneTimeScheduled = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'One Time, Scheduled')]")));
            oneTimeScheduled.Click();

            var transferOn = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer On')]")));
            transferOn.Click();

            var close = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Close')]")));
            close.Click();

            var continueButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Continue")));
            continueButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Schedule Transfer")));

            var scheduleTransfer = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Schedule Transfer")));
            scheduleTransfer.Click();

            // transfer-successful
            var doneButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                 ExpectedConditions.ElementExists(By.Name("Done")));
            doneButton.Click();
        }

        [Test]
        [Obsolete]

        // Verify Weekly Transfer -Never Ending - Success
        public void Case106370()
        {
            this.test = this.extent.StartTest("CASE_106370");
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var betweenEasternAccountsButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer Between My Eastern Bank Accounts')]")));
            betweenEasternAccountsButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Choose From Account")));

            var chooseFromAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose From Account")));
            chooseFromAccountButton.Click();

            var fromAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'ACCT *7927')]")));
            fromAccount.Click();

            var amountInput = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Enter Amount $0.00")));
            amountInput.SendKeys("100");

            var chooseToAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose To Account")));
            chooseToAccountButton.Click();

            var toAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'FREE CHECKING *5921')]")));
            toAccount.Click();

            var frequency = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Frequency One Time Now')]")));
            frequency.Click();

            var oneTimeScheduled = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Weekly')]")));
            oneTimeScheduled.Click();

            var startOn = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Start On')]")));
            startOn.Click();

            var close = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Close')]")));
            close.Click();

            var endTransfer = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'End Transfer Never')]")));
            endTransfer.Click();

            var never = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Never')]")));
            never.Click();

            var continueButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Continue")));
            continueButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Schedule Transfer")));

            var scheduleTransfer = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                 ExpectedConditions.ElementExists(By.Name("Schedule Transfer")));
            scheduleTransfer.Click();

            // transfer-successful
            var doneButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                 ExpectedConditions.ElementExists(By.Name("Done")));
            doneButton.Click();
        }

        [Test]
        [Obsolete]

        // Verify Weekly Transfer - End after # of transfers -success.
        // **************valid hash transfers
        public void Case106371()
        {
            this.test = this.extent.StartTest("CASE_106371");
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var betweenEasternAccountsButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer Between My Eastern Bank Accounts')]")));
            betweenEasternAccountsButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Choose From Account")));

            var chooseFromAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose From Account")));
            chooseFromAccountButton.Click();

            var fromAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'ACCT *7927')]")));
            fromAccount.Click();

            var amountInput = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Enter Amount $0.00")));
            amountInput.SendKeys("100");

            var chooseToAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose To Account")));
            chooseToAccountButton.Click();

            var toAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'FREE CHECKING *5921')]")));
            toAccount.Click();

            var frequency = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Frequency One Time Now')]")));
            frequency.Click();

            var oneTimeScheduled = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Weekly')]")));
            oneTimeScheduled.Click();

            var startOn = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Start On')]")));
            startOn.Click();

            var nextMonth = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Next Month')]")));
            nextMonth.Click();

            var selectAvailableDate = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Available')]")));
            selectAvailableDate.Click();

            var endTransfer = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'End Transfer Never')]")));
            endTransfer.Click();

            var afterTransfers = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'After # Transfers')]")));
            afterTransfers.Click();

            var enterNoOfTransfers = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeStaticText[contains(@name,'Enter # Transfers')]//parent::XCUIElementTypeOther//following-sibling::XCUIElementTypeTextField")));
            enterNoOfTransfers.SendKeys("3");

            // driver.HideKeyboard();
            var continueButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Continue")));
            continueButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Schedule Transfer")));

            var scheduleTransfer = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Schedule Transfer")));
            scheduleTransfer.Click();

            // transfer-successful
            var doneButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                 ExpectedConditions.ElementExists(By.Name("Done")));
            this.test.Log(LogStatus.Pass, "End of # Transfers is scheduled");
            doneButton.Click();
        }

        [Test]
        [Obsolete]

        // Verify Weekly Transfer- End on Date Error Then Success W/Valid Recurring Date
        // ***************************************************************************
        public void Case106372()
        {
            this.test = this.extent.StartTest("CASE_106372");
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            // TouchAction touchTransfer = new TouchAction(driver);
            // touchTransfer.Tap(85, 680).Perform();
            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            // WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            // wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));
            var betweenEasternAccountsButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Transfer Between My Eastern Bank Accounts')]")));
            betweenEasternAccountsButton.Click();

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Choose From Account")));

            var chooseFromAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose From Account")));
            chooseFromAccountButton.Click();

            var fromAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'ACCT *7927')]")));
            fromAccount.Click();

            var amountInput = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Enter Amount $0.00")));
            amountInput.SendKeys("100");

            var chooseToAccountButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Choose To Account")));
            chooseToAccountButton.Click();

            var toAccount = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'FREE CHECKING *5921')]")));
            toAccount.Click();

            var frequency = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Frequency One Time Now')]")));
            frequency.Click();

            var every2Weeks = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Every 2 Weeks')]")));
            every2Weeks.Click();

            var startOn = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Start On')]")));
            startOn.Click();

            var nextMonth = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Next Month')]")));
            nextMonth.Click();

            var selectAvailableStartDate = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Available')]")));
            selectAvailableStartDate.Click();

            var endTransfer = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'End Transfer Never')]")));
            endTransfer.Click();

            var onDate = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'On Date')]")));
            Assert.AreEqual(true, onDate.Displayed);
            onDate.Click();

            var nextMonth1 = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Next Month')]")));
            nextMonth1.Click();

            var selectAvailableStartDate1 = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Available')]")));
            selectAvailableStartDate1.Click();

            var continueButton = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Continue")));
            continueButton.Click();

            var alertTitle = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Alert")));
            Assert.AreEqual(true, alertTitle.Displayed);

            var alertMessage = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Please enter a valid recurring date")));
            Assert.AreEqual("Please enter a valid recurring date", alertMessage.Text);

            var alertOk = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("OK")));
            alertOk.Click();

        }

        [Test]
        [Obsolete]

        // Verify Edit Transfer
        // ***************************************************************************
        public void Case106395()
        {
            this.test = this.extent.StartTest("CASE_106395");
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var viewManageScheduledTransfers = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'View/Manage Scheduled Transfers')]")));
            viewManageScheduledTransfers.Click();

            var ScheduledTransfers_Title = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Scheduled Transfers")));
            Assert.AreEqual(true, ScheduledTransfers_Title.Displayed);


            var EditDelete = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Recurring Weekly')]")));
            EditDelete.Click();

            var Edit = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Edit")));
            Edit.Click();

            var amountInput = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'Enter Amount')]")));
            amountInput.SendKeys("100");
            this.Driver.HideKeyboard();

            var Save = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Save")));
            Save.Click();

            var TransferSuccessfullyUpdated = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
                ExpectedConditions.ElementExists(By.Name("Transfer Successfully Updated")));
            Assert.AreEqual(true, TransferSuccessfullyUpdated.Displayed);
            
        }

        [Test]
        [Obsolete]

        // Verify Delete Only This Transfer
        // ***************************************************************************
        public void Case106396()
        {
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var viewManageScheduledTransfers = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'View/Manage Scheduled Transfers')]")));
            viewManageScheduledTransfers.Click();
        }

        [Test]
        [Obsolete]

        // Verify Delete This And All Future Transfers
        // ***************************************************************************
        public void Case106397()
        {
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var viewManageScheduledTransfers = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'View/Manage Scheduled Transfers')]")));
            viewManageScheduledTransfers.Click();
        }

        [Test]
        [Obsolete]

        // Verify Zelle Dashboard for enrolled users in QA/Staging
        // ***************************************************************************
        public void Case106406()
        {
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));

            var viewManageScheduledTransfers = (RemoteWebElement)new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1)).Until(
            ExpectedConditions.ElementExists(By.XPath("//XCUIElementTypeButton[contains(@name,'View/Manage Scheduled Transfers')]")));
            viewManageScheduledTransfers.Click();
        }

        [Test]
        [Obsolete]

        // Confirm You Can Do an Advance from a commercial loc in both online and mobile.
        // ***************************************************************************
        public void Case106411()
        {
            this.Login();

            var touchTransfer = new TouchAction(this.Driver);
            touchTransfer.Tap(85, 680).Perform();

            var wait = new WebDriverWait(this.Driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Name("Transfer Between My Eastern Bank Accounts")));
        }
    }
}
