using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hive5;
using UnityEngine;
using System.Threading;
using Hive5.Model;

namespace maui_sdk.test
{
    [TestClass]
    public class Hive5UnitTest
    {

        #region 설정값들

#if DEBUG
        public Hive5APIZone TestZone = Hive5APIZone.Beta;
#else
        public Hive5APIZone TestZone = Hive5APIZone.Production;
#endif
        public const string ValidUserId = "88197948207226176";
        public const string InvalidUserId = "88197948207226112";
        public const string ValidAppKey = "a40e4122-99d9-44a6-b916-68ed756f79d6";
        public const string Uuid = "747474747";

        public const string GoogleSdkVersion = "3";

        #endregion 설정값들

        public Hive5Client ApiClient { get; set; }

        [TestInitialize]
        public void InitializeTests()
        {
            if (this.ApiClient == null)
            {
                TestInit();
            }
        }

        private void Login()
        {
            string userId = Hive5UnitTest.ValidUserId;
            string sdkVersion = Hive5UnitTest.GoogleSdkVersion;
            string[] objectKeys = new string[] { "" };		// 로그인 후 가져와야할 사용자 object의 key 목록
            string[] configKeys = new string[] { "time_event1" };	// 로그인 후 가져와야할 사용자 configuration의 key

            try
            {
                var completion = new ManualResetEvent(false);

                this.ApiClient.Login(OSType.Android, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, (response) =>
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
            string appKey = Hive5UnitTest.ValidAppKey;
            string uuid = Hive5UnitTest.Uuid;

            try
            {
                client.Init(appKey, uuid, this.TestZone);
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
        public void Test로그아웃Logout()
        {
            string userId = Hive5UnitTest.ValidUserId;

            try
            {
                var completion = new ManualResetEvent(false);

                this.ApiClient.Logout(userId, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is LogoutResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                    // 2. 프로퍼티 검증
                    LogoutResponseBody body = response.ResultData as LogoutResponseBody;
                    Assert.IsTrue(body.UserData != null); // 잘못된 아이디로 로그인했으니

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

        #endregion AUTH



        #region LEADERBOARD
        
        [TestMethod, TestCategory("Leader Board")]
        public void Test내랭킹확인GetMyScore()
        {
            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.GetMyScore(3, 0, 100, (response) =>
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

                this.ApiClient.GetScores(3, 0, 100, null, null, (response) =>
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
                        Assert.IsTrue(score.items != null);
                        Assert.IsTrue(string.IsNullOrEmpty(score.platformUserId) == false);
                        Assert.IsTrue(score.rank >= -1);
                        Assert.IsTrue(score.scoredAt != null);
                        Assert.IsTrue(score.userData != null);
                        Assert.IsTrue(score.value != null);
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
        public void Test리더보드보상받기Prize()
        {
            Assert.Inconclusive("404에러 발생하는 것으로 알고 있음");
            return;

            try
            {
                Login();

                var completion = new ManualResetEvent(false);

                this.ApiClient.Prize(3, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is PrizeResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                     // 2. 프로퍼티 검증
                    PrizeResponseBody body = response.ResultData as PrizeResponseBody;
                    Assert.IsTrue(body.Prized != null);
                    Assert.IsTrue(body.Prized.rank >= -1);
                    Assert.IsTrue(body.Prized.reward != null);
                    Assert.IsTrue(body.Prized.score >= -1);
                    Assert.IsTrue(body.Prized.scoresCount >= 0);
                    Assert.IsTrue(body.Prized.topScores != null);
                    
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

                this.ApiClient.SubmitScore(3, 100, (response) =>
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

                this.ApiClient.GetSocialScores(3, (response) =>
                {
                    // 1. 기본 반환값 검증
                    Assert.IsTrue(response.ResultCode == Hive5ResultCode.Success); // 일단 반환성공
                    Assert.IsTrue(response.ResultData != null); // 반환데이터는 null이면 안 됨
                    Assert.IsTrue(response.ResultData is GetSocialScoresResponseBody); // 제대로 된 반환데이터가 오는지 타입체크

                     // 2. 프로퍼티 검증
                    GetSocialScoresResponseBody body = response.ResultData as GetSocialScoresResponseBody;
                    Assert.IsTrue(body.Scores != null);
                    Assert.IsTrue(body.Scores.Count > 0);
                    
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

      
    }
}
