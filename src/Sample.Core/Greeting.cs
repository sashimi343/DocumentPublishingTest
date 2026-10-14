namespace Sample.Core;

/// <summary>
///     <para xml:lang="en">An immutable greeting message.</para>
///     <para xml:lang="ja">不変の挨拶メッセージ。</para>
/// </summary>
/// <param name="Text">
///     <para xml:lang="en">The greeting text.</para>
///     <para xml:lang="ja">挨拶の本文。</para>
/// </param>
/// <seealso cref="Greeter"/>
public sealed record Greeting(string Text);
