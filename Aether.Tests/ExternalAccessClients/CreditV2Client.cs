using Aether.Extensions;
using Aether.ExternalAccessClients;
using Aether.ExternalAccessClients.Interfaces;
using Aether.Models.Configuration;
using AutoBogus;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aether.Tests.ExternalAccessClients
{
    [TestClass]
    public class CreditV2ClientTests
    {
        CreditV2Client _target;
        Mock<IHttpClientWrapper> _mockHttpClient;

        [TestInitialize]
        public void Init()
        {
            _mockHttpClient = new Mock<IHttpClientWrapper>();

            _mockHttpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new System.Net.Http.HttpResponseMessage() { Content = new StringContent(testMismo) });

            CreditV2Configuration config = AutoFaker.Generate<CreditV2Configuration>();

            _target = new CreditV2Client(_mockHttpClient.Object, Options.Create(config));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreditClient_nullDependencies_ArgumentNullException()
        {
            var target = new CreditV2Client(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreditClient_nullConfigurations_ArgumentNullException()
        {
            var target = new CreditV2Client(new HttpClientWrapper(null, null), Options.Create(new Aether.Models.Configuration.CreditV2Configuration()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CreditClient_PullCredit_ArgumentNullException()
        {
            await _target.PullCredit(null);
        }

        [TestMethod]
        public async Task CreditClient_PullCredit()
        {
            string guidString = Guid.NewGuid().ToString();

            await _target.PullCredit(guidString);

            _mockHttpClient.Verify(x => x.GetAsync(It.Is<string>(s => s.EndsWith(guidString))));
        }

        #region testData

        private string testMismo = "<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>" + Environment.NewLine +
"<RESPONSE_GROUP MISMOVersionID=\"2.3.1\" _ID=\"RGRept000001\">" + Environment.NewLine +
"<RESPONDING_PARTY _Name=\"CoreLogic Credco\" _StreetAddress=\"P.O. BOX 509124\" _StreetAddress2=\"\" _City=\"SAN DIEGO\" _State=\"CA\" _PostalCode=\"92150\">" + Environment.NewLine +
"<CONTACT_DETAIL>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Phone\" _Value=\"800 986 4343\"/>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Fax\" _Value=\"800 523 0688\"/>" + Environment.NewLine +
"</CONTACT_DETAIL>" + Environment.NewLine +
"</RESPONDING_PARTY>" + Environment.NewLine +
"<RESPOND_TO_PARTY _Name=\"QUICKEN HOME LOANS INC - TEST ACCOUNT\" _StreetAddress=\"20555 VICTOR PARKWAY\" _City=\"LIVONIA\" _State=\"MI\" _PostalCode=\"48152\">" + Environment.NewLine +
"<CONTACT_DETAIL _Name=\"IT BUSINESS OFFICE\">" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Phone\" _Value=\"7344621431\" _PreferenceIndicator=\"Y\"/>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Fax\" _Value=\"7348058826\" _PreferenceIndicator=\"Y\"/>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Email\" _PreferenceIndicator=\"Y\"/>" + Environment.NewLine +
"</CONTACT_DETAIL>" + Environment.NewLine +
"</RESPOND_TO_PARTY>" + Environment.NewLine +
"<RESPONSE _ID=\"REQRept000001\" ResponseDateTime=\"2021-06-17T11:19:02.843-07:00\" InternalAccountIdentifier=\"4000460\">" + Environment.NewLine +
"<KEY _Name=\"EquifaxBeacon5.0_MinimumValue\" _Value=\"334\"/>" + Environment.NewLine +
"<KEY _Name=\"EquifaxBeacon5.0_MaximumValue\" _Value=\"818\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRankPercent_CRRept2147483591001-30057526579_CRScoEFX011009-30057526579\" _Value=\"46\"/>" + Environment.NewLine +
"<KEY _Name=\"ExperianFairIsaac_MinimumValue\" _Value=\"320\"/>" + Environment.NewLine +
"<KEY _Name=\"ExperianFairIsaac_MaximumValue\" _Value=\"844\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRankPercent_CRRept2147483591001-30057526579_CRScoXPN021002-30057526579\" _Value=\"41\"/>" + Environment.NewLine +
"<KEY _Name=\"FICORiskScoreClassic04_MinimumValue\" _Value=\"309\"/>" + Environment.NewLine +
"<KEY _Name=\"FICORiskScoreClassic04_MaximumValue\" _Value=\"839\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRankPercent_CRRept2147483591001-30057526579_CRScoTUC031009-30057526579\" _Value=\"44\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_IntervalCount\" _Value=\"8\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval1_LowValue\" _Value=\"334\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval1_HighValue\" _Value=\"499\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval1_OccurencePercent\" _Value=\"3\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval2_LowValue\" _Value=\"500\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval2_HighValue\" _Value=\"549\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval2_OccurencePercent\" _Value=\"5\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval3_LowValue\" _Value=\"550\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval3_HighValue\" _Value=\"599\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval3_OccurencePercent\" _Value=\"8\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval4_LowValue\" _Value=\"600\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval4_HighValue\" _Value=\"649\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval4_OccurencePercent\" _Value=\"11\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval5_LowValue\" _Value=\"650\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval5_HighValue\" _Value=\"699\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval5_OccurencePercent\" _Value=\"14\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval6_LowValue\" _Value=\"700\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval6_HighValue\" _Value=\"749\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval6_OccurencePercent\" _Value=\"16\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval7_LowValue\" _Value=\"750\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval7_HighValue\" _Value=\"799\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval7_OccurencePercent\" _Value=\"24\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval8_LowValue\" _Value=\"800\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval8_HighValue\" _Value=\"818\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_EquifaxBeacon5.0_Interval8_OccurencePercent\" _Value=\"19\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_IntervalCount\" _Value=\"8\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval1_LowValue\" _Value=\"320\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval1_HighValue\" _Value=\"499\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval1_OccurencePercent\" _Value=\"2\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval2_LowValue\" _Value=\"500\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval2_HighValue\" _Value=\"549\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval2_OccurencePercent\" _Value=\"6\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval3_LowValue\" _Value=\"550\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval3_HighValue\" _Value=\"599\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval3_OccurencePercent\" _Value=\"10\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval4_LowValue\" _Value=\"600\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval4_HighValue\" _Value=\"649\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval4_OccurencePercent\" _Value=\"10\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval5_LowValue\" _Value=\"650\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval5_HighValue\" _Value=\"699\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval5_OccurencePercent\" _Value=\"12\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval6_LowValue\" _Value=\"700\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval6_HighValue\" _Value=\"749\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval6_OccurencePercent\" _Value=\"14\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval7_LowValue\" _Value=\"750\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval7_HighValue\" _Value=\"799\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval7_OccurencePercent\" _Value=\"23\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval8_LowValue\" _Value=\"800\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval8_HighValue\" _Value=\"844\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_ExperianFairIsaac_Interval8_OccurencePercent\" _Value=\"23\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_IntervalCount\" _Value=\"8\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval1_LowValue\" _Value=\"309\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval1_HighValue\" _Value=\"499\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval1_OccurencePercent\" _Value=\"3\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval2_LowValue\" _Value=\"500\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval2_HighValue\" _Value=\"549\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval2_OccurencePercent\" _Value=\"6\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval3_LowValue\" _Value=\"550\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval3_HighValue\" _Value=\"599\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval3_OccurencePercent\" _Value=\"8\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval4_LowValue\" _Value=\"600\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval4_HighValue\" _Value=\"649\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval4_OccurencePercent\" _Value=\"9\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval5_LowValue\" _Value=\"650\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval5_HighValue\" _Value=\"699\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval5_OccurencePercent\" _Value=\"13\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval6_LowValue\" _Value=\"700\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval6_HighValue\" _Value=\"749\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval6_OccurencePercent\" _Value=\"17\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval7_LowValue\" _Value=\"750\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval7_HighValue\" _Value=\"799\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval7_OccurencePercent\" _Value=\"26\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval8_LowValue\" _Value=\"800\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval8_HighValue\" _Value=\"839\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditScoreRange_FICORiskScoreClassic04_Interval8_OccurencePercent\" _Value=\"18\"/>" + Environment.NewLine +
"<KEY _Name=\"CBid\" _Value=\"2026864\"/>" + Environment.NewLine +
"<KEY _Name=\"Loan Number\" _Value=\"3480898660\"/>" + Environment.NewLine +
"<KEY _Name=\"RMLoanId\" _Value=\"\"/>" + Environment.NewLine +
"<KEY _Name=\"AnalyzerProfId\" _Value=\"\"/>" + Environment.NewLine +
"<KEY _Name=\"Environment\" _Value=\"Test\"/>" + Environment.NewLine +
"<KEY _Name=\"LoanPurpose\" _Value=\"Refinance\"/>" + Environment.NewLine +
"<KEY _Name=\"RefinancePurpose\" _Value=\"\"/>" + Environment.NewLine +
"<KEY _Name=\"PullType\" _Value=\"TriMerge\"/>" + Environment.NewLine +
"<KEY _Name=\"CreditRequestGuid\" _Value=\"7d9b9920-cf98-11eb-ae53-005056af3ab6\"/>" + Environment.NewLine +
"<KEY _Name=\"IsNewGuid\" _Value=\"Y\"/>" + Environment.NewLine +
"<KEY _Name=\"LOS System\" _Value=\"QuickenLoans\"/>" + Environment.NewLine +
"<KEY _Name=\"SubmittingParty\" _Value=\"Lakewood\"/>" + Environment.NewLine +
"<KEY _Name=\"MinScore\" _Value=\"0\"/>" + Environment.NewLine +
"<RESPONSE_DATA>" + Environment.NewLine +
"<CREDIT_RESPONSE MISMOVersionID=\"2.3.1\" CreditReportMergeType=\"Blend\" CreditRatingCodeType=\"Other\" CreditRatingCodeTypeOtherDescription=\"CREDCO\" CreditResponseID=\"CRRept2147483591001-30057526579\" CreditReportType=\"Merge\" CreditReportIdentifier=\"300575265790000\" CreditReportTransactionIdentifier=\"30057526579\" CreditReportFirstIssuedDate=\"2021-06-17T11:19:02.779-07:00\" CreditReportLastUpdatedDate=\"2021-06-17T11:19:02.843-07:00\">" + Environment.NewLine +
"<CREDIT_BUREAU _Name=\"CoreLogic Credco\" _StreetAddress=\"P.O. BOX 509124\" _StreetAddress2=\"\" _City=\"SAN DIEGO\" _State=\"CA\" _PostalCode=\"92150\">" + Environment.NewLine +
"<CONTACT_DETAIL>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Phone\" _RoleType=\"Work\" _PreferenceIndicator=\"Y\" _Value=\"800 986 4343\"/>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Fax\" _RoleType=\"Work\" _PreferenceIndicator=\"N\" _Value=\"800 523 0688\"/>" + Environment.NewLine +
"</CONTACT_DETAIL>" + Environment.NewLine +
"</CREDIT_BUREAU>" + Environment.NewLine +
"<CREDIT_REPORT_PRICE _Type=\"Other\" _TypeOtherDescription=\"Transaction\" _Amount=\"0.000000\"/>" + Environment.NewLine +
"<CREDIT_REPORT_PRICE _Type=\"Total\" _Amount=\"0.000000\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY_INCLUDED _EquifaxIndicator=\"Y\" _ExperianIndicator=\"Y\" _TransUnionIndicator=\"Y\" _OtherRepositoryName=\"Credco\"/>" + Environment.NewLine +
"<REQUESTING_PARTY InternalAccountIdentifier=\"4000460\" LenderCaseIdentifier=\"3480898660\" _Name=\"QuickenLoans\" _StreetAddress=\"123 Main Street\" _City=\"Detroit\" _State=\"MI\" _PostalCode=\"48226\" _RequestedByName=\"mphill02\"/>" + Environment.NewLine +
"<CREDIT_REQUEST_DATA BorrowerID=\"Borrower1\" CreditReportRequestActionType=\"Submit\" CreditReportType=\"Merge\" CreditRequestType=\"Individual\">" + Environment.NewLine +
"<CREDIT_REPOSITORY_INCLUDED _EquifaxIndicator=\"Y\" _ExperianIndicator=\"Y\" _TransUnionIndicator=\"Y\"/>" + Environment.NewLine +
"</CREDIT_REQUEST_DATA>" + Environment.NewLine +
"<BORROWER BorrowerID=\"Borrower1\" _PrintPositionType=\"Borrower\" _BirthDate=\"1965-01-01\" _FirstName=\"ALICE\" _LastName=\"FIRSTIMER\" _SSN=\"991919991\" MaritalStatusType=\"NotProvided\">" + Environment.NewLine +
"<_RESIDENCE _StreetAddress=\"9991 N WARFORD STREET\" _City=\"DAWSON\" _State=\"IA\" _PostalCode=\"50066\" BorrowerResidencyType=\"Current\">" + Environment.NewLine +
"<PARSED_STREET_ADDRESS _HouseNumber=\"9991\" _StreetName=\"N WARFORD STREET\"/>" + Environment.NewLine +
"</_RESIDENCE>" + Environment.NewLine +
"</BORROWER>" + Environment.NewLine +
"<CREDIT_LIABILITY CreditLiabilityID=\"CRLiab011005-30057526579\" BorrowerID=\"Borrower1\" CreditFileID=\"CRFilEFX000001-30057526579\" _AccountIdentifier=\"3563A019732\" _AccountOpenedDate=\"2018-09-15\" _AccountOwnershipType=\"Individual\" _AccountReportedDate=\"2021-04\" _AccountBalanceDate=\"2021-04\" _AccountStatusDate=\"2018-09-15\" _AccountStatusType=\"Open\" _AccountType=\"Installment\" _ConsumerDisputeIndicator=\"N\" _CreditLimitAmount=\"4400\" _DerogatoryDataIndicator=\"N\" _HighBalanceAmount=\"4400\" _LastActivityDate=\"2021-04\" _MonthlyPaymentAmount=\"123\" _MonthsRemainingCount=\"5\" _MonthsReviewedCount=\"32\" _PastDueAmount=\"0\" _TermsDescription=\"Monthly\" _TermsMonthsCount=\"36\" _TermsSourceType=\"Provided\" _UnpaidBalanceAmount=\"2600\" CreditBusinessType=\"Banking\" CreditCounselingIndicator=\"N\" CreditLoanType=\"Lease\">" + Environment.NewLine +
"<_CREDITOR _Name=\"MOUNTAIN BANK\"/>" + Environment.NewLine +
"<_CURRENT_RATING _Code=\"1\" _Type=\"AsAgreed\"/>" + Environment.NewLine +
"<_LATE_COUNT _30Days=\"00\" _60Days=\"00\" _90Days=\"00\"/>" + Environment.NewLine +
"<_PAYMENT_PATTERN _Data=\"CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC\" _StartDate=\"2021-04\"/>" + Environment.NewLine +
"<CREDIT_COMMENT _Code=\"41\" _SourceType=\"CreditBureau\" _Type=\"BureauRemarks\">" + Environment.NewLine +
"<_Text>LEASE</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_COMMENT _Code=\"48\" _SourceType=\"CreditBureau\" _Type=\"BureauRemarks\">" + Environment.NewLine +
"<_Text>PAYMENTS BEING MADE</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"Equifax\" _SubscriberCode=\"134BB01201\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"Experian\" _SubscriberCode=\"AC1119999\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"TransUnion\" _SubscriberCode=\"BB0744C00\"/>" + Environment.NewLine +
"</CREDIT_LIABILITY>" + Environment.NewLine +
"<CREDIT_LIABILITY CreditLiabilityID=\"CRLiab011006-30057526579\" BorrowerID=\"Borrower1\" CreditFileID=\"CRFilEFX000001-30057526579\" _AccountClosedDate=\"2018-09\" _AccountIdentifier=\"1590345859403\" _AccountOpenedDate=\"2013-08-15\" _AccountOwnershipType=\"Individual\" _AccountPaidDate=\"2021-04-15\" _AccountReportedDate=\"2021-04-15\" _AccountBalanceDate=\"2018-09\" _AccountStatusDate=\"2018-09\" _AccountStatusType=\"Closed\" _AccountType=\"Installment\" _ConsumerDisputeIndicator=\"N\" _CreditLimitAmount=\"9000\" _DerogatoryDataIndicator=\"Y\" _HighBalanceAmount=\"9000\" _LastActivityDate=\"2021-04-15\" _MonthsReviewedCount=\"70\" _PastDueAmount=\"0\" _TermsDescription=\"Monthly\" _TermsMonthsCount=\"60\" _UnpaidBalanceAmount=\"0\" CreditBusinessType=\"Banking\" CreditCounselingIndicator=\"N\" CreditLoanType=\"InstallmentLoan\">" + Environment.NewLine +
"<_CREDITOR _Name=\"CENTRAL BANK\"/>" + Environment.NewLine +
"<_CURRENT_RATING _Code=\"1\" _Type=\"AsAgreed\"/>" + Environment.NewLine +
"<_HIGHEST_ADVERSE_RATING _Amount=\"150\" _Code=\"2\" _Date=\"2020-02\" _Type=\"Late30Days\"/>" + Environment.NewLine +
"<_LATE_COUNT _30Days=\"05\" _60Days=\"00\" _90Days=\"00\"/>" + Environment.NewLine +
"<_PAYMENT_PATTERN _Data=\"CCCCCCCCCCCCCC1CCCCCCCCCCCCCCCCCCCCCCCCCCC1CC111CCCCCCCCCCCCCCCCCCCCCX\" _StartDate=\"2021-04-15\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"2\" _Type=\"Late30Days\" _Date=\"2020-02\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"2\" _Type=\"Late30Days\" _Date=\"2017-10\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"2\" _Type=\"Late30Days\" _Date=\"2017-07\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"2\" _Type=\"Late30Days\" _Date=\"2017-06\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"2\" _Type=\"Late30Days\" _Date=\"2017-05\"/>" + Environment.NewLine +
"<CREDIT_COMMENT _Code=\"394\" _SourceType=\"CreditBureau\" _Type=\"BureauRemarks\">" + Environment.NewLine +
"<_Text>INSTALLMENT SALES CONTRACT</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_COMMENT _Code=\"48\" _SourceType=\"CreditBureau\" _Type=\"BureauRemarks\">" + Environment.NewLine +
"<_Text>PAYMENTS BEING MADE</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"Experian\" _SubscriberCode=\"BI1014152\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"Equifax\" _SubscriberCode=\"654BB01776\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"TransUnion\" _SubscriberCode=\"B00021208\"/>" + Environment.NewLine +
"</CREDIT_LIABILITY>" + Environment.NewLine +
"<CREDIT_LIABILITY CreditLiabilityID=\"CRLiab011007-30057526579\" BorrowerID=\"Borrower1\" CreditFileID=\"CRFilEFX000001-30057526579\" _AccountIdentifier=\"98E543184\" _AccountOpenedDate=\"2013-06-15\" _AccountOwnershipType=\"Individual\" _AccountReportedDate=\"2021-04\" _AccountBalanceDate=\"2021-04\" _AccountStatusDate=\"2013-06-15\" _AccountStatusType=\"Open\" _AccountType=\"Revolving\" _ConsumerDisputeIndicator=\"N\" _CreditLimitAmount=\"1000\" _DerogatoryDataIndicator=\"N\" _HighCreditAmount=\"1000\" _LastActivityDate=\"2021-04\" _MonthlyPaymentAmount=\"44\" _MonthsReviewedCount=\"26\" _PastDueAmount=\"0\" _TermsDescription=\"Monthly\" _TermsMonthsCount=\"0\" _TermsSourceType=\"Provided\" _UnpaidBalanceAmount=\"437\" CreditBusinessType=\"Banking\" CreditCounselingIndicator=\"N\" CreditLoanType=\"CreditCard\">" + Environment.NewLine +
"<_CREDITOR _Name=\"HEMLOCKS\"/>" + Environment.NewLine +
"<_CURRENT_RATING _Code=\"1\" _Type=\"AsAgreed\"/>" + Environment.NewLine +
"<_LATE_COUNT _30Days=\"00\" _60Days=\"00\" _90Days=\"00\"/>" + Environment.NewLine +
"<_PAYMENT_PATTERN _Data=\"CCCCCCCCCCCCCCCCCCCCCCCCCC\" _StartDate=\"2021-04\"/>" + Environment.NewLine +
"<CREDIT_COMMENT _Code=\"265\" _SourceType=\"CreditBureau\" _Type=\"BureauRemarks\">" + Environment.NewLine +
"<_Text>CREDIT CARD</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_COMMENT _Code=\"48\" _SourceType=\"CreditBureau\" _Type=\"BureauRemarks\">" + Environment.NewLine +
"<_Text>PAYMENTS BEING MADE</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"Equifax\" _SubscriberCode=\"155BB03495\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"Experian\" _SubscriberCode=\"BC2810893\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"TransUnion\" _SubscriberCode=\"BB703Q001\"/>" + Environment.NewLine +
"</CREDIT_LIABILITY>" + Environment.NewLine +
"<CREDIT_LIABILITY CreditLiabilityID=\"CRLiab011008-30057526579\" BorrowerID=\"Borrower1\" CreditFileID=\"CRFilEFX000001-30057526579\" _AccountIdentifier=\"4681123R1\" _AccountOpenedDate=\"2012-08-15\" _AccountOwnershipType=\"Individual\" _AccountReportedDate=\"2021-04\" _AccountBalanceDate=\"2021-04\" _AccountStatusDate=\"2012-08-15\" _AccountStatusType=\"Open\" _AccountType=\"Revolving\" _ConsumerDisputeIndicator=\"N\" _CreditLimitAmount=\"1600\" _DerogatoryDataIndicator=\"Y\" _HighCreditAmount=\"1600\" _LastActivityDate=\"2021-04\" _MonthsReviewedCount=\"39\" _PastDueAmount=\"0\" _TermsDescription=\"Monthly\" _TermsMonthsCount=\"0\" _UnpaidBalanceAmount=\"0\" CreditBusinessType=\"RealEstateAndPublicAccommodation\" CreditCounselingIndicator=\"N\" CreditLoanType=\"CreditCard\">" + Environment.NewLine +
"<_CREDITOR _Name=\"BAY COMPANY\"/>" + Environment.NewLine +
"<_CURRENT_RATING _Code=\"1\" _Type=\"AsAgreed\"/>" + Environment.NewLine +
"<_HIGHEST_ADVERSE_RATING _Amount=\"120\" _Code=\"3\" _Date=\"2018-08\" _Type=\"Late60Days\"/>" + Environment.NewLine +
"<_LATE_COUNT _30Days=\"02\" _60Days=\"04\" _90Days=\"00\"/>" + Environment.NewLine +
"<_PAYMENT_PATTERN _Data=\"CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC222112X\" _StartDate=\"2021-04\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"3\" _Type=\"Late60Days\" _Date=\"2018-08\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"3\" _Type=\"Late60Days\" _Date=\"2018-07\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"3\" _Type=\"Late60Days\" _Date=\"2018-06\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"2\" _Type=\"Late30Days\" _Date=\"2018-05\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"2\" _Type=\"Late30Days\" _Date=\"2018-04\"/>" + Environment.NewLine +
"<_PRIOR_ADVERSE_RATING _Code=\"3\" _Type=\"Late60Days\" _Date=\"2018-03\"/>" + Environment.NewLine +
"<CREDIT_COMMENT _Code=\"265\" _SourceType=\"CreditBureau\" _Type=\"BureauRemarks\">" + Environment.NewLine +
"<_Text>CREDIT CARD</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_COMMENT _Code=\"48\" _SourceType=\"CreditBureau\" _Type=\"BureauRemarks\">" + Environment.NewLine +
"<_Text>PAYMENTS BEING MADE</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"Equifax\" _SubscriberCode=\"217RA01272\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"Experian\" _SubscriberCode=\"BC1728085\"/>" + Environment.NewLine +
"<CREDIT_REPOSITORY _SourceType=\"TransUnion\" _SubscriberCode=\"B00103213\"/>" + Environment.NewLine +
"</CREDIT_LIABILITY>" + Environment.NewLine +
"<CREDIT_FILE CreditFileID=\"CRFilEFX000001-30057526579\" BorrowerID=\"Borrower1\" CreditScoreID=\"CRScoEFX011009-30057526579\" CreditRepositorySourceType=\"Equifax\" _InfileDate=\"1984-09-15\" _ResultStatusType=\"FileReturned\">" + Environment.NewLine +
"<_BORROWER _BirthDate=\"1965-03-15\" _FirstName=\"ALICE\" _LastName=\"FIRSTIMER\" _SSN=\"991919991\">" + Environment.NewLine +
"<_RESIDENCE _StreetAddress=\"9991 WARFORD  ST\" _City=\"DAWSON\" _State=\"IA\" _PostalCode=\"50066-0000\" BorrowerResidencyType=\"Current\">" + Environment.NewLine +
"<PARSED_STREET_ADDRESS _HouseNumber=\"9991\" _StreetName=\"WARFORD\" _StreetSuffix=\"ST\"/>" + Environment.NewLine +
"</_RESIDENCE>" + Environment.NewLine +
"<EMPLOYER _Name=\"EMPLOYER X\" EmploymentCurrentIndicator=\"Y\" EmploymentPositionDescription=\"PROFESSIONAL\"/>" + Environment.NewLine +
"</_BORROWER>" + Environment.NewLine +
"<_VARIATION _Type=\"DifferentBirthDate\"/>" + Environment.NewLine +
"<CREDIT_COMMENT _SourceType=\"Equifax\" _Type=\"Other\" _TypeOtherDescription=\"SSNMessage\">" + Environment.NewLine +
"<_Text>SSN Matches</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"</CREDIT_FILE>" + Environment.NewLine +
"<CREDIT_FILE CreditFileID=\"CRFilXPN000002-30057526579\" BorrowerID=\"Borrower1\" CreditScoreID=\"CRScoXPN021002-30057526579\" CreditRepositorySourceType=\"Experian\" _ResultStatusType=\"FileReturned\">" + Environment.NewLine +
"<_BORROWER _BirthDate=\"1965-04-15\" _FirstName=\"ALICE\" _LastName=\"FIRSTIMER\" _SSN=\"991919991\">" + Environment.NewLine +
"<EMPLOYER _Name=\"EMPLOYER X\" EmploymentCurrentIndicator=\"N\" EmploymentReportedDate=\"2018-01-15\"/>" + Environment.NewLine +
"</_BORROWER>" + Environment.NewLine +
"<_VARIATION _Type=\"DifferentBirthDate\"/>" + Environment.NewLine +
"<CREDIT_COMMENT _SourceType=\"Experian\" _Type=\"Other\" _TypeOtherDescription=\"SSNMessage\">" + Environment.NewLine +
"<_Text>SSN Matches</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"</CREDIT_FILE>" + Environment.NewLine +
"<CREDIT_FILE CreditFileID=\"CRFilTUC000003-30057526579\" BorrowerID=\"Borrower1\" CreditScoreID=\"CRScoTUC031009-30057526579\" CreditRepositorySourceType=\"TransUnion\" _InfileDate=\"2012-12-15\" _ResultStatusType=\"FileReturned\">" + Environment.NewLine +
"<_BORROWER _BirthDate=\"1965-04-15\" _FirstName=\"ALICE\" _LastName=\"FIRSTIMER\" _SSN=\"991919991\">" + Environment.NewLine +
"<EMPLOYER _Name=\"EMPLOYER X\" EmploymentCurrentIndicator=\"N\" EmploymentPositionDescription=\"PROFESSIONAL\"/>" + Environment.NewLine +
"</_BORROWER>" + Environment.NewLine +
"<_VARIATION _Type=\"DifferentBirthDate\"/>" + Environment.NewLine +
"<CREDIT_COMMENT _SourceType=\"TransUnion\" _Type=\"Other\" _TypeOtherDescription=\"SSNMessage\">" + Environment.NewLine +
"<_Text>SSN Matches</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"</CREDIT_FILE>" + Environment.NewLine +
"<CREDIT_SCORE CreditScoreID=\"CRScoEFX011009-30057526579\" CreditReportIdentifier=\"300575265790000\" BorrowerID=\"Borrower1\" CreditFileID=\"CRFilEFX000001-30057526579\" CreditRepositorySourceType=\"Equifax\" _Date=\"2021-06-17\" _FACTAInquiriesIndicator=\"N\" _ModelNameType=\"EquifaxBeacon5.0\" _Value=\"+715\"/>" + Environment.NewLine +
"<CREDIT_SCORE CreditScoreID=\"CRScoXPN021002-30057526579\" CreditReportIdentifier=\"300575265790000\" BorrowerID=\"Borrower1\" CreditFileID=\"CRFilXPN000002-30057526579\" CreditRepositorySourceType=\"Experian\" _Date=\"2021-06-17\" _FACTAInquiriesIndicator=\"N\" _ModelNameType=\"ExperianFairIsaac\" _Value=\"+703\"/>" + Environment.NewLine +
"<CREDIT_SCORE CreditScoreID=\"CRScoTUC031009-30057526579\" CreditReportIdentifier=\"300575265790000\" BorrowerID=\"Borrower1\" CreditFileID=\"CRFilTUC000003-30057526579\" CreditRepositorySourceType=\"TransUnion\" _Date=\"2021-06-17\" _FACTAInquiriesIndicator=\"N\" _ModelNameType=\"FICORiskScoreClassic04\" _Value=\"+710\"/>" + Environment.NewLine +
"<CREDIT_COMMENT _SourceType=\"CreditBureau\">" + Environment.NewLine +
"<_Text>SSN Possible ITIN</_Text>" + Environment.NewLine +
"</CREDIT_COMMENT>" + Environment.NewLine +
"<CREDIT_CONSUMER_REFERRAL _Name=\"EQUIFAX INFORMATION SVCS\" _StreetAddress=\"P.O. BOX 740241\" _City=\"ATLANTA\" _State=\"GA\" _PostalCode=\"30374\">" + Environment.NewLine +
"<CONTACT_DETAIL>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Phone\" _Value=\"(800) 685-1111\"/>" + Environment.NewLine +
"</CONTACT_DETAIL>" + Environment.NewLine +
"</CREDIT_CONSUMER_REFERRAL>" + Environment.NewLine +
"<CREDIT_CONSUMER_REFERRAL _Name=\"EXPERIAN\" _StreetAddress=\"P.O. BOX 2002\" _City=\"ALLEN\" _State=\"TX\" _PostalCode=\"75013\">" + Environment.NewLine +
"<CONTACT_DETAIL>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Phone\" _Value=\"(888) 397-3742\"/>" + Environment.NewLine +
"</CONTACT_DETAIL>" + Environment.NewLine +
"</CREDIT_CONSUMER_REFERRAL>" + Environment.NewLine +
"<CREDIT_CONSUMER_REFERRAL _Name=\"TRANS UNION\" _StreetAddress=\"P.O. BOX 1000\" _City=\"CHESTER\" _State=\"PA\" _PostalCode=\"19016\">" + Environment.NewLine +
"<CONTACT_DETAIL>" + Environment.NewLine +
"<CONTACT_POINT _Type=\"Phone\" _Value=\"(800) 916-8800\"/>" + Environment.NewLine +
"</CONTACT_DETAIL>" + Environment.NewLine +
"</CREDIT_CONSUMER_REFERRAL>" + Environment.NewLine +
"<CREDIT_SUMMARY BorrowerID=\"Borrower1\" _Name=\"CREDCO_SUMMARY\">" + Environment.NewLine +
"<_DATA_SET _Name=\"_IdentityOnFileDate\" _Value=\"1984-09-15\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Term\" _Value=\"LIFE_OF_LOAN\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_AnyPublicRecordsInLast2Years\" _Value=\"N\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalPRCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_InquiryAgeDays\" _Value=\"Days_90\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalInquiriesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_ElimSameDayInqCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_InquiryAdjustedTotal\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_NewTradesIn6Months\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_OldestTradeDate\" _Value=\"2012-08-15\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_PaidAsAgreed\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_NowDelinquent\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_AllDelinquent\" _Value=\"2\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_HighestHighCredit\" _Value=\"9000\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_LowestHighCredit\" _Value=\"1000\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalCollections\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_HistoricalDelinquenciesCount\" _Value=\"11\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RevAndOtherAccountsCount\" _Value=\"2\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_BalanceOnRevolvingAndOther\" _Value=\"437\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_PaymentsOnRevolvingAndOther\" _Value=\"44\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RevAndOtherCreditAvailable\" _Value=\"2163\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RevAndOtherCreditPercent\" _Value=\"83\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalTradelinesCount\" _Value=\"4\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalTrlBalanceAmount\" _Value=\"3037\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalTrlMonthlyPaymentAmount\" _Value=\"167\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalCurrentTradelinesCount\" _Value=\"3\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalClosedTradelinesCount\" _Value=\"1\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalUnratedTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalLate30DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalLate60DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalLate90PlusDaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalLate30DaysHistoricalDelinquencies\" _Value=\"7\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalLate60DaysHistoricalDelinquencies\" _Value=\"4\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_TotalLate90PlusDaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_TradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstateTrl_BalanceAmount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstateTrl_MonthlyPaymentAmount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_CurrentTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_ClosedTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_UnratedTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_Late30DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_Late60DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_Late90PlusDaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_Late30DaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_Late60DaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RealEstate_Late90PlusDaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_LastDelinquencyDate\" _Value=\"2020-02\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_TradelinesCount\" _Value=\"2\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_InstallmentTrl_BalanceAmount\" _Value=\"2600\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_InstallmentTrl_MonthlyPaymentAmount\" _Value=\"123\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_CurrentTradelinesCount\" _Value=\"1\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_ClosedTradelinesCount\" _Value=\"1\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_UnratedTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_Late30DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_Late60DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_Late90PlusDaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_Late30DaysHistoricalDelinquencies\" _Value=\"5\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_Late60DaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Installment_Late90PlusDaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_LastDelinquencyDate\" _Value=\"2018-08\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RevolvingCreditAvailable\" _Value=\"2163\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RevolvingCreditTotal\" _Value=\"2600\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_PrcntRevolvingCreditAvailable\" _Value=\"83\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_TradelinesCount\" _Value=\"2\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RevolvingTrl_BalanceAmount\" _Value=\"437\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_RevolvingTrl_MonthlyPaymentAmount\" _Value=\"44\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_CurrentTradelinesCount\" _Value=\"2\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_ClosedTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_UnratedTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_Late30DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_Late60DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_Late90PlusDaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_Late30DaysHistoricalDelinquencies\" _Value=\"2\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_Late60DaysHistoricalDelinquencies\" _Value=\"4\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Revolving_Late90PlusDaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_OtherCreditAvailable\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_OtherCreditTotal\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_TradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_OtherTrl_BalanceAmount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_OtherTrl_MonthlyPaymentAmount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_CurrentTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_ClosedTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_UnratedTradelinesCount\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_Late30DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_Late60DaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_Late90PlusDaysCurrentDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_Late30DaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_Late60DaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_DATA_SET _Name=\"_Other_Late90PlusDaysHistoricalDelinquencies\" _Value=\"0\"/>" + Environment.NewLine +
"<_Text>Credco Summary</_Text>" + Environment.NewLine +
"</CREDIT_SUMMARY>" + Environment.NewLine +
"<REGULATORY_PRODUCT BorrowerID=\"Borrower1\" CreditRepositorySourceType=\"Other\" CreditRepositorySourceTypeOtherDescription=\"CoreLogic Credco\" _SourceType=\"OFAC\" _ProviderDescription=\"ProScan\" _ResultText=\"OFAC CHECK CLEAR\" _ResultStatusType=\"Clear\"/>" + Environment.NewLine +
"</CREDIT_RESPONSE>" + Environment.NewLine +
"</RESPONSE_DATA>" + Environment.NewLine +
"</RESPONSE>" + Environment.NewLine +
"</RESPONSE_GROUP>";

        #endregion
    }
}
