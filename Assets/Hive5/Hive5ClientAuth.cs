using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;
using Hive5.Util;


namespace Hive5
{
    /// <summary>
    /// Hive5 client.
    /// </summary>
#if UNITTEST
    public partial class Hive5Client : MockMonoSingleton<Hive5Client>
    {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif
        /********************************************************************************
			Auth API Group
		*********************************************************************************/

        /** 
        * @api {GET} Login �α���
        * @apiVersion 1.0.0-alpha
        * @apiName Login
        * @apiGroup Auth
        *
        * @apiParam {string} os OSType(android, ios)
        * @apiParam {string} userPlatform �÷��� Type
        * @apiParam {string} userId �÷��� UserId(īī�� ID, GOOGLE ID, FACEBOOK ID ....)
        * @apiParam {Callback} callback �ݹ� �Լ�
        *
        * @apiSuccess {String} resultCode Error Code ����
        * @apiSuccess {String} resultMessage ��û ���н� �޽���
        * @apiExample Example usage:
        * string userPlatform = "kakao";
        * string userId 		= "88197xxxx07226176";
        * 
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Login (OSType.Android, userPlatform, userId, response => {
        * 	Logger.Log ("response = "+ response.ResultData);
        * });
        */
        public void Login(string os, string userPlatform, string userId, Callback callback)
        {
            if (!InitState)
                return;

            // Hive5 API URL �ʱ�ȭ
            var url = InitializeUrl(APIPath.PlatformLogin);

            Logger.Log("login LoginState=" + LoginState);

            var requestBody = new
            {
                user = new { 
                    userPlatform = userPlatform,
                    userId = userId,
                },
                os = os,
            };'

            PostHttpAsync(url, requestBody, LoginResponseBody.Load, (response) =>
            {
                if (response.ResultCode == Hive5ResultCode.Success)
                {
                    var body = response.ResultData as LoginResponseBody;
                    if(body != null)
                    {
                        SetAccessToken(body.AccessToken, body.SessionKey);
                    }
                }
                this.loginState = true;
                callback(response);
            });
        }

        /** 
        * @api {post} Logout �α׾ƿ�
        * @apiVersion 1.0.0-alpha
        * @apiName Logout
        * @apiGroup Auth
        *
        * @apiParam {String} userId ���� ID
        * @apiParam {String} accessToken Login SDK ���� ���� ���� accessToken
        * @apiParam {Callback} callback �ݹ� �Լ�
        *
        * @apiSuccess {String} resultCode Error Code ����
        * @apiSuccess {String} resultMessage ��û ���н� �޽���
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Logout(userId, accessToken, callback);
        */
        public void Logout(string userId, Callback callback)
        {
            string accessToken = string.Empty;
            this.SessionKey = string.Empty;

            throw new NotImplementedException();
        }

