using System.Collections.Generic;

using Microsoft.AspNetCore.Authentication.OAuth;

namespace HRInPocket.Authentication.HH
{
    public class HHChallengeProperties : OAuthChallengeProperties
    {
        /// <summary>
        /// Ключ параметра для аргумента "access_type"
        /// </summary>
        public static readonly string AccessTypeKey = "access_type";
        /// <summary>
        /// Ключ параметра для аргумента "approval_prompt".
        /// </summary>
        public static readonly string ApprovalPromptKey = "approval_prompt";
        /// <summary>
        /// Ключ параметра для аргумента "include_granted_scopes".
        /// </summary>
        public static readonly string IncludeGrantedScopesKey = "include_granted_scopes";
        /// <summary>
        /// Ключ параметра для аргумента "login_hint".
        /// </summary>
        public static readonly string LoginHintKey = "login_hint";
        /// <summary>
        /// Ключ параметра для аргумента "prompt".
        /// </summary>
        public static readonly string PromptParameterKey = "prompt";

        public HHChallengeProperties() : base() { }

        public HHChallengeProperties(IDictionary<string, string> items) : base(items) { }

        public HHChallengeProperties(
            IDictionary<string, string> items,
            IDictionary<string, object> parameters) : base(items, parameters) { }

        public string AccessType
        {
            get => GetParameter<string>(AccessTypeKey);
            set => SetParameter(AccessTypeKey, value);
        }

        public string ApprovalPrompt
        {
            get => GetParameter<string>(ApprovalPromptKey);
            set => SetParameter(ApprovalPromptKey, value);
        }

        public bool? IncludeGrantedScopes
        {
            get => GetParameter<bool?>(IncludeGrantedScopesKey);
            set => SetParameter(IncludeGrantedScopesKey, value);
        }

        public string LoginHint
        {
            get => GetParameter<string>(LoginHintKey);
            set => SetParameter(LoginHintKey, value);
        }

        public string Prompt
        {
            get => GetParameter<string>(PromptParameterKey);
            set => SetParameter(PromptParameterKey, value);
        }
    }
}
