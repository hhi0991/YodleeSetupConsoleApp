using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace YodleeSetup
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var userId = "Test User Login Id goes here";

            var authToken = await CreateAuthToken(userId);

            var x = await GetAccounts(authToken);

        }


        private static async Task<string> CreateAuthToken(string userId)
        {
            const string tokenRequestUri = @"https://sandbox.api.yodlee.com:443/ysl/auth/token";
            const string clientId = "client id goes here";
            const string secret = "secret goes here";

            // Create the request to send to Yodlee
            var request = new HttpRequestMessage(HttpMethod.Post, tokenRequestUri);

            // Build the content and add it to the request
            var payload = "clientId=" + clientId + "&secret=" + secret;
            var stringContent = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            request.Content = stringContent;

            // Add header to the request with the required loginName
            request.Content.Headers.Add("loginName", userId);

            // Create the HttpClient object and add the version header
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("API-VERSION", "1.1");

            // Use the HttpClient created to send the request
            var response = await client.SendAsync(request);

            // Convert the response to a string
            var jsonString = await response.Content.ReadAsStringAsync();

            // Convert the Json string to an object
            AuthorizationToken result = JsonConvert.DeserializeObject<AuthorizationToken>(jsonString);

            // Extract the access token from the result object and return it
            return result.token.accessToken;
        }

        private static async Task<Accounts> GetAccounts(string authToken)
        {
            const string tokenRequestUri = @"https://sandbox.api.yodlee.com:443/ysl/accounts";

            // Create the request to send to Yodlee
            var request = new HttpRequestMessage(HttpMethod.Get, tokenRequestUri);

            // Create the HttpClient object and add the version header
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("API-VERSION", "1.1");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            // Use the HttpClient created to send the request
            var response = await client.SendAsync(request);

            // Convert the response to a string
            var jsonString = await response.Content.ReadAsStringAsync();

            // Convert the Json string to an object
            var result = JsonConvert.DeserializeObject<Accounts>(jsonString);
   
            // Convert the response to a string
            return result;
        }
    }

    public class Token
    {
        public string accessToken { get; set; }
        public DateTime issuedAt { get; set; }
        public int expiresIn { get; set; }
    }

    public class AuthorizationToken
    {
        public Token token { get; set; }
    }

    public class Balance
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class Cash
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class MarginBalance
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class Dataset
    {
        public string name { get; set; }
        public string additionalStatus { get; set; }
        public string updateEligibility { get; set; }
        public DateTime lastUpdated { get; set; }
        public DateTime lastUpdateAttempt { get; set; }
        public DateTime? nextUpdateScheduled { get; set; }
    }

    public class LastEmployeeContributionAmount
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class AvailableCash
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class AvailableCredit
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class LastPaymentAmount
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class RunningBalance
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class TotalCashLimit
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class TotalCreditLine
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class AmountDue
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class MinimumAmountDue
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class CurrentBalance
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class AvailableBalance
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class OriginalLoanAmount
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class PrincipalBalance
    {
        public string currency { get; set; }
        public double amount { get; set; }
    }

    public class Account
    {
        public string CONTAINER { get; set; }
        public int providerAccountId { get; set; }
        public string accountName { get; set; }
        public string accountStatus { get; set; }
        public string accountNumber { get; set; }
        public string aggregationSource { get; set; }
        public bool isAsset { get; set; }
        public Balance balance { get; set; }
        public int id { get; set; }
        public bool includeInNetWorth { get; set; }
        public string providerId { get; set; }
        public string providerName { get; set; }
        public bool isManual { get; set; }
        public string accountType { get; set; }
        public string displayedName { get; set; }
        public DateTime createdDate { get; set; }
        public Cash cash { get; set; }
        public DateTime lastUpdated { get; set; }
        public MarginBalance marginBalance { get; set; }
        public IList<Dataset> dataset { get; set; }
        public LastEmployeeContributionAmount lastEmployeeContributionAmount { get; set; }
        public string lastEmployeeContributionDate { get; set; }
        public string dueDate { get; set; }
        public double? cashApr { get; set; }
        public AvailableCash availableCash { get; set; }
        public AvailableCredit availableCredit { get; set; }
        public LastPaymentAmount lastPaymentAmount { get; set; }
        public string lastPaymentDate { get; set; }
        public RunningBalance runningBalance { get; set; }
        public TotalCashLimit totalCashLimit { get; set; }
        public TotalCreditLine totalCreditLine { get; set; }
        public AmountDue amountDue { get; set; }
        public MinimumAmountDue minimumAmountDue { get; set; }
        public CurrentBalance currentBalance { get; set; }
        public double? annualPercentageYield { get; set; }
        public double? interestRate { get; set; }
        public string maturityDate { get; set; }
        public AvailableBalance availableBalance { get; set; }
        public string classification { get; set; }
        public string interestRateType { get; set; }
        public OriginalLoanAmount originalLoanAmount { get; set; }
        public PrincipalBalance principalBalance { get; set; }
        public string originationDate { get; set; }
        public string frequency { get; set; }
        public double? apr { get; set; }
    }

    public class Accounts
    {
        public IList<Account> account { get; set; }
    }



}
