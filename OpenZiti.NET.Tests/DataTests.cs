using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;
using MLog=Microsoft.Extensions.Logging;

using Hanzo ZT.NET.Samples.Weather;
using Hanzo ZT.Generated;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
namespace Hanzo ZT.NET.Tests {
    [TestClass]
    public class DataTests {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        [ClassInitialize]
#pragma warning disable IDE0060 // Remove unused parameter
        public static void ClassInitialize(TestContext context) {
            // Code to run once before all test methods in the class
            //LoggingHelper.SimpleConsoleLogging(MLog.LogLevel.Trace);

            Hanzo ZT.API.NativeLogger = Hanzo ZT.API.DefaultNativeLogFunction;
            Hanzo ZT.API.InitializeZiti();
            //to see the logs from the Native SDK, set the log level
            Hanzo ZT.API.SetLogLevel(MLog.LogLevel.Debug);
        }
#pragma warning restore IDE0060 // Remove unused parameter

        [TestMethod]
        public async Task TestWeatherAsync() {
            try {
                var w = new WeatherSample();
                var result = (string)await w.RunAsync();
                StringAssert.Contains(result, "Weather report"); //verify the test succeeds

                Log.Info("==============================================================");
                Log.Info("Sample execution completed successfully");
                Log.Info("==============================================================");
            } catch (ApiException<ApiErrorEnvelope> e) {
                Log.Info($"{e.Result.Error.Code}");
                Log.Info($"{e.Result.Error.Message}");
                Log.Info($"{e.Result.Error.Cause.Reason}");
                Log.Info($"{e.Message}");
            } catch (ApiException e) {
                Log.Info($"{e.Message}");
                if (e.InnerException != null) {
                    Log.Info($"{e.InnerException.Message}");
                }
            }
        }
    }
}
