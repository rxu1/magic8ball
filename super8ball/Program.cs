using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading; 
using Microsoft.CognitiveServices.Speech;

namespace super8Ball
{


  public class Super8ball
  {

    static void Main(string[] args)
    {
      StartContinuousRecognitionAsync().Wait();
      // SynthesisToSpeakerAsync().Wait();
      Console.WriteLine("Please press a key to continue.");
      string questionText = Console.ReadLine();

      ConsoleColor oldColor = Console.ForegroundColor; 
      System.Console.ForegroundColor = System.ConsoleColor.Cyan;
      System.Console.BackgroundColor = System.ConsoleColor.Black;

      string welcome = System.IO.File.ReadAllText(@"../welcome.txt");
      System.Console.WriteLine("{0}", welcome);


      
      ProgramName();

      Random randomObject = new Random();
      while(true)
      {
        string questionString = GetQuestionFromUser();

        int numberOfSecondsToSleep = randomObject.Next(2)+1;
        System.Console.WriteLine("Thinking about your answer, stand by...");
        Thread.Sleep(numberOfSecondsToSleep * 1000);

        if (questionString.Length == 0)
        {
          System.Console.WriteLine("You need to type a question! I can't read  minds!");
          continue;
        }

        //Exits out of question
        if (questionString.ToLower() == "quit")
        {
          break;
        }
        
        //joke
        if (questionString.ToLower() == "you suck")
        {
          System.Console.WriteLine("You suck even more! Bye!");
          break;
        }
        // System.Console.WriteLine("{0}", randomObject.Next(10)+1);
        int randomNumber = randomObject.Next(6);
        //use random numnber to determine response
        switch(randomNumber)
        {
          case 0: 
            {
              // System.Console.WriteLine("NullReference Exception");
              string text = System.IO.File.ReadAllText(@"../1.txt");
              System.Console.WriteLine("{0}", text);
              break;
            }
          case 1: 
            {
              // System.Console.WriteLine("Your questions is very important to us, please wait as I pretend to care.");
              string text = System.IO.File.ReadAllText(@"../2.txt");
              System.Console.WriteLine("{0}", text);
              break;
            }
          case 2:
            {
              // System.Console.WriteLine("I'm Batmaaaan!");
              string text = System.IO.File.ReadAllText(@"../3.txt");
              System.Console.WriteLine("{0}", text);
              break;
            }
          case 3:
            {
              // System.Console.WriteLine("Ask again never");
              string text = System.IO.File.ReadAllText(@"../4.txt");
              System.Console.WriteLine("{0}", text);
              break;
            }
          case 4:
            {
              // System.Console.WriteLine("You were expecting me to say yes or no, weren't you? Surprised you, haven't I?");
              string text = System.IO.File.ReadAllText(@"../5.txt");
              System.Console.WriteLine("{0}", text);
              break;
            }
          case 5:
            {
              string text = System.IO.File.ReadAllText(@"../6.txt");
              System.Console.WriteLine("{0}", text);
              // System.Console.WriteLine("Out of over 7 billion people on this planet, you decided to ask me?");
              break;
            }
          // case 6:
          //   {
          //     System.Console.WriteLine("I'm literally an ASCII 8 ball. What are you expecting, the answer to life?");
          //     break;
          //   }
          // case 7:
          //   {
          //     System.Console.WriteLine("Error 404: Answer not found, try again!");
          //     break;
          //   }
        }
      }
      
      Console.ForegroundColor = oldColor;

    }
    public static void ProgramName()
    {
      System.Console.WriteLine("Magic 8 Ball (by: Justin, Pierre, Raymond)");
    }

    public static string GetQuestionFromUser()
    {
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write($"Ask a question?: {questionText}");
      Console.ForegroundColor = ConsoleColor.Cyan;
      string questionString = Console.ReadLine();

      return questionString;
    }

    public static async Task StartContinuousRecognitionAsync()
    {
      // Creates an instance of a speech config with specified subscription key and service region.
      // Replace with your own subscription key and service region (e.g., "westus").
      var config = SpeechConfig.FromSubscription("f961d606be3a4bc38f2a0a1ef0cc0d5a", "westus");

      // Creates a speech recognizer.
      using (var recognizer = new SpeechRecognizer(config))
      {
        Console.WriteLine("Say something...");

        // Starts speech recognition, and returns after a single utterance is recognized. The end of a
        // single utterance is determined by listening for silence at the end or until a maximum of 15
        // seconds of audio is processed.  The task returns the recognition text as result. 
        // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
        // shot recognition like command or query. 
        // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
        var result = await recognizer.RecognizeOnceAsync();

        // Checks result.
        if (result.Reason == ResultReason.RecognizedSpeech)
        {
          Console.WriteLine($"We recognized: {result.Text}");
        }
        else if (result.Reason == ResultReason.NoMatch)
        {
          Console.WriteLine($"NOMATCH: Speech could not be recognized.");
        }
        else if (result.Reason == ResultReason.Canceled)
        {
          var cancellation = CancellationDetails.FromResult(result);
          Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

          if (cancellation.Reason == CancellationReason.Error)
          {
            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
            Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
            Console.WriteLine($"CANCELED: Did you update the subscription info?");
          }
        }
        
      }
    }



  }
}