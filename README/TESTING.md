### Description

This lab will focus on testing code with Copilot. The testing framework has already been created in the `/tests/` directory.

### Steps
#### 1. Ask Copilot how to test the Routes.cs file.
```text
How do I write test cases for #file:Routes.cs?
```

<details>
<summary>Output</summary>
<p>

GitHub Copilot: To write test cases for [`Routes.cs`](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2FUsers%2Fcory%2FProjects%2Fgithub%2Finfomagnus%2Fim-copilot-sandbox%2Fcsharp-webapi-memorygame%2Fapi%2FRoutes%2FRoutes.cs%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%5D "/Users/cory/Projects/github/infomagnus/im-copilot-sandbox/csharp-webapi-memorygame/api/Routes/Routes.cs"), follow these steps:

1. **Setup Test Environment**: Use a testing framework like xUnit for .NET.
2. **Mock Dependencies**: Mock the [`WebApplication`](command:_github.copilot.openSymbolFromReferences?%5B%7B%22%24mid%22%3A1%2C%22path%22%3A%22%2Fvar%2Ffolders%2F16%2F1p4hvhz51ql6z4yk5j7_v32c0000gn%2FT%2FMetadataAsSource%2F9e92525448324d94ba5a2d95b3ebfc40%2FDecompilationMetadataAsSourceFileProvider%2Fbfa922407a3a4f27af9389434f4645fa%2FWebApplication.cs%22%2C%22scheme%22%3A%22file%22%7D%2C%7B%22line%22%3A26%2C%22character%22%3A24%7D%5D "../../../../../../../var/folders/16/1p4hvhz51ql6z4yk5j7_v32c0000gn/T/MetadataAsSource/9e92525448324d94ba5a2d95b3ebfc40/DecompilationMetadataAsSourceFileProvider/bfa922407a3a4f27af9389434f4645fa/WebApplication.cs") and any services or data access layers used, such as [`GameData`](command:_github.copilot.openSymbolFromReferences?%5B%7B%22%24mid%22%3A1%2C%22path%22%3A%22%2FUsers%2Fcory%2FProjects%2Fgithub%2Finfomagnus%2Fim-copilot-sandbox%2Fcsharp-webapi-memorygame%2Fapi%2FServices%2FGameData.cs%22%2C%22scheme%22%3A%22file%22%7D%2C%7B%22line%22%3A5%2C%22character%22%3A17%7D%5D "api/Services/GameData.cs").
3. **Write Test Methods**: For each endpoint, write a test method that simulates a request and asserts the expected response.

### Pseudocode for Test Cases

```plaintext
1. Setup xUnit test project
2. Add Moq package for mocking dependencies
3. Create a class GameEndpointsTests

   For each endpoint in Routes.cs:
   
   a. Test "/greeting" GET Endpoint
      - Mock WebApplication
      - Call the endpoint
      - Assert the response is 200 OK and the content matches the greeting message

   b. Test "/game" POST Endpoint
      - Mock WebApplication and GameData service
      - Create a valid Game object and send it
      - Assert the response is 200 OK and the content indicates success
      - Repeat with invalid Game object and assert 400 Bad Request

   c. Test "/game/{handle}" GET Endpoint
      - Mock WebApplication and GameData service to return a Game object
      - Call the endpoint with a valid handle
      - Assert the response is 200 OK and the Game object is returned
      - Repeat with an invalid handle and assert 404 Not Found

   d. Test "/leaderboard" POST Endpoint
      - Mock WebApplication and GameData service
      - Create a valid Leaderboard entry and send it
      - Assert the response is 200 OK and the content indicates success
      - Repeat with invalid entry and assert 400 Bad Request

   e. Test "/leaderboard" GET Endpoint
      - Mock WebApplication and GameData service to return a list of Leaderboard entries
      - Call the endpoint
      - Assert the response is 200 OK and the top ten entries are returned
```

### Example Test Case for "/greeting" Endpoint

```csharp
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using api.Routes;
using System.Threading.Tasks;

public class GameEndpointsTests
{
    [Fact]
    public async Task GreetingEndpoint_ReturnsWelcomeMessage()
    {
        // Arrange
        var application = new WebApplicationMock();
        GameEndpoints.MapGameEndpoints(application.App);

        // Act
        var response = await application.Client.GetAsync("/greeting");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Welcome to the Memory Game API!", content);
    }
}

// Mock setup for WebApplication, using TestServer or similar approach
```

This example demonstrates how to test the "/greeting" endpoint. You would need to create similar tests for the other endpoints, adjusting the setup and assertions according to what each endpoint does. Remember to mock [`GameData`](command:_github.copilot.openSymbolFromReferences?%5B%7B%22%24mid%22%3A1%2C%22path%22%3A%22%2FUsers%2Fcory%2FProjects%2Fgithub%2Finfomagnus%2Fim-copilot-sandbox%2Fcsharp-webapi-memorygame%2Fapi%2FServices%2FGameData.cs%22%2C%22scheme%22%3A%22file%22%7D%2C%7B%22line%22%3A5%2C%22character%22%3A17%7D%5D "api/Services/GameData.cs") and any other dependencies to isolate the tests from external factors.
</p>
</details>

#### 2. Ask Copilot how to install xunit

```text
How can xunit be installed?
```

`NOTE` that xunit and xunit runner has already been installed for this lab.

<details>
<summary>Output</summary>
<p>
GitHub Copilot: To install xUnit in your .NET project, follow these steps using the .NET CLI:

1. Open your terminal.
2. Navigate to your project directory.
3. Run the following command to install the xUnit package:

```shell
dotnet add package xunit --version 2.8.1
```

4. To add the xUnit runner, which is necessary to run your tests, execute:

```shell
dotnet add package xunit.runner.visualstudio --version 2.8.1
```

5. Optionally, if you want to use the `[Theory]` and `[InlineData]` attributes for parameterized tests, you might also want to install the `xunit.extensions` package:

```shell
dotnet add package xunit.extensions --version 2.8.1
```

After running these commands, your project will have xUnit installed, and you can start writing your tests.
</p>
</details>

#### 3. Ask Copilot How to Check Code Coverage

