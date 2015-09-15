using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hive5;
using System.Threading;
using Hive5.Models;
using System.Net;
using System.Collections.Generic;
using LitJson;
using Hive5.Util;
using System.Diagnostics;

namespace hive5_sdk_unity.test
{
    [TestClass]
    public class Hive5UnitTest
    {

        #region 설정값들

        public static TestConfig CurrentConfig { get; set; }

        #endregion 설정값들

        [TestInitialize]
        public void InitializeTests()
        {
            CurrentConfig = TestConfig.Default;
            if (Hive5Client.IsInitialized == false)
            {
                TestInit();
            }
        }

        private User LoggedInUser;

        private void Login(User user)
        {
            try
            {
                var completion = new ManualResetEvent(false);

                Hive5Client.Auth.LogIn(OSType.Android, "1.0", "ko-KR", user, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is LoginResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    LoginResponseBody body = response.ResultData as LoginResponseBody;
                    Assert.IsTrue(string.IsNullOrEmpty(body.AccessToken) == false); // 잘못된 아이디로 로그인했으니
                    Assert.IsTrue(body.NewMailCount >= 0);
                    Assert.IsFalse(string.IsNullOrEmpty(body.User.platform));
                    Assert.IsFalse(string.IsNullOrEmpty(body.User.id)); // 잘못된 아이디로 로그인했으니

                    LoggedInUser = body.User;
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
            Hive5Config.AppKey = CurrentConfig.AppKey;
            Hive5Config.Host = CurrentConfig.Host;
            Hive5Config.XPlatformKey = CurrentConfig.XPlatformKey;
            Hive5Config.CustomAccountPlatformName = CurrentConfig.CustomAccountPlatformName;
            Hive5Config.HealthCheckUrl = CurrentConfig.HealthCheckUrl;
            
            string uuid = CurrentConfig.Uuid;

            try
            {
                Hive5Client.Initialize(uuid);
                Hive5Client.SetDebugMode();
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
                Login(CurrentConfig.TestUser);
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
        //            Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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
                 Login(CurrentConfig.TestUser);
                string oldSessionKey = Hive5Client.Auth.SessionKey;

                Login(CurrentConfig.TestUser);
                string newSessionKey = Hive5Client.Auth.SessionKey;

                Assert.AreNotEqual(oldSessionKey, newSessionKey);

                Hive5Client.Auth.SetAccessToken(Hive5Client.Auth.AccessToken, oldSessionKey);
                var completion = new ManualResetEvent(false);

                Hive5Client.Settings.CheckNicknameAvailability("불량사과", (response) =>
                {
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.TheSessionKeyIsInvalid);
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
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                Hive5Client.Settings.CheckNicknameAvailability("불량사과", (response) =>
                {
                    // 1. 기본 반환값 검증
                    if (response.ResultCode != Hive5ErrorCode.Success)
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
        public void Test탈퇴Unregister()
        {
            try
            {
                Hive5Client.Initialize("unregister_test");
                Login(null);
                Hive5Client.Auth.Unregister((response) => {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크
                });
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Auth")]
        public void Test플랫폼계정생성CreatePlatformAccount()
        {
            var completion = new ManualResetEvent(false);

            string name = DateTime.Now.Ticks.ToString().ToString();
            string email = string.Format("{0}@example.com", name);
            Hive5Client.Auth.CreatePlatformAccount(name, "12345678", (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreatePlatformAccountResponseBody body = response.ResultData as CreatePlatformAccountResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.User.id) == false);
                Assert.IsTrue(string.IsNullOrEmpty(body.User.platform) == false);
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
            Hive5Client.Auth.CheckPlatformNameAvailability(name, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                    
                    // 2. 프로퍼티 검증

                    completion1.Set();
                });
            completion1.WaitOne();

            var completion2 = new ManualResetEvent(false);
            Hive5Client.Auth.CreatePlatformAccount(name, "12345678", (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreatePlatformAccountResponseBody body = response.ResultData as CreatePlatformAccountResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.User.id) == false);
                Assert.IsTrue(string.IsNullOrEmpty(body.User.platform) == false);
                completion2.Set();
            }, "tester", email);


            completion2.WaitOne();

            var completion3 = new ManualResetEvent(false);
            Hive5Client.Auth.CheckPlatformNameAvailability(name, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.AlreadyExistingPlatformUserName); // 일단 반환성공
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
            Hive5Client.Auth.CheckPlatformEmailAvailablity(email, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));

                // 2. 프로퍼티 검증

                completion1.Set();
            });
            completion1.WaitOne();

            var completion2 = new ManualResetEvent(false);
            Hive5Client.Auth.CreatePlatformAccount(name, "12345678", (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreatePlatformAccountResponseBody body = response.ResultData as CreatePlatformAccountResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.User.id) == false);
                Assert.IsTrue(string.IsNullOrEmpty(body.User.platform) == false);
                completion2.Set();
            }, "tester", email);


            completion2.WaitOne();

            var completion3 = new ManualResetEvent(false);
            Hive5Client.Auth.CheckPlatformEmailAvailablity(email, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.AlreadyExistingPlatformUserEmail); // 일단 반환성공
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
            Hive5Client.Auth.CreatePlatformAccount(name, password, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreatePlatformAccountResponseBody body = response.ResultData as CreatePlatformAccountResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.User.id) == false);
                Assert.IsTrue(string.IsNullOrEmpty(body.User.platform) == false);
                platformUserId = body.User.id;

                completion1.Set();
            }, "tester", email);

