//using system;
//using system.collections.generic;
//using system.identitymodel.tokens.jwt;
//using system.linq;
//using system.web;
//using microsoft.owin.security;
//using microsoft.owin.security.datahandler.encoder;
//using nlog.internal;
//using thinktecture.identitymodel.tokens;

//namespace senatwebap.providers
//{
//    public class customjwtformat : isecuredataformat<authenticationticket>
//    {
//        private readonly string _issuer = string.empty;

//        public customjwtformat(string issuer)
//        {
//            _issuer = issuer;
//        }

//        public string protect(authenticationticket data)
//        {
//            if (data == null)
//            {
//                throw new argumentnullexception("data");
//            }

//            string audienceid = configurationmanager.appsettings["as:audienceid"];
//            string symmetrickeyasbase64 = configurationmanager.appsettings["as.audiencesecret"];
//            var keybytearray = textencodings.base64url.decode(symmetrickeyasbase64);
//            var signingkey = new hmacsigningcredentials(keybytearray);
//            var issued = data.properties.issuedutc;
//            var expires = data.properties.expiresutc;
//            var token = new jwtsecuritytoken(_issuer, audienceid, data.identity.claims, issued.value.utcdatetime,
//                expires.value.utcdatetime, signingkey);
//            var handler = new jwtsecuritytokenhandler();
//            var jwt = handler.writetoken(token);
//            return jwt;

//        }

//        public authenticationticket unprotect(string protectedtext)
//        {
//            throw new notimplementedexception();
//        }
//    }
//}