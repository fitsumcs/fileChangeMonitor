# File Change Monitor

This is a simple C# console application that monitors changes in a specified text file and reports the changes every 15 seconds.

## How to Run

1. Clone the repository.
2. Open the project in Visual Studio Code.
3. Create a `.env` file in the root of your project directory and add the following line:
   ```env
   TARGET_FILE_PATH=/path/to/your/file.txt
   ```
4. Install the required packages using the terminal:

```sh
dotnet add package dotenv.net
dotnet add package DiffMatchPatch
dotnet add package Microsoft.Extensions.DependencyInjection

```
