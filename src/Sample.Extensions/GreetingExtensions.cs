using Sample.Core;

namespace Sample.Extensions;

/// <summary>
///     <para xml:lang="en">Extension methods for <see cref="Greeting"/>.</para>
///     <para xml:lang="ja"><see cref="Greeting"/>の拡張メソッド。</para>
/// </summary>
public static class GreetingExtensions
{
    /// <summary>
    ///     <para xml:lang="en">Converts the <see cref="Greeting.Text"/> to upper case.</para>
    ///     <para xml:lang="ja"><see cref="Greeting.Text"/>を大文字に変換する。</para>
    /// </summary>
    /// <param name="greeting">
    ///     <para xml:lang="en">The source greeting.</para>
    ///     <para xml:lang="ja">変換元の挨拶。</para>
    /// </param>
    /// <returns>
    ///     <para xml:lang="en">A new <see cref="Greeting"/> in upper case.</para>
    ///     <para xml:lang="ja">大文字に変換した新しい<see cref="Greeting"/>。</para>
    /// </returns>
    /// <seealso cref="Greeter.Greet(string)"/>
    /// <seealso cref="LoudGreeter"/>
    public static Greeting ToLoud(this Greeting greeting) => greeting with { Text = greeting.Text.ToUpperInvariant() };
}
