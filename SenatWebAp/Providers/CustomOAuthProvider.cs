//using system;
//using system.collections.generic;
//using system.linq;
//using system.security.claims;
//using system.threading.tasks;
//using system.web;
//using microsoft.aspnet.identity.owin;
//using microsoft.owin.security;
//using microsoft.owin.security.oauth;
//using senatwebap.infrastructure;

//namespace senatwebap.providers
//{
//    public class customoauthprovider : oauthauthorizationserverprovider
//    {
//        public override task validateclientauthentication(oauthvalidateclientauthenticationcontext context)
//        {
//            context.validated();
//            return task.fromresult<object>(null);
//        }

//        public override async task grantresourceownercredentials(oauthgrantresourceownercredentialscontext context)
//        {
//            var allowedorigin = "*";
//            context.owincontext.response.headers.add("access-control-allow-origin", new [] {allowedorigin});
//            var usermanager = context.owincontext.getusermanager<senatusermanager>();
//            senatuser user = await usermanager.findasync(context.username, context.password);
//            if (user == null)
//            {
//                context.seterror("invalid_grant","the user name or password is incorrect");
//                return;
//            }

//            claimsidentity oauthidentity = await user.generateuseridentityasync(usermanager, "jwt");
//            var ticket = new authenticationticket(oauthidentity, null);
//            context.validated(ticket);
//        }
//    }
//}