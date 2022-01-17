namespace DRAMA.Extensions.Playwright;

// TODO: Write Unit Tests For These Methods

public static class PageExtensions
{
    /// <summary>
    ///     Clicks the element with the defined selector and waits for the call to the endpoint that contains the defined substring to complete.
    ///     The method also takes an optional number of milliseconds to wait for, additionally, after the initial tasks have completed.
    /// </summary>
    public static async Task ClickElementAndWaitForResponse(this IPage page, string clickableElementSelector, string endpointSubstring, int additionalWaitInMilliseconds = 0)
        => await Task.WhenAll(page.ClickAsync(clickableElementSelector), page.WaitForResponseAsync(response => response.Url.Contains(endpointSubstring)))
            .ContinueWith(task => Task.Delay(additionalWaitInMilliseconds));

    /// <summary>
    ///     Clicks the element with the defined selector and waits for the calls to the endpoints that contain all the respective defined substrings to complete.
    ///     The method also takes an optional number of milliseconds to wait for, additionally, after the initial tasks have completed.
    /// </summary>
    public static async Task ClickElementAndWaitForResponses(this IPage page, string clickableElementSelector, string[] endpointSubstrings, int additionalWaitInMilliseconds = 0)
        => await Task.WhenAll(endpointSubstrings.Select(endpointSubstring => page.WaitForResponseAsync(response => response.Url.Contains(endpointSubstring)))
            .Prepend(page.ClickAsync(clickableElementSelector))).ContinueWith(task => Task.Delay(additionalWaitInMilliseconds));

    /// <summary>
    ///     Clicks the defined element and waits for the call to the endpoint that contains the defined substring to complete.
    ///     The method also takes an optional number of milliseconds to wait for, additionally, after the initial tasks have completed.
    /// </summary>
    public static async Task ClickElementAndWaitForResponse(this IPage page, IElementHandle clickableElement, string endpointSubstring, int additionalWaitInMilliseconds = 0)
        => await Task.WhenAll(clickableElement.ClickAsync(), page.WaitForResponseAsync(response => response.Url.Contains(endpointSubstring)))
            .ContinueWith(task => Task.Delay(additionalWaitInMilliseconds));

    /// <summary>
    ///     Clicks the defined element and waits for the calls to the endpoints that contain all the respective defined substrings to complete.
    ///     The method also takes an optional number of milliseconds to wait for, additionally, after the initial tasks have completed.
    /// </summary>
    public static async Task ClickElementAndWaitForResponses(this IPage page, IElementHandle clickableElement, string[] endpointSubstrings, int additionalWaitInMilliseconds = 0)
        => await Task.WhenAll(endpointSubstrings.Select(endpointSubstring => page.WaitForResponseAsync(response => response.Url.Contains(endpointSubstring)))
            .Prepend(clickableElement.ClickAsync())).ContinueWith(task => Task.Delay(additionalWaitInMilliseconds));

    /// <summary>
    ///     <para>
    ///         Runs a task and waits for the call to the endpoint that contains the defined substring to complete.
    ///         The task should be an action which triggers an API call, such as navigating to a page or submitting a form.
    ///         The method also takes an optional number of milliseconds to wait for, additionally, after the initial tasks have completed.
    ///     </para>
    ///     <para>
    ///         Playwright does a very good job at automatically waiting for specific events (e.g. navigation completed or DOM loaded) to be fired, however, this does not work well in some scenarios.
    ///         This method should only be used in those rare cases which are not supported by the automatic waiting built into Playwright.
    ///     </para>
    /// </summary>
    public static async Task RunTaskAndWaitForResponse(this IPage page, Task apiCallingTask, string endpointSubstring, int additionalWaitInMilliseconds = 0)
        => await Task.WhenAll(apiCallingTask, page.WaitForResponseAsync(response => response.Url.Contains(endpointSubstring)))
            .ContinueWith(task => Task.Delay(additionalWaitInMilliseconds));

    /// <summary>
    ///     <para>
    ///         Runs a task and waits for the calls to the endpoints that contain all the respective defined substrings to complete.
    ///         The task should be an action which triggers an API call, such as navigating to a page or submitting a form.
    ///         The method also takes an optional number of milliseconds to wait for, additionally, after the initial tasks have completed.
    ///     </para>
    ///     <para>
    ///         Playwright does a very good job at automatically waiting for specific events (e.g. navigation completed or DOM loaded) to be fired, however, this does not work well in some scenarios.
    ///         This method should only be used in those rare cases which are not supported by the automatic waiting built into Playwright.
    ///     </para>
    /// </summary>
    public static async Task RunTaskAndWaitForResponses(this IPage page, Task apiCallingTask, string[] endpointSubstrings, int additionalWaitInMilliseconds = 0)
        => await Task.WhenAll(endpointSubstrings.Select(endpointSubstring => page.WaitForResponseAsync(response => response.Url.Contains(endpointSubstring)))
            .Prepend(apiCallingTask)).ContinueWith(task => Task.Delay(additionalWaitInMilliseconds));
}
