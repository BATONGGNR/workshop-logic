using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using WorkshopLogic;

namespace WorkshopTests
{
    public class LoadTestGenerator
    {
        private readonly ConcurrentBag<string> _results = new();
        private readonly string _storagePath = "./load_test_storage";

        public async Task<LoadTestResult> RunLoadTest(int userCount, int operationsPerUser)
        {
            var stopwatch = Stopwatch.StartNew();
            var successCount = 0;
            var failCount = 0;
            var totalOperations = userCount * operationsPerUser;

            var tasks = new Task[userCount];

            for (int i = 0; i < userCount; i++)
            {
                int userId = i;
                tasks[i] = Task.Run(async () =>
                {
                    for (int j = 0; j < operationsPerUser; j++)
                    {
                        try
                        {
                            await SimulateUserOperation(userId, j);
                            System.Threading.Interlocked.Increment(ref successCount);
                        }
                        catch (Exception ex)
                        {
                            _results.Add($"User {userId} Op {j}: {ex.Message}");
                            System.Threading.Interlocked.Increment(ref failCount);
                        }
                    }
                });
            }

            await Task.WhenAll(tasks);
            stopwatch.Stop();

            return new LoadTestResult
            {
                TotalUsers = userCount,
                TotalOperations = totalOperations,
                SuccessCount = successCount,
                FailCount = failCount,
                TotalTimeMs = stopwatch.ElapsedMilliseconds,
                AvgTimePerOperation = (double)stopwatch.ElapsedMilliseconds / totalOperations,
                OperationsPerSecond = totalOperations / (stopwatch.ElapsedMilliseconds / 1000.0)
            };
        }

        private Task SimulateUserOperation(int userId, int operationId)
        {
            return Task.Run(() =>
            {
                var authService = new AuthenticationService();
                var updater = new UserUpdater(authService, _storagePath);
                var simulator = new WorkshopSimulator();

                // Регистрация/авторизация
                updater.Update(userId, $"user_{userId}", $"pass_{userId}", "Operator");
                authService.Login($"user_{userId}", $"pass_{userId}");

                // Расчёты (имитация работы цеха)
                simulator.CalculateLoadFactor(45 + userId % 10, 15 + userId % 5);
                simulator.CanProcessPart(5 + userId % 3, 10);
                simulator.CalculateBatchTime(20 + userId % 5, 5);
            });
        }

        public void CleanupTestFiles()
        {
            if (System.IO.Directory.Exists(_storagePath))
            {
                System.IO.Directory.Delete(_storagePath, true);
            }
        }
    }

    public class LoadTestResult
    {
        public int TotalUsers { get; set; }
        public int TotalOperations { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
        public long TotalTimeMs { get; set; }
        public double AvgTimePerOperation { get; set; }
        public double OperationsPerSecond { get; set; }
    }
}