            completion1.WaitOne();

            var completion2 = new ManualResetEvent(false);
            Hive5Client.Auth.AuthenticatePlatformAccount(name, password, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(string.IsNullOrEmpty(response.ResultMessage));
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is AuthenticatePlatformAccountResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                AuthenticatePlatformAccountResponseBody body = response.ResultData as AuthenticatePlatformAccountResponseBody;
                Assert.IsTrue(body.User.id == platformUserId);

                completion2.Set();
            });

            completion2.WaitOne();

            var completion3 = new ManualResetEvent(false);
            Hive5Client.Auth.AuthenticatePlatformAccount(name, wrongPassword, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.InvalidNameOrPassword); // 일단 반환성공
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
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                Hive5Client.Leaderboard.GetMyScore(CurrentConfig.LeaderBoardKey, 0, 100, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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
        public void Test랭킹가져오기ListScores()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                Hive5Client.Leaderboard.ListScores(CurrentConfig.LeaderBoardKey, CurrentConfig.ObjectClasses, 0, 100, null, null, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ListScoresResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ListScoresResponseBody body = response.ResultData as ListScoresResponseBody;
                    Assert.IsTrue(body.Scores != null);
                    Assert.IsTrue(body.Scores.Count >= 0);

                    if (body.Scores.Count > 0)
                    {
                        var score = body.Scores[0];
                        Assert.IsTrue(string.IsNullOrEmpty(score.User.platform) == false);
                        Assert.IsTrue(string.IsNullOrEmpty(score.User.id) == false);
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
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                Hive5Client.Leaderboard.SubmitScore(CurrentConfig.LeaderBoardKey, 100, "", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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
        public void Test친구랭킹가져오기ListSocialScores()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                List<string> objectClasses = new List<string>()
                {
                    "sword",
                };
                Hive5Client.Leaderboard.ListSocialScores(CurrentConfig.LeaderBoardKey, objectClasses, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ListScoresResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ListScoresResponseBody body = response.ResultData as ListScoresResponseBody;
                    Assert.IsTrue(body.Scores != null);
                    Assert.IsTrue(body.Scores.Count > 0);

                    if (body.Scores.Count > 0)
                    {
                        var score = body.Scores[0];
                        Assert.IsTrue(string.IsNullOrEmpty(score.User.platform) == false);
                        Assert.IsTrue(string.IsNullOrEmpty(score.User.id) == false);
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
        public void Test메일태그제거RemoveMailTags()
        {
            //Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            //return;

            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                string[] initialTags = new string[] { "reward", "notice" };
                
                string createdMailId = CreateMail("aaaa", CurrentConfig.TestUser, "", initialTags);
                
                string[] removeTags = new string[] { "reward" };
                Hive5Client.Mail.RemoveTags(createdMailId, removeTags, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is RemoveMailTagsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    RemoveMailTagsResponseBody body = response.ResultData as RemoveMailTagsResponseBody;
                    Assert.IsTrue(body.Tags != null);
                    Assert.IsTrue(body.Tags.Count == 1);

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        private Hive5Response AddTags(string mailId, string[] tags)
        {
            var completion = new ManualResetEvent(false);

            Hive5Response body = null;
             
            Hive5Client.Mail.AddTags(mailId, tags, (response) =>
            {
                body = response;        
                completion.Set();
            });

            completion.WaitOne();
            return body;
        }

        [TestMethod, TestCategory("Mail")]
        public void Test메일태그추가AttachMailTags()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                string sampleTag = "reward";
                string[] tags = new string[] {  sampleTag };

                var createdMailId = CreateMail("abcd", CurrentConfig.TestUser, "", tags);
                
                var response = AddTags(createdMailId, tags);
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is AddMailTagsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                AddMailTagsResponseBody body = response.ResultData as AddMailTagsResponseBody;
                Assert.IsTrue(body.Tags != null);
                Assert.IsTrue(body.Tags.Count == 1);
                Assert.IsTrue(body.Tags.Contains(sampleTag) == true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }


        [TestMethod, TestCategory("Mail")]
        public void Test메일개수확인CountMail()
        {
             try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                string sampleTag = "reward";

                Hive5Client.Mail.CountMail(DataOrder.DESC, "0", sampleTag, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is CountMailsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    CountMailsResponseBody body = response.ResultData as CountMailsResponseBody;
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
        public void Test메일리스트가져오기ListMails()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                string sampleTag = "reward";

                Hive5Client.Mail.List(10, sampleTag, DataOrder.DESC, 0, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ListMailsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ListMailsResponseBody body = response.ResultData as ListMailsResponseBody;
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
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                // 지울 메일을 미리 생성
                string createMailId = CreateMail("메일삭제 테스트메일입니다.", CurrentConfig.TestUser, "", null);

                // 그 다음 삭제
                Hive5Client.Mail.DeleteMail(createMailId, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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
            try
            {
                Login(CurrentConfig.TestUser);

                string createdMailId = CreateMail("메일생성 테스트메일입니다.", CurrentConfig.TestUser, "", null);
                Assert.IsTrue(string.IsNullOrEmpty(createdMailId) == false);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        public string CreateMail(string content, User receiver, string extraJson, string[] tags)
        {
            string createdMailId = null;
            var completion = new ManualResetEvent(false);

            Hive5Client.Mail.Create(content, receiver, extraJson, tags, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreateMailResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                CreateMailResponseBody body = response.ResultData as CreateMailResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.Id) == false);

                createdMailId = body.Id;
                completion.Set();
            });

            completion.WaitOne();

            return createdMailId;
        }

        [TestMethod, TestCategory("Mail")]
        public void Test메일수정UpdateMail()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                string createMailId = CreateMail("메일수정 테스트메일입니다.", CurrentConfig.TestUser, "", null);

                Hive5Client.Mail.UpdateMail(createMailId, "수정된 메일수정 테스트메일입니다.", "", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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

        #region PROCEDURE

        [TestMethod, TestCategory("Procedure")]
        public void Test프로시저호출CallProcedure()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);
                var parameters = new {
                    echo = "gilbok"
                };

                Hive5Client.Script.RunScript("echo", parameters, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is RunScriptResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    RunScriptResponseBody body = response.ResultData as RunScriptResponseBody;
                    Assert.IsTrue(string.IsNullOrEmpty(body.CallReturn) == false);
                    Assert.AreEqual(body.CallReturn, parameters.echo);

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
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                var someValue = "dummy";
                var parameters = new { echo =  someValue};

                Hive5Client.Script.CheckScript("echo", parameters, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is RunScriptResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    RunScriptResponseBody body = response.ResultData as RunScriptResponseBody;
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
            Login(CurrentConfig.TestUser);

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
            Hive5Client.Script.RunScript("z_unittest_callee", parameterObject, (response) =>
            {
                if (response.ResultCode != Hive5ErrorCode.Success)
                {
                    completion.Set();
                    return;
                }

                var body = response.ResultData as RunScriptResponseBody;

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
            try
            {
                Login(CurrentConfig.TestUser);
                List<User> friends = new List<User>() {CurrentConfig.Friend};
                AddFriends("default", friends);

                var body = CreateGooglePurchase();

                Assert.IsNotNull(body);
                Assert.IsTrue(string.IsNullOrEmpty(body.Id) == false);
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

            CreateGooglePurchaseResponseBody body = null;

            

            Hive5Client.Purchase.CreateGooglePurchase(productCode, CurrentConfig.Friend, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is CreateGooglePurchaseResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                body = response.ResultData as CreateGooglePurchaseResponseBody;
                Assert.IsTrue(string.IsNullOrEmpty(body.Id)== false);

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
                Login(CurrentConfig.TestUser);

                var googlePurchaseBody = CreateGooglePurchase();

                var completion = new ManualResetEvent(false);

                string id = googlePurchaseBody.Id;
                long listPrice = 1100;
                long purchasePrice = 1100;
                string currency = null;
                string purchaseData = "{\"purchaseToken\":\"\",\"developerPayload\":\"\",\"packageName\":\"\",\"purchaseState\":,\"orderId\":\"\",\"purchaseTime\":,\"productId\":\"\"}";
                string signature = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx==";

                Hive5Client.Purchase.CompleteGooglePurchase(id, listPrice, purchasePrice, currency, purchaseData, signature, "", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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
                Login(CurrentConfig.TestUser);

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

            Hive5Client.Purchase.CreateNaverPurchase(productCode, payement_sequence, "hive5", 1, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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
                Login(CurrentConfig.TestUser);

                var purchaseBody = CreateNaverPurchase();

                var completion = new ManualResetEvent(false);

                long id = purchaseBody.Id;
                long listPrice = 1100;
                long purchasePrice = 1100;
                string currency = null;

                Hive5Client.Purchase.CompleteNaverPurchase(id, listPrice, purchasePrice, currency, "", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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
                Login(CurrentConfig.TestUser);

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

            CreateApplePurchaseResponseBody body = null;

            Hive5Client.Purchase.CreateGooglePurchase(productCode, CurrentConfig.Friend, (response) =>
            {
                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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
                Login(CurrentConfig.TestUser);

                var purchaseBody = CreateApplePurchase();

                var completion = new ManualResetEvent(false);

                long id = purchaseBody.Id;
                long listPrice = 1100;
                long purchasePrice = 1100;
                string currency = null;
                string receipt = "{\"purchaseToken\":\"\",\"developerPayload\":\"\",\"packageName\":\"\",\"purchaseState\":,\"orderId\":\"\",\"purchaseTime\":,\"productId\":\"\"}";
                bool isSandbox = false;

                Hive5Client.Purchase.CompleteApplePurchase(id, listPrice, purchasePrice, currency, receipt, isSandbox, "", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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

        [TestMethod, TestCategory("Coupon")]
        public void Test쿠폰적용RedeemCoupon()
        {
            Assert.Inconclusive("현재 alpha 서버에서는 적용할 수 있는 쿠폰 발행이 불가 발생");
            return;

            try
            {
                Login(CurrentConfig.TestUser);

                var completion1 = new ManualResetEvent(false);

                var coupon = "555NL985DDLBBYGN"; // 테스트 전에 알파서버서 발급하기
                
                Hive5Client.Coupon.Redeem(coupon, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is RunScriptResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    RunScriptResponseBody body = response.ResultData as RunScriptResponseBody;

                    completion1.Set();
                });

                completion1.WaitOne();

                var completion2 = new ManualResetEvent(false);

                Hive5Client.Coupon.Redeem(coupon, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.ThePlayerHasAlreadyConsumedTheCoupon); // 일단 반환성공
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

        #region SETTINGS
         [TestMethod, TestCategory("Settings")]
        public void Test푸쉬토큰등록및업데이트UpdatePushToken()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                Hive5Client.Settings.UpdatePushToken (PlatformType.Kakao, "test_token", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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

        [TestMethod, TestCategory("Settings")]
        public void Test푸쉬수신활성화ActivatePush()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                Hive5Client.Settings.ActivatePush((response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ActivatePushResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ActivatePushResponseBody body = response.ResultData as ActivatePushResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        [TestMethod, TestCategory("Settings")]
        public void Test푸쉬수신비활성화DeactivatePush()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                Hive5Client.Settings.DeactivatePush((response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ActivatePushResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ActivatePushResponseBody body = response.ResultData as ActivatePushResponseBody;

                    completion.Set();
                });

                completion.WaitOne();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }
        #endregion SETTINGS

        #region SOCIALGRAPH

        [TestMethod, TestCategory("Social Graph")]
        public void Test친구목록가져오기GetFriends()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                Hive5Client.SocialGraph.ListFriends("default", (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is ListFriendsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    ListFriendsResponseBody body = response.ResultData as ListFriendsResponseBody;
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
        public void Test친구추가AddFriends()
        {
            try
            {
                Login(CurrentConfig.TestUser);

                var friends = new List<User>()
                { 
                    new User() { platform = "anonymous", id = "14" }, 
                    new User() { platform = "kakao", id = "-881979482072261765" },
                };

                var response = AddFriends("default", friends);

                // 1. 기본 반환값 검증
                Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
                Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                Assert.IsTrue(response.ResultData is AddFriendsResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                // 2. 프로퍼티 검증
                AddFriendsResponseBody body = response.ResultData as AddFriendsResponseBody;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message + ex.InnerException != null ? "\n" + ex.InnerException : "");
            }
        }

        private Hive5Response AddFriends(string group, List<User> friends)
        {
            Hive5Response responseReturn = null;
            var completion = new ManualResetEvent(false);
            Hive5Client.SocialGraph.AddFriends(group, friends, (response) =>
            {
                responseReturn = response;
                completion.Set();
            });

            completion.WaitOne();

            return responseReturn;
        }

        [TestMethod, TestCategory("Social Graph")]
        public void Test친구리스트업데이트UpdateFriends()
        {
           
            try
            {
                Login(CurrentConfig.TestUser);

                var completion = new ManualResetEvent(false);

                var friends = new List<Friend>()
                { 
                    new Friend() { platform = "kakao", id = "-881979482072261763" }, 
                    new Friend() { platform = "kakao", id = "-881979482072261765" },
                };

                Hive5Client.SocialGraph.UpdateFriends("default", friends, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ErrorCode.Success); // 일단 반환성공
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

        [TestMethod]
        public void TestJsonSerializeTupleList()
        {
            var parameters = new TupleList<string, string>();
            parameters.Add("echo", "gilbok");
            var serialized = LitJson.JsonMapper.ToJson(parameters);
        }

        [TestMethod]
        public void TestIssueRequestId()
        {
            Dictionary<long, bool> idDic = new Dictionary<long, bool>();
           
            bool duplicated = false;
            for (int i = 0; i < 10000; i++)
            {
                PublishMessage message = new PublishMessage();
                if (idDic.ContainsKey(message.RequestId) == true)
                {
                    Debug.WriteLine("duplicated: " + message.RequestId.ToString());
                    duplicated = true;
                    continue;
                }

                Debug.WriteLine(message.RequestId);
                idDic.Add(message.RequestId, false);
            }

            Assert.IsFalse(duplicated);
        }
    }
}
