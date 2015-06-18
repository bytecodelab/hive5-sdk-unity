using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hive5;
using UnityEngine;
using System.Threading;
using Hive5.Model;
using System.Net;
using System.Collections.Generic;
using LitJson;
using Hive5.Util;

namespace maui_sdk.test
{
    [TestClass]
    public class Hive5UnitTest
    {

        #region 설정값들

#if DEBUG
        public static Hive5APIZone TestZone = Hive5APIZone.Alpha;
#else
        public static Hive5APIZone TestZone = Hive5APIZone.Production;
#endif
        
        public static TestValueSet TestValues { get; set; }

        #endregion 설정값들

        public Hive5Client ApiClient { get; set; }

        [TestInitialize]
        public void InitializeTests()
        {
            //TestValues = TestValueSet.AinaRod;
            TestValues = TestValueSet.Default;
            Hive5.Hive5Config.CustomAccountPlatformName = "test";
            Hive5.Hive5Config.XPlatformKey = "4b9ea368-2809-4e57-91a1-d9ce7ac39534"; // alpha: UnitTest Game
            if (this.ApiClient == null)
            {
                TestInit();
            }
        }

        private void Login()
        {
            string userPlatform = PlatformType.Google;
            string userId = TestValues.ValidPlatformUserId;

            try
            {
                var completion = new ManualResetEvent(false);

                this.ApiClient.Login(OSType.Android, userPlatform, userId, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is LoginResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    LoginResponseBody body = response.ResultData as LoginResponseBody;
                    Assert.IsTrue(string.IsNullOrEmpty(body.AccessToken) == false); // 잘못된 아이디로 로그인했으니
                    Assert.IsTrue(body.Agreements != null);
                    Assert.IsTrue(body.CompletedMissions != null);
                    Assert.IsTrue(body.Configs != null);
                    Assert.IsTrue(body.MailboxNewItemCount >= 0);
                    Assert.IsTrue(body.Promotions != null);
                    Assert.IsTrue(body.UserId > 0); // 잘못된 아이디로 로그인했으니

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        #region INIT

        [TestMethod, TestCategory("Init")]
        public void TestInit()
        {
            var client = Hive5Client.Instance;
            //client.SetDebug();
            string appKey = TestValues.AppKey;
            string uuid = TestValues.Uuid;

            try
            {
                client.Init(appKey, uuid, Hive5UnitTest.TestZone);
                client.SetDebug();
                this.ApiClient = client;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        #endregion INIT

        #region AUTH

        [TestMethod, TestCategory("Auth")]
        public void Test로그인Login()
        {
            try
            {
                Login();
            }
            catch
            {
                Assert.Fail();
            }
        }

        //[TestMethod, TestCategory("Auth")]
        //public void TestLoginFail()
        //{
        //    string userId 		= Hive5UnitTest.InvalidUserId;	// 잘못된 사용자
        //    string sdkVersion 	= Hive5UnitTest.GoogleSdkVersion;		
        //    string[] objectKeys 	= new string[] {""};		// 로그인 후 가져와야할 사용자 object의 key 목록
        //    string[] configKeys 	= new string[] {"time_event1"};	// 로그인 후 가져와야할 사용자 configuration의 key

        //    try
        //    {
        //        var completion = new ManualResetEvent(false);

        //        this.ApiClient.Login(OSType.Android, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, (response) => 
        //        {
        //            // 1. 기본 반환값 검증
        //            Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
        //            Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
        //            Assert.IsTrue(response.ResultData is LoginResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

        //            // 2. 프로퍼티 검증
        //            LoginResponseBody body = response.ResultData as LoginResponseBody;
        //            Assert.IsTrue(string.IsNullOrEmpty(body.AccessToken) == true); // 잘못된 아이디로 로그인했으니
        //            Assert.IsTrue(body.Agreements != null); 
        //            Assert.IsTrue(body.CompletedMissions != null); 
        //            Assert.IsTrue(body.Configs != null); 
        //            Assert.IsTrue(body.MailboxNewItemCount >= 0); 
        //            Assert.IsTrue(body.Promotions != null); 
        //            Assert.IsTrue(body.UserId == 0); // 잘못된 아이디로 로그인했으니

        //            completion.Set();
        //        });

        //        completion.WaitOne();
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
        //    }
        //}

        [TestMethod, TestCategory("Auth")]
        public void TestSessionKey()
        {
            try
            {
                 Login();
                string oldSessionKey = this.ApiClient.SessionKey;

                Login();
                string newSessionKey = this.ApiClient.SessionKey;

                Assert.AreNotEqual(oldSessionKey, newSessionKey);

                this.ApiClient.SetAccessToken(this.ApiClient.AccessToken, oldSessionKey);
                var completion = new ManualResetEvent(false);

                //var parameters = new TupleList<string, string>();
                //parameters.Add("echo", "gilbok");
                //parameters.Add("echo", "gilbok");
                //this.ApiClient.CallProcedure("echo", parameters, (response) =>
                //{
                this.ApiClient.CheckNicknameAvailability("불량사과", (response) =>
                {
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.TheSessionKeyIsInvalid);
                    completion.Set();
                });

                completion.WaitOne();
                
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod, TestCategory("Auth")]
        public void Test닉네임확인CheckNicknameAvailability()
        {
            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.CheckNicknameAvailability("불량사과", (response) =>
                {
                    // 1. 기본 반환값 검증
                    if (response.ResultCode != Hive5ResultCode.Success)
                    { 
                        Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage) == false); 
                    }

                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CommonResponseBody); // 제대로 된 반환데이터가 오는지 타입체크


                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }


        [TestMethod, TestCategory("Auth")]
        public void Test약관동의내역보기GetAgreements()
        {
            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.GetAgreements((response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetAgreementsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetAgreementsResponseBody body = response.ResultData as GetAgreementsResponseBody;
                    Assert.IsTrue(body.Agreements != null);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }

        }

        [TestMethod, TestCategory("Auth")]
        public void Test약관동의SubmitAgreements()
        {
            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.SubmitAgreements("1.0", "1.0", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CommonResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    // 할 것이 없음

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Auth")]
        public void Test탈퇴Unregister()
        {
            Assert.Inconclusive("테스트 구현 안함 - 가입API가 없는데, 소중한 유저 탈퇴를 호출하기 겁남");
        }

        [TestMethod, TestCategory("Auth")]
        public void Test플랫폼계정생성CreatePlatformAccount()
        {
            var completion = new ManualResetEvent(false);

            string name = DateTime.Now.Ticks.ToString().ToString();
            string email = string.Format("{0}@example.com", name);
            this.ApiClient.CreatePlatformAccount(name, "12345678", (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreatePlatformAccountResponseBody body = response.ResultData as CreatePlatformAccountResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.Id) == false);

                completion.Set();
            }, "tester", email);

            completion.WaitOne();
        }

        [TestMethod, TestCategory("Auth")]
        public void Test플랫폼계정이름중복확인CheckPlatformNameAvailability()
        {
            string name = DateTime.Now.Ticks.ToString().ToString();
            string email = string.Format("{0}@example.com", name);

            var completion1 = new ManualResetEvent(false);
            this.ApiClient.CheckPlatformNameAvailability(name, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                    
                    // 2. 프로퍼티 검증

                    completion1.Set();
                });
            completion1.WaitOne();

            var completion2 = new ManualResetEvent(false);
            this.ApiClient.CreatePlatformAccount(name, "12345678", (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreatePlatformAccountResponseBody body = response.ResultData as CreatePlatformAccountResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.Id) == false);

                completion2.Set();
            }, "tester", email);


            completion2.WaitOne();

            var completion3 = new ManualResetEvent(false);
            this.ApiClient.CheckPlatformNameAvailability(name, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.AlreadyExistingPlatformUserName); // 일단 반환성공
                Assert.IsFalse(string.IsNullOrEmpty(response.ResultMessage));
               
                // 2. 프로퍼티 검증

                completion3.Set();
            });
            completion3.WaitOne();
        }