        /** 
        * @api {POST} Unregister Ż��
        * @apiVersion 1.0.0-alpha
        * @apiName Unregister
        * @apiGroup Auth
        *
        * @apiParam {Callback} callback �ݹ� �Լ�
        *
        * @apiSuccess {String} resultCode Error Code ����
        * @apiSuccess {String} resultMessage ��û ���н� �޽���
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Unregister(callback);
        */
        public void Unregister(Callback callback)
        {
            var url = InitializeUrl(APIPath.Unregister);

            // WWW ȣ��
            PostHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {POST} SubmitAgreements ��� ����
        * @apiVersion 1.0.0-alpha
        * @apiName SubmitAgreements
        * @apiGroup Auth
        *
        * @apiParam {string} generalVersion ��� ����
        * @apiParam {string} partnershipVersion ��Ʈ�ʽ� ����
        * @apiParam {Callback} callback �ݹ� �Լ�
        *
        * @apiSuccess {String} resultCode Error Code ����
        * @apiSuccess {String} resultMessage ��û ���н� �޽���
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SubmitAgreements(generalVersion, partnershipVersion, callback);
        */
        public void SubmitAgreements(string generalVersion, string partnershipVersion, Callback callback)
        {
            var url = InitializeUrl(APIPath.Agreement);

            var requestBody = new
            {
                general_agreement = generalVersion,
                partnership_agreement = partnershipVersion
            };

            // WWW ȣ��
            PostHttpAsync(url, requestBody, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {GET} GetAgreements ��� ���� ��������
        * @apiVersion 1.0.0-alpha
        * @apiName GetAgreements
        * @apiGroup Auth
        *
        * @apiParam {Callback} callback �ݹ� �Լ�
        *
        * @apiSuccess {String} resultCode Error Code ����
        * @apiSuccess {String} resultMessage ��û ���н� �޽���
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.GetAgreements(callback);
        */
        public void GetAgreements(Callback callback)
        {
            var url = InitializeUrl(APIPath.Agreement);

            // Hive5 API �Ķ���� ����
            TupleList<string, string> parameters = new TupleList<string, string>();

            // WWW ȣ��           
            GetHttpAsync(url, parameters.data, GetAgreementsResponseBody.Load, callback);
        }

        /** 
        * @api {POST} SwitchPlatform �α��� �÷��� �ٲٱ�
        * @apiVersion 1.0.0-alpha
        * @apiName SwitchPlatform
        * @apiGroup Auth
        *
        * @apiParam {string} platformType �÷���Ÿ�� PlatformType.Kakao ��
        * @apiParam {string} platformUserId �÷��� ����� ���̵�
        * @apiParam {Callback} callback �ݹ� �Լ�
        *
        * @apiSuccess {String} resultCode Error Code ����
        * @apiSuccess {String} resultMessage ��û ���н� �޽���
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SwitchPlatform(PlatformType.Kakao, platformUserId, callback);
        */
        public void SwitchPlatform(string platformType, string platformUserId, Callback callback)
        {
            
            Logger.Log("SwitchPlatform called");

            var url = InitializeUrl(APIPath.SwitchPlatform);

            var requestBody = new
            {
                platform = platformType,
                platform_user_id = platformUserId
            };

            Logger.Log(url);

            // WWW ȣ��
            PostHttpAsync(url, requestBody, SwitchPlatformResponseBody.Load, callback);
        }

		public void CreatePlatformAccount(string name, string password, Callback callback, string displayName = "", string email = "")
		{
			Logger.Log("CreatePlatformAccount called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 


			var url = InitializeUrl(APIPath.CreatePlatformAccount);
			
			var requestBody = new
			{
				name = name,
				password = password,
				display_name = displayName,
				email = email,
			};
			
			Logger.Log(url);
			
			// WWW ȣ��
			PostHttpAsync(url, requestBody, CreatePlatformAccountResponseBody.Load, callback);
		}

		public void CheckPlatformNameAvailability(string name, Callback callback)
		{
			Logger.Log("CheckPlatformNameAvailability called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = string.Format(InitializeUrl(APIPath.CheckPlatformNameAvailability), name);
		
			Logger.Log(url);
		
			// WWW ȣ��           
			GetHttpAsync(url, null, CheckPlatformNameAvailabilityResponseBody.Load, callback);
		}

		public void CheckPlatformEmailAvailablity(string email, Callback callback)
		{
			Logger.Log("CheckPlatformEmailAvailablity called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = string.Format(InitializeUrl(APIPath.CheckPlatformEmailAvailability), email);
			
			Logger.Log(url);
			
			// WWW ȣ��           
			GetHttpAsync(url, null, CheckPlatformEmailAvailabilityResponseBody.Load, callback);
		}

		public void AuthenticatePlatformAccount(string name, string password, Callback callback)
		{
			Logger.Log("AuthenticatePlatformAccount called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = InitializeUrl(APIPath.AuthenticatePlatformAccount);
			
			var requestBody = new
			{
				name = name,
				password = password,
			};
			
			Logger.Log(url);
			
			// WWW ȣ��
			PostHttpAsync(url, requestBody, AuthenticatePlatformAccountResponseBody.Load, callback);
		}
    }
}
