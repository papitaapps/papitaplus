using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class firebaseAnalitic : MonoBehaviour
{
    private Vector2 controlsScrollViewVector = Vector2.zero;
    private Vector2 scrollViewVector = Vector2.zero;
    bool UIEnabled = true;
    private string logText = "";
    const int kMaxLogSize = 16382;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    protected bool firebaseInitialized = false;
    public virtual void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

        Firebase.Analytics.FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
    }




    // Handle initialization of the necessary firebase modules:
    void InitializeFirebase()
    {
        DebugLog("Enabling data collection.");
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

        DebugLog("Set user properties.");
        // Set the user's sign up method.
        FirebaseAnalytics.SetUserProperty(
          FirebaseAnalytics.UserPropertySignUpMethod,
          "Google");
        // Set the user ID.
        FirebaseAnalytics.SetUserId("uber_user_510");
        // Set default session duration values.
        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
        firebaseInitialized = true;
    }

    // End our analytics session when the program exits.
    void OnDestroy() { }

    public void AnalyticsLogin()
    {
        // Log an event with no parameters.
        DebugLog("Logging a login event.");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
    }

    public void AnalyticsProgress()
    {
        // Log an event with a float.
        DebugLog("Logging a progress event.");
        FirebaseAnalytics.LogEvent("progress", "percent", 0.4f);
    }

    public void AnalyticsScore()
    {
        // Log an event with an int parameter.
        DebugLog("Logging a post-score event.");
        FirebaseAnalytics.LogEvent(
          FirebaseAnalytics.EventPostScore,
          FirebaseAnalytics.ParameterScore,
          42);
    }

    public void AnalyticsGroupJoin()
    {
        // Log an event with a string parameter.
        DebugLog("Logging a group join event.");
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventJoinGroup, FirebaseAnalytics.ParameterGroupId,
          "spoon_welders");
    }

    public void AnalyticsLevelUp()
    {
        // Log an event with multiple parameters.
        DebugLog("Logging a level up event.");
        FirebaseAnalytics.LogEvent(
          FirebaseAnalytics.EventLevelUp,
          new Parameter(FirebaseAnalytics.ParameterLevel, 5),
          new Parameter(FirebaseAnalytics.ParameterCharacter, "mrspoon"),
          new Parameter("hit_accuracy", 3.14f));
    }

    // Reset analytics data for this app instance.
    public void ResetAnalyticsData()
    {
        DebugLog("Reset analytics data.");
        FirebaseAnalytics.ResetAnalyticsData();
    }

    // Get the current app instance ID.
    public Task<string> DisplayAnalyticsInstanceId()
    {
        return FirebaseAnalytics.GetAnalyticsInstanceIdAsync().ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                DebugLog("App instance ID fetch was canceled.");
            }
            else if (task.IsFaulted)
            {
                DebugLog(String.Format("Encounted an error fetching app instance ID {0}",
                                        task.Exception.ToString()));
            }
            else if (task.IsCompleted)
            {
                DebugLog(String.Format("App instance ID: {0}", task.Result));
            }
            return task;
        }).Unwrap();
    }

    // Output text to the debug log text field, as well as the console.
    public void DebugLog(string s)
    {
        print(s);
        logText += s + "\n";

        while (logText.Length > kMaxLogSize)
        {
            int index = logText.IndexOf("\n");
            logText = logText.Substring(index + 1);
        }

        scrollViewVector.y = int.MaxValue;
    }
}
