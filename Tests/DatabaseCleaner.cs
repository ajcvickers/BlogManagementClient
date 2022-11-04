using Xunit.Abstractions;
using Xunit.Sdk;

[assembly: TestFramework("DatabaseCleaner", "Tests")]
public class DatabaseCleaner : XunitTestFramework
{
    public DatabaseCleaner(IMessageSink messageSink)
        :base(messageSink)
    {
        TestData.RecreateDatabase(10, 10);
    }
}
