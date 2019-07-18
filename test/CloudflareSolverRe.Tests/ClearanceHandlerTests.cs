﻿using CloudflareSolverRe.CaptchaProviders;
using CloudflareSolverRe.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudflareSolverRe.Tests
{
    [TestClass]
    public class ClearanceHandlerTests
    {
        [TestMethod]
        public void SolveWebsiteChallenge_uamhitmehardfun()
        {
            var target = new Uri("https://uam.hitmehard.fun/HIT");

            var handler = new ClearanceHandler
            {
                MaxTries = 3,
                ClearanceDelay = 3000
            };

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0");

            var content = client.GetStringAsync(target).Result;
            Assert.AreEqual("Dstat.cc is the best", content);
        }

        [TestMethod]
        public void SolveWebsiteChallenge_uamhitmehardfun_WithAntiCaptcha()
        {
            var target = new Uri("https://uam.hitmehard.fun/HIT");

            var handler = new ClearanceHandler(new AntiCaptchaProvider("YOUR_API_KEY"))
            {
                MaxTries = 3,
                MaxCaptchaTries = 2,
                //ClearanceDelay = 3000  // Default value is the delay time determined in challenge code (not required in captcha)
            };

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0");

            var content = client.GetStringAsync(target).Result;
            Assert.AreEqual("Dstat.cc is the best", content);
        }

        [TestMethod]
        public void SolveWebsiteChallenge_uamhitmehardfun_With2Captcha()
        {
            var target = new Uri("https://uam.hitmehard.fun/HIT");

            var handler = new ClearanceHandler(new TwoCaptchaProvider("YOUR_API_KEY"))
            {
                MaxTries = 3,
                MaxCaptchaTries = 2,
                //ClearanceDelay = 3000  // Default value is the delay time determined in challenge code (not required in captcha)
            };

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:66.0) Gecko/20100101 Firefox/66.0");

            var content = client.GetStringAsync(target).Result;
            Assert.AreEqual("Dstat.cc is the best", content);
        }
    }
}