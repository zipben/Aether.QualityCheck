namespace Aether
{
    public static class Constants
    {
        //This should be around 185kb if each identifier is ~36 chars the actual max is 256kb. Leaving some room.
        public const int MAX_AWS_MESSAGE_SIZE = 5000;

        public const int OYA_API_APP_ID     = 207953;
        public const int OYA_UI_APP_ID      = 208259;
        public const int THEMIS_API_APP_ID  = 207844;
        public const int THEMIS_UI_APP_ID   = 206980;

        public const string CALL_INITIATOR_HEADER_KEY = "CALL_INITIATOR";
        public const string IS_TEST_HEADER_KEY = "IS_TEST";

        public static class Consent
        {
            public const string CONSENT_SETTINGS = "ConsentConfiguration";
        }
    }
}
