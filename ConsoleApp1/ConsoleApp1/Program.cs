

using ConsoleApp1;
using System;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

/**
 *# Problem Statement

You are required to develop a C# console application that retrieves and processes user data from a public API. The application will perform the following tasks:

1. Fetch user information from the API: `https://jsonplaceholder.typicode.com/users`
2. Fetch user posts using user IDs from the API: `https://jsonplaceholder.typicode.com/posts?userId=`
3. Print the list of users and their posts.
4. (Optional) Print the time taken to fetch and process the data.

# Example Output

```
Name: Leanne Graham  
Posts:
- Post title
- Post title

Name: Ervin Howell  
Posts:
- Post title
- Post title
- Post title

(Optional) Fetched and processed 10 records in 1ms.

// */
//var watch = new Stopwatch();
//using var client = new HttpClient();
//var jsonConfig = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };


//watch.Start();
//var users = await client.GetFromJsonAsync<IEnumerable<User>>("https://jsonplaceholder.typicode.com/users");
////var userContent = await usersData.Content.ReadAsStringAsync();
////var users = JsonSerializer.Deserialize<IEnumerable<User>>(usersData, jsonConfig);
//watch.Stop();
//var usersTime = watch.ElapsedMilliseconds;
//Console.WriteLine("USERS");
//Console.WriteLine($"time: {usersTime} ms{Environment.NewLine}time/10 records: {usersTime / (users.Count() / 10)} ms");
//Console.WriteLine();

//watch.Restart();
//var postsData = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts");
//var postContent = await postsData.Content.ReadAsStringAsync();
//var posts = JsonSerializer.Deserialize<IEnumerable<Post>>(postContent, jsonConfig);
//watch.Stop();
//var postsTime = watch.ElapsedMilliseconds;
//Console.WriteLine("POSTS");
//Console.WriteLine($"time: {postsTime} ms{Environment.NewLine}time/10 records: {postsTime / (posts.Count() / 10)} ms");
//Console.WriteLine();


//Console.WriteLine("TOTAL");
//Console.WriteLine($"time: {usersTime + postsTime} ms{Environment.NewLine}time/10 records: {usersTime + postsTime / ((users.Count() + posts.Count()) / 10)} ms");

////watch.Restart();
//////var usersRequest = users.Select(
//////    user => Task.Run(async () =>
//////    {
//////        var postData = await client.GetAsync($"https://jsonplaceholder.typicode.com/posts?userId={user.Id}");
//////        var postContent = await postData.Content.ReadAsStringAsync();
//////        user.Posts = JsonSerializer.Deserialize<Post[]>(postContent, jsonConfig);
//////    }));

//////await Task.WhenAll(usersRequest);

//foreach (var user in users)
//{
//    Console.WriteLine($"Name: {user.Name}");
//    Console.WriteLine("Post:");
//    foreach (var post in posts.Where(p => p.UserId == user.Id))
//    {
//        Console.WriteLine($"- {post.Title}");
//    }

//    Console.WriteLine();
//}

//watch.Stop();
//Console.WriteLine(watch.ElapsedMilliseconds);

//var chunker = new Chunker();
//var actual = Chunker.Chunk("abcdefg", 3);
//foreach (var chunk in actual)
//    Console.WriteLine(chunk);


string uri = "https://raw.githubusercontent.com/qualified/challenge-data/master/words_alpha.txt";
using (var client = new HttpClient())
{
    var result = client
                .GetAsync(uri)
                .ConfigureAwait(true)
                .GetAwaiter()
                .GetResult();
    var content = result
                  .Content
                  .ReadAsStringAsync()
                  .ConfigureAwait(true)
                  .GetAwaiter()
                  .GetResult();
    Console.WriteLine(content);
    var x = content.Split("\n");
}