        [TestMethod, TestCategory("Auth")]
        public void Test플랫폼계정이메일중복확인CheckPlatformEmailAvailability()
        {
            string name = DateTime.Now.Ticks.ToString().ToString();
            string email = string.Format("{0}@example.com", name);

            var completion1 = new ManualResetEvent(false);
            this.ApiClient.CheckPlatformEmailAvailablity(email, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));

                // 2. 프로퍼티 검증

                completion1.Set();
            });
            completion1.WaitOne();

            var completion2 = new ManualResetEvent(false);
            this.ApiClient.CreatePlatformAccount(name, "12345678", (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreatePlatformAccountResponseBody body = response.ResultData as CreatePlatformAccountResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.Id) == false);

                completion2.Set();
            }, "tester", email);


            completion2.WaitOne();

            var completion3 = new ManualResetEvent(false);
            this.ApiClient.CheckPlatformEmailAvailablity(email, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.AlreadyExistingPlatformUserEmail); // 일단 반환성공
                Assert.IsFalse(string.IsNullOrEmpty(response.ResultMessage));

                // 2. 프로퍼티 검증

                completion3.Set();
            });
            completion3.WaitOne();
        }

        [TestMethod, TestCategory("Auth")]
        public void Test플랫폼계정인증AuthenticatePlatformAccount()
        {
            string name = DateTime.Now.Ticks.ToString().ToString();
            string email = string.Format("{0}@example.com", name);
            string password = "12345678";
            string wrongPassword = "00000000";

            string platformUserId = "";

            var completion1 = new ManualResetEvent(false);
            this.ApiClient.CreatePlatformAccount(name, password, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreatePlatformAccountResponseBody body = response.ResultData as CreatePlatformAccountResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.Id) == false);
                platformUserId = body.Id;

                completion1.Set();
            }, "tester", email);

            completion1.WaitOne();

            var completion2 = new ManualResetEvent(false);
            this.ApiClient.AuthenticatePlatformAccount(name, password, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is AuthenticatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                AuthenticatePlatformAccountResponseBody body = response.ResultData as AuthenticatePlatformAccountResponseBody;
                Assert.IsTrue(body.Id == platformUserId);

                completion2.Set();
            });

            completion2.WaitOne();

            var completion3 = new ManualResetEvent(false);
            this.ApiClient.AuthenticatePlatformAccount(name, wrongPassword, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.InvalidNameOrPassword); // 일단 반환성공
                Assert.IsFalse(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData == null); // 반환데이터는 null이면 안 됨
              
                // 2. 프로퍼티 검증

                completion3.Set();
            });

            completion3.WaitOne();
        }

        #endregion AUTH

        #region LEADERBOARD

        [TestMethod, TestCategory("Leader Board")]
        public void Test내랭킹확인GetMyScore()
        {
            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.GetMyScore("3", 0, 100, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetMyScoreResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetMyScoreResponseBody body = response.ResultData as GetMyScoreResponseBody;
                    Assert.IsTrue(body.Rank >= -1);
                    Assert.IsTrue(body.ScoresCount >= 0);
                    Assert.IsTrue(body.Value >= -1);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Leader Board")]
        public void Test랭킹가져오기GetScores()
        {
            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.GetScores(TestValues.LeaderBoardKey, TestValues.ObjectClasses, 0, 100, null, null, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetScoresResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetScoresResponseBody body = response.ResultData as GetScoresResponseBody;
                    Assert.IsTrue(body.Scores != null);
                    Assert.IsTrue(body.Scores.Count >= 0);

                    if (body.Scores.Count > 0)
                    {
                        var score = body.Scores[0];
                        Assert.IsTrue(string.IsNullOrEmpty(score.platform) == false);
                        Assert.IsTrue(string.IsNullOrEmpty(score.platformUserId) == false);
                        Assert.IsTrue(score.rank >= -1);
                        Assert.IsTrue(score.value != null);
                        Assert.IsTrue(score.objects != null);
                    }

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Leader Board")]
        public void Test점수기록SubmitScore()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.SubmitScore("3", 100, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is SubmitScoreResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    SubmitScoreResponseBody body = response.ResultData as SubmitScoreResponseBody;
                    // 검증할 것 없음

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Leader Board")]
        public void Test친구랭킹가져오기GetSocialScores()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                List<string> objectClasses = new List<string>()
                {
                    "sword",
                };
                this.ApiClient.GetSocialScores("3", objectClasses, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetSocialScoresResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetSocialScoresResponseBody body = response.ResultData as GetSocialScoresResponseBody;
                    Assert.IsTrue(body.Scores != null);
                    Assert.IsTrue(body.Scores.Count > 0);
                    if (body.Scores.Count > 0)
                    {
                        var score = body.Scores[0];
                        Assert.IsTrue(string.IsNullOrEmpty(score.platform) == false);
                        Assert.IsTrue(string.IsNullOrEmpty(score.platformUserId) == false);
                        Assert.IsTrue(score.rank >= -1);
                        Assert.IsTrue(score.value != null);
                        Assert.IsTrue(score.objects != null);
                    }

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        #endregion LEADERBOARD

        #region MAIL

        [TestMethod, TestCategory("Mail")]
        public void Test메일태그제거DetachMailTags()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                string[] tags = new string[] { "reward" };

                this.ApiClient.DetachMailTags(1, tags, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is DetachMailTagsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    DetachMailTagsResponseBody body = response.ResultData as DetachMailTagsResponseBody;
                    Assert.IsTrue(body.Tags != null);
                    Assert.IsTrue(body.Tags.Count >= 0);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Mail")]
        public void Test메일태그추가AttachMailTags()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                string sampleTag = "reward";
                string[] tags = new string[] { sampleTag };

                this.ApiClient.AttachMailTags(1, tags, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is AttachMailTagsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    AttachMailTagsResponseBody body = response.ResultData as AttachMailTagsResponseBody;
                    Assert.IsTrue(body.Tags != null);
                    Assert.IsTrue(body.Tags.Count >= 1);
                    Assert.IsTrue(body.Tags.Contains(sampleTag) == true);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }


        [TestMethod, TestCategory("Mail")]
        public void Test메일개수확인GetMailCount()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                string sampleTag = "reward";

                this.ApiClient.GetMailCount(OrderType.DESC, 0, sampleTag, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetMailCountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetMailCountResponseBody body = response.ResultData as GetMailCountResponseBody;
                    Assert.IsTrue(body.Count >= 0);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Mail")]
        public void Test메일리스트가져오기GetMails()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                string sampleTag = "reward";

                this.ApiClient.GetMails(10, OrderType.DESC, 0, sampleTag, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetMailsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetMailsResponseBody body = response.ResultData as GetMailsResponseBody;
                    Assert.IsTrue(body.Mails != null);
                    Assert.IsTrue(body.Mails.Count >= 0);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Mail")]
        public void Test메일삭제DeleteMail()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                // 지울 메일을 미리 생성
                long createMailId = CreateMail("메일삭제 테스트메일입니다.");

                // 그 다음 삭제
                this.ApiClient.DeleteMail(createMailId, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CommonResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CommonResponseBody body = response.ResultData as CommonResponseBody;
                    // 검증할 프로퍼티 없음

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Mail")]
        public void Test메일생성CreateMail()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                CreateMail("메일생성 테스트메일입니다.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        public long CreateMail(string content)
        {
            var completion = new ManualResetEvent(false);

            string sampleTag = "reward";
            string[] tags = new string[] { sampleTag };
            long createMailId = 0;

            this.ApiClient.CreateMail(content, TestValues.ValidPlatformUserId, tags, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreateMailResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreateMailResponseBody body = response.ResultData as CreateMailResponseBody;
                Assert.IsTrue(body.Id >= 0);

                createMailId = body.Id;
                completion.Set();
            });

            completion.WaitOne();

            return createMailId;
        }

        [TestMethod, TestCategory("Mail")]
        public void Test메일수정UpdateMail()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                // 지울 메일을 미리 생성
                long createMailId = CreateMail("메일수정 테스트메일입니다.");

                // 그 다음 삭제
                this.ApiClient.UpdateMail(createMailId, "수정된 메일수정 테스트메일입니다.", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CommonResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CommonResponseBody body = response.ResultData as CommonResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Mail")]
        public void Test메일범위삭제DeleteAllMail()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                // 지울 메일을 미리 생성
                long createMailId = CreateMail("메일범위삭제 테스트메일입니다.");

                // 그 다음 삭제
                this.ApiClient.DeleteAllMail(createMailId - 1, createMailId, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CommonResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CommonResponseBody body = response.ResultData as CommonResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }



        #endregion MAIL

        #region MISSION

        [TestMethod, TestCategory("Mission")]
        public void Test미션완료CompleteMission()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.CompleteMission("missionImpossible", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success ||
                                  response.ResultCode == Hive5ResultCode.DataDoesNotExist); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CompleteMissionResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CompleteMissionResponseBody body = response.ResultData as CompleteMissionResponseBody;
                    Assert.IsTrue(body.MailId >= -1);
                    Assert.IsTrue(body.RewardId >= -1);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Mission")]
        public void Test완료된미션가져오기GetCompletedMissions()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.GetCompletedMissions((response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success ||
                                  response.ResultCode == Hive5ResultCode.DataDoesNotExist); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetCompletedMissionsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetCompletedMissionsResponseBody body = response.ResultData as GetCompletedMissionsResponseBody;
                    if (body.Missions != null)
                    {
                        Assert.IsTrue(body.Missions.Count >= 0);
                    }

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        #endregion MISSION

        #region OBJECT

        [TestMethod, TestCategory("Object")]
        public void Test오브젝트리스트GetObjects()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                string sampleClassType = "sword";

                List<HObject> objects = new List<HObject>()
                {
                    new HObject() { @class = sampleClassType },
                };

                this.ApiClient.GetObjects(objects, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success ||
                                  response.ResultCode == Hive5ResultCode.DataDoesNotExist); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetObjectsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetObjectsResponseBody body = response.ResultData as GetObjectsResponseBody;
                    if (body.Objects != null)
                    {
                        Assert.IsTrue(body.Objects.Count >= 0);
                    }

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Object")]
        public void Test오브젝트생성CreateObjects()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();


                string sampleClassType = "sword";
                string sampleClassType2 = "shield";

                List<HObject> objects = new List<HObject>()
                {
                    new HObject() 
                    { 
                        @class = sampleClassType, 
                    },
                    new HObject() 
                    { 
                        @class = sampleClassType2, 
                    },
                };

                CreateObjects(objects);

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        private CreateObjectsResponseBody CreateObjects(List<HObject> objects)
        {
            var completion = new ManualResetEvent(false);

            CreateObjectsResponseBody body = null;

            this.ApiClient.CreateObjects(objects, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success ||
                              response.ResultCode == Hive5ResultCode.DataDoesNotExist); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreateObjectsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                body = response.ResultData as CreateObjectsResponseBody;
                if (body.Objects != null)
                {
                    Assert.IsTrue(body.Objects.Count >= 0);
                }

                completion.Set();
            });

            completion.WaitOne();
            return body;
        }

        [TestMethod, TestCategory("Object")]
        public void Test오브젝트저장SetObjects()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                string sampleClassType = "sword";
                string sampleClassType2 = "shield";

                List<HObject> objects = new List<HObject>()
                {
                    new HObject() 
                    { 
                        @class = sampleClassType, 
                    },
                    new HObject() 
                    { 
                        @class = sampleClassType2, 
                    },
                };

                SetObjects(objects);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        public CommonResponseBody SetObjects(List<HObject> objects)
        {
            CommonResponseBody body = null;
            var completion = new ManualResetEvent(false);
            this.ApiClient.SetObjects(objects, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CommonResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                body = response.ResultData as CommonResponseBody;

                completion.Set();
            });

            completion.WaitOne();
            return body;
        }


        [TestMethod, TestCategory("Object")]
        public void Test오브젝트제거DestroyObjects()
        {
            try
            {
                Login();

                // 오브젝트 생성하기
                string sampleClassType = "sword";
                string sampleClassType2 = "shield";

                List<HObject> objects = new List<HObject>()
                {
                    new HObject() 
                    { 
                        @class = sampleClassType, 
                    },
                    new HObject() 
                    { 
                        @class = sampleClassType2, 
                    },
                };

                CreateObjects(objects);

                // 오브젝트 지우기
                var completion = new ManualResetEvent(false);

                List<HObject> destroyObjects = new List<HObject>()
                {
                    new HObject() 
                    { 
                        @class = sampleClassType, 
                    },
                    new HObject() 
                    { 
                        @class = sampleClassType2, 
                    },
                };

                this.ApiClient.DestroyObjects(destroyObjects, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CommonResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CommonResponseBody body = response.ResultData as CommonResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        #endregion OBJECT

        #region PROCEDURE

        [TestMethod, TestCategory("Procedure")]
        public void Test프로시저호출CallProcedure()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);
                var parameters = new TupleList<string, string>();
                parameters.Add("echo", "gilbok");
                parameters.Add("echo", "gilbok");

                this.ApiClient.CallProcedure("echo", parameters, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CallProcedureResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CallProcedureResponseBody body = response.ResultData as CallProcedureResponseBody;
                    Assert.IsTrue(string.IsNullOrEmpty(body.CallReturn) == false);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

         [TestMethod, TestCategory("Procedure")]
        public void Test플레이어정보없이프로시저호출CallProcedureWithoutAuth()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                var someValue = "dummy";
                var parameters = new { echo =  someValue};

                this.ApiClient.CallProcedureWithoutAuth("echo", parameters, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CallProcedureResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CallProcedureResponseBody body = response.ResultData as CallProcedureResponseBody;
                    Assert.IsTrue(string.IsNullOrEmpty(body.CallReturn) == false);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Procedure")]
        public void TestCallProcedureInJson()
        {
            Login();

             var completion = new ManualResetEvent(false);

            List<Item> items = new List<Item>()
            {
                new Item() { Id = 1, Grade = 1, Rate = 1 },
                new Item() { Id = 2, Grade = 2, Rate = 2 },
                new Item() { Id = 3, Grade = 3, Rate = 3 },
            };
            var parameterObject = new { 
                gamble_items = items
            };

            // alpha 서버에서는 없는 프로시저 오류발생할 수 있음.
            this.ApiClient.CallProcedure("z_unittest_callee", parameterObject, (response) =>
            {
                if (response.ResultCode != Hive5ResultCode.Success)
                {
                    // handle error here
                    return;
                }

                var body = response.ResultData as CallProcedureResponseBody;

                Assert.AreEqual("{\"params\":{\"gamble_items\":[{\"Id\":1,\"Grade\":1,\"Rate\":1},{\"Id\":2,\"Grade\":2,\"Rate\":2},{\"Id\":3,\"Grade\":3,\"Rate\":3}]}}",
                    body.CallReturn);

                var jsonData = LitJson.JsonMapper.ToObject(body.CallReturn);

                Assert.IsTrue(jsonData.IsObject == true);
                Assert.IsTrue(jsonData["params"].IsObject == true);
                Assert.IsTrue(jsonData["params"]["gamble_items"].IsArray == true);
                Assert.IsTrue(jsonData["params"]["gamble_items"].Count == 3);
                Assert.IsTrue((int)jsonData["params"]["gamble_items"][0]["Id"] == 1);

                // code here
                completion.Set();
            });

            completion.WaitOne();
        }


        #endregion PROCEDURE

        #region PURCHASE

        [TestMethod, TestCategory("Purchase")]
        public void Test구글결제시작CreateGooglePurchase()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login();

                var body = CreateGooglePurchase();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        public CreateGooglePurchaseResponseBody CreateGooglePurchase()
        {
            var completion = new ManualResetEvent(false);

            string productCode = "google_product_100";
            string receiverPlatformUserId = null;
            string mailForReceiver = null;

            CreateGooglePurchaseResponseBody body = null;

            this.ApiClient.CreateGooglePurchase(productCode, receiverPlatformUserId, mailForReceiver, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreateGooglePurchaseResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                body = response.ResultData as CreateGooglePurchaseResponseBody;
                Assert.IsTrue(body.Id >= 0);

                completion.Set();
            });

            completion.WaitOne();
            return body;
        }


        [TestMethod, TestCategory("Purchase")]
        public void Test구글결제완료CompleteGooglePurchase()
        {
            Assert.Inconclusive("signature 값을 제대로 채울 수가 없음");
            return;

            try
            {
                Login();

                var googlePurchaseBody = CreateGooglePurchase();

                var completion = new ManualResetEvent(false);

                long id = googlePurchaseBody.Id;
                long listPrice = 1100;
                long purchasePrice = 1100;
                string currency = null;
                string purchaseData = "{\"purchaseToken\":\"\",\"developerPayload\":\"\",\"packageName\":\"\",\"purchaseState\":,\"orderId\":\"\",\"purchaseTime\":,\"productId\":\"\"}";
                string signature = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx==";

                this.ApiClient.CompleteGooglePurchase(id, listPrice, purchasePrice, currency, purchaseData, signature, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CompleteGooglePurchaseResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CompleteGooglePurchaseResponseBody body = response.ResultData as CompleteGooglePurchaseResponseBody;
                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Purchase")]
        public void Test네이버결제시작CreateNaverPurchase()
        {
            Assert.Inconclusive("payement_sequence 값을 제대로 채울 수가 없음");
            return;

            try
            {
                Login();

                var body = CreateNaverPurchase();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        public CreateNaverPurchaseResponseBody CreateNaverPurchase()
        {
            var completion = new ManualResetEvent(false);

            string productCode = "naver_product_100";
            string payement_sequence = null;

            CreateNaverPurchaseResponseBody body = null;

            this.ApiClient.CreateNaverPurchase(productCode, payement_sequence, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreateNaverPurchaseResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                body = response.ResultData as CreateNaverPurchaseResponseBody;
                Assert.IsTrue(body.Id >= 0);

                completion.Set();
            });

            completion.WaitOne();
            return body;
        }


        [TestMethod, TestCategory("Purchase")]
        public void Test네이버결제완료CompleteNaverPurchase()
        {
            Assert.Inconclusive("payement_sequence 값을 제대로 채울 수가 없음");
            return;

            try
            {
                Login();

                var purchaseBody = CreateNaverPurchase();

                var completion = new ManualResetEvent(false);

                long id = purchaseBody.Id;
                long listPrice = 1100;
                long purchasePrice = 1100;
                string currency = null;

                this.ApiClient.CompleteNaverPurchase(id, listPrice, purchasePrice, currency, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CompleteNaverPurchaseResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CompleteNaverPurchaseResponseBody body = response.ResultData as CompleteNaverPurchaseResponseBody;
                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Purchase")]
        public void Test애플결제시작CreateApplePurchase()
        {
            Assert.Inconclusive("호출성공조건을 아직 모름");
            return;

            try
            {
                Login();

                var body = CreateApplePurchase();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        public CreateApplePurchaseResponseBody CreateApplePurchase()
        {
            var completion = new ManualResetEvent(false);

            string productCode = "apple_product_100";
            string receiverPlatformUserId = null;
            string mailForReceiver = null;

            CreateApplePurchaseResponseBody body = null;

            this.ApiClient.CreateGooglePurchase(productCode, receiverPlatformUserId, mailForReceiver, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreateApplePurchaseResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                body = response.ResultData as CreateApplePurchaseResponseBody;
                Assert.IsTrue(body.Id >= 0);

                completion.Set();
            });

            completion.WaitOne();
            return body;
        }


        [TestMethod, TestCategory("Purchase")]
        public void Test애플결제완료CompleteApplePurchase()
        {
            Assert.Inconclusive("receipt 값을 제대로 채울 수 없음");
            return;

            try
            {
                Login();

                var purchaseBody = CreateApplePurchase();

                var completion = new ManualResetEvent(false);

                long id = purchaseBody.Id;
                long listPrice = 1100;
                long purchasePrice = 1100;
                string currency = null;
                string receipt = "{\"purchaseToken\":\"\",\"developerPayload\":\"\",\"packageName\":\"\",\"purchaseState\":,\"orderId\":\"\",\"purchaseTime\":,\"productId\":\"\"}";
                bool isSandbox = false;

                this.ApiClient.CompleteApplePurchase(id, listPrice, purchasePrice, currency, receipt, isSandbox, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CreateApplePurchaseResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CreateApplePurchaseResponseBody body = response.ResultData as CreateApplePurchaseResponseBody;
                    Assert.IsTrue(body.Id >= 0);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        #endregion PURCHASE

        #region COUPON

        [TestMethod, TestCategory("Push")]
        public void Test쿠폰적용ApplyCoupon()
        {
            try
            {
                Login();

                var completion1 = new ManualResetEvent(false);

                var coupon = "555NL985DDLBBYGN"; // 테스트 전에 알파서버서 발급하기
                
                this.ApiClient.ApplyCoupon(coupon, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CallProcedureResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CallProcedureResponseBody body = response.ResultData as CallProcedureResponseBody;

                    completion1.Set();
                });

                completion1.WaitOne();

                var completion2 = new ManualResetEvent(false);

                this.ApiClient.ApplyCoupon(coupon, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.ThePlayerHasAlreadyConsumedTheCoupon); // 일단 반환성공
                    Assert.IsTrue(response.ResultData == null); // 반환데이터는 null이면 안 됨
               
                    completion2.Set();
                });

                completion2.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        #endregion COUPON

        #region PUSH

        [TestMethod, TestCategory("Push")]
        public void Test푸쉬토큰등록및업데이트UpdatePushToken()
        {
            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.UpdatePushToken(PlatformType.Kakao, "test_token", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is UpdatePushTokenResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    UpdatePushTokenResponseBody body = response.ResultData as UpdatePushTokenResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Push")]
        public void Test푸쉬수신여부설정TogglePushAccept()
        {
            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.TogglePushAccept(true, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is UpdatePushTokenResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    UpdateTogglePushAcceptResponseBody body = response.ResultData as UpdateTogglePushAcceptResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        #endregion PUSH

        #region REWARD

        [TestMethod, TestCategory("Reward")]
        public void Test보상무효화InvalidateReward()
        {
            Assert.Inconclusive("Test 가능한 rewardId 값 필요.");
            return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                long rewardId = 1;

                this.ApiClient.InvalidateReward(rewardId, false, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is InvalidateRewardResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    InvalidateRewardResponseBody body = response.ResultData as InvalidateRewardResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Reward")]
        public void Test보상정보GetRewardInfo()
        {
            Assert.Inconclusive("Test 가능한 rewardId 값 필요.");
            return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                long rewardId = 1;

                this.ApiClient.GetRewardInfo(rewardId, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetRewardInfoResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetRewardInfoResponseBody body = response.ResultData as GetRewardInfoResponseBody;
                    if (body.Rewards != null)
                    {
                        Assert.IsTrue(body.Rewards.Count >= 0);
                    }

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Reward")]
        public void Test보상받기ApplyReward()
        {
            Assert.Inconclusive("Test 가능한 rewardId 값 필요.");
            return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                long rewardId = 1;

                this.ApiClient.ApplyReward(rewardId, false, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ApplyRewardResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ApplyRewardResponseBody body = response.ResultData as ApplyRewardResponseBody;
                    Assert.IsTrue(body.CallReturn != null);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Reward")]
        public void Test보상전체받기ApplyAllRewards()
        {
            //Assert.Inconclusive("deleteMail에 true나 false를 넣어도 InvalidParameter가 반환됨");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.ApplyAllRewards(true, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ApplyAllRewardsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ApplyAllRewardsResponseBody body = response.ResultData as ApplyAllRewardsResponseBody;

                    completion.Set();
                });

                completion.WaitOne();

                var completion2 = new ManualResetEvent(false);

                this.ApiClient.ApplyAllRewards(false, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ApplyAllRewardsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ApplyAllRewardsResponseBody body = response.ResultData as ApplyAllRewardsResponseBody;

                    completion2.Set();
                });

                completion2.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }


        #endregion REWARD

        #region SOCIALGRAPH

        [TestMethod, TestCategory("Social Graph")]
        public void Test친구들정보가져오기GetFriendsInfo()
        {
            //Assert.Inconclusive("InvalidParameter 발생");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                var friend_ids = new string[] { "881979482072261763", "881979482072261765" };
                var objectClasses = new List<string>()
                {
                    "sword",
                };

                this.ApiClient.GetFriendsInfo(friend_ids, objectClasses, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetFriendsInfoResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetFriendsInfoResponseBody body = response.ResultData as GetFriendsInfoResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

         [TestMethod, TestCategory("Social Graph")]
        public void Test친구목록가져오기GetFriends()
        {
            //Assert.Inconclusive("InvalidParameter 발생");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                var friend_ids = new string[] { "881979482072261763", "881979482072261765" };

                this.ApiClient.GetFriends("default", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetFriendsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    GetFriendsResponseBody body = response.ResultData as GetFriendsResponseBody;
                    Assert.IsTrue(body.Friends != null);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Social Graph")]
        public void Test친구리스트업데이트UpdateFriends()
        {
            //Assert.Inconclusive("InvalidParameter 발생");
            //return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                var friend_ids = new string[] { "-881979482072261763", "-881979482072261765" };

                this.ApiClient.UpdateFriends("default", "kakao", friend_ids, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is UpdateFriendsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    UpdateFriendsResponseBody body = response.ResultData as UpdateFriendsResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }


        #endregion SOCIALGRAPH

        [TestMethod, TestCategory("Etc")]
        public void TestToJson()
        {
            List<Item> items = new List<Item>()
            {
                new Item() { Id = 1, Grade = 2, Rate = 2 },
                new Item() { Id = 2, Grade = 3, Rate = 1 },
                new Item() { Id = 3, Grade = 4, Rate = 0 },
            };

            string jsonString = LitJson.JsonMapper.ToJson(items);
            string final_json = string.Format("{{ items: {0} }}", jsonString);
            Console.WriteLine(final_json);
        }

        [TestMethod, TestCategory("JsonData")]
        public void TestJsonDataToArray()
        {
            string testJson = "{\"result\":0,\"kakao\":{\"today_invites\":7,\"show_profile_image\":true,\"id\":null,\"total_invites\":7,\"reset\":{\"hour\":0,\"max\":1,\"nextTime\":1423699200,\"value\":0,\"period\":1},\"class\":\"kakao\",\"invited_ids\":[\"-000000016\",\"-000000084\",\"-000000065\"],\"numbers\":[1,2,3],\"bools\":[true, false, true]}}}";
            var jsonData = LitJson.JsonMapper.ToObject(testJson);

            var partJson = jsonData["kakao"]["invited_ids"];
            var idsList = partJson.ToList<string>();
            Assert.AreEqual(idsList.Count, 3);
            var ids = partJson.ToArray<string>();
            Assert.AreEqual(ids.Length, 3);

             var numbersJson = jsonData["kakao"]["numbers"];
             var intList = numbersJson.ToList<int>();
            Assert.AreEqual(intList.Count, 3);
            var longList = numbersJson.ToList<long>();
            Assert.AreEqual(longList.Count, 3);

            var boolsJson = jsonData["kakao"]["bools"];
            var boolList = boolsJson.ToList<bool>();
            Assert.AreEqual(boolList.Count, 3);
        }
    }
